using System;
using Entidades;
using Logica;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace Proyecto_Integrador
{
    public partial class ABM_Empleados : System.Web.UI.Page
    {
        protected string ModoABM
        {
            get { return ViewState["ModoABM"]?.ToString(); }
            set { ViewState["ModoABM"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelFormulario.Visible = false;

                Empleados emp = Session["empleado"] as Empleados;

                if (emp == null || emp.RolEmpleado.id_rol != 1)
                {
                    Response.Redirect("Default.aspx");
                }



                string modo = Request.QueryString["modo"];
                if (!string.IsNullOrEmpty(modo))
                {
                    ModoABM = modo;
                    EjecutarAccion(modo);
                }
                CargarRoles();
            }
        }

        protected void Accion_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string accion = btn.CommandArgument;
            ModoABM = accion;
            LimpiarFormulario();
            EjecutarAccion(accion);
        }

        private void EjecutarAccion(string accion)
        {
            LimpiarFormulario();

            MostrarFormulario(true);

            switch (accion)
            {
                case "altaEmpl":
                    HabilitarCamposEmpleado(true);
                    MostrarCamposEmpleado(true);
                    MostrarCamposUsuario(true);
                    divBaja.Visible = false;
                    chkBaja.Visible = false;
                    btnVolverGrilla.Visible = false;
                    break;

                case "modificacionEmpl":
                    PanelFormulario.Visible = false;
                    CargarTodosLosEmpleados();
                    break;

                case "bajaEmpl":
                    PanelFormulario.Visible = false;
                    CargarTodosLosEmpleados();
                    break;

                default:
                    MostrarFormulario(false);

                    break;
            }
        }

        private List<Empleados> EmpleadosEnMemoria
        {
            get { return (List<Empleados>)ViewState["Empleados"] ?? new List<Empleados>(); }
            set { ViewState["Empleados"] = value; }
        }

        private void CargarTodosLosEmpleados()
        {
            L_Empleados logica = new L_Empleados();
            EmpleadosEnMemoria = logica.ListarEmpleados();
            gvEmpleados.DataSource = EmpleadosEnMemoria;
            gvEmpleados.DataBind();

        }

        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            string filtroNombre = "";
            string filtroApellido = "";

            TextBox txtNombre = (TextBox)gvEmpleados.HeaderRow.FindControl("txtFiltroNombre");
            TextBox txtApellido = (TextBox)gvEmpleados.HeaderRow.FindControl("txtFiltroApellido");

            if (txtNombre != null) filtroNombre = txtNombre.Text.Trim();
            if (txtApellido != null) filtroApellido = txtApellido.Text.Trim();

            var filtrados = EmpleadosEnMemoria
                .Where(x =>
                    (string.IsNullOrEmpty(filtroNombre) || x.Nombre.IndexOf(filtroNombre, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    (string.IsNullOrEmpty(filtroApellido) || x.Apellido.IndexOf(filtroApellido, StringComparison.OrdinalIgnoreCase) >= 0)
                )
                .ToList();

            gvEmpleados.DataSource = filtrados;
            gvEmpleados.DataBind();
        }
        protected void gvEmpleados_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            btnVolverGrilla.Visible = false;
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idEmpleado = (int)gvEmpleados.DataKeys[index].Value;

                L_Empleados logica = new L_Empleados();
                Empleados emp = logica.ListarEmpleados().FirstOrDefault(x => x.id_empleado == idEmpleado);

                if (emp != null)
                {
                    CargarEmpleadoEnFormulario(emp);
                    ViewState["IdUsuario"] = emp.UsuarioEmpleado?.Id_Usuario ?? 0;

                    // Ocultar la grilla
                    gvEmpleados.Visible = false;

                    // Mostrar el formulario para edición
                    MostrarFormulario(true);

                    if (ModoABM == "bajaEmpl")
                    {
                        // En modo baja, campos deshabilitados excepto el checkbox de baja
                        HabilitarCamposEmpleado(false);
                        HabilitarCamposUsuario (false);
                        MostrarCamposEmpleado(true);
                        MostrarCamposUsuario(true);
                        chkBaja.Enabled = true;  // Solo el checkbox habilitado
                        
                    }
                    else
                    {
                        // En otros modos, todo habilitado
                        HabilitarCamposEmpleado(true);
                        MostrarCamposEmpleado(true);
                        MostrarCamposUsuario(true);
                        chkBaja.Enabled = false;
                    }
                }
            }
        }



        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            L_Empleados logica = new L_Empleados();
            Empleados emp;

            L_Usuario l_usuario = new L_Usuario();
            Usuarios nuevoUsuario = ObtenerUsuarioDelFormulario();
            int idUsuario;
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtLegajo.Text) ||
                string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                lblMensaje.Text = "Por favor completa todos los campos obligatorios.";
                return;
            }

            if (!int.TryParse(txtLegajo.Text, out _))
            {
                lblMensaje.Text = "El campo Legajo debe ser un numero.";
                return;
            }

            try
            {
                switch (ModoABM)
                {
                    case "altaEmpl":
                        divBaja.Visible = false;

                        idUsuario = l_usuario.AgregarUsuario(nuevoUsuario);
                        emp = ObtenerEmpleadoDelFormulario(idUsuario);
                        logica.AgregarEmpleado(emp);
                        lblMensaje.Text = "Empleado agregado correctamente.";
                        btnAceptarConfirmacion.Visible = true;
                        btnGuardar.Enabled = false;

                        break;

                    case "modificacionEmpl":
                        if (ViewState["IdUsuario"] != null)
                        {
                            idUsuario = (int)ViewState["IdUsuario"];
                            emp = ObtenerEmpleadoDelFormulario(idUsuario);
                            logica.ModificarEmpleado(emp);
                            lblMensaje.Text = "Empleado modificado correctamente.";
                            btnAceptarConfirmacion.Visible = true;
                            btnGuardar.Enabled = false;
                        }
                        else
                        {
                            lblMensaje.Text = "No se pudo obtener el ID del usuario para modificar.";
                        }
                        break;

                    case "bajaEmpl":
                        lblMensaje.Text = "Empleado dado de baja.";
                        LimpiarFormulario();
                        break;

                    default:
                        lblMensaje.Text = "Accion invalida.";
                        break;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }


        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            string modo = ModoABM;  // Guardamos el valor antes de limpiar

            LimpiarFormulario();
            ModoABM = null;
            lblMensaje.Text = "";

            if (modo == "altaEmpl")
            {
                Response.Redirect("Gerente.aspx");
            }
            else if (modo == "modificacionEmpl" || modo == "bajaEmpl")
            {
                Response.Redirect($"ABM_Empleados.aspx?modo={modo}");
            }
            else
            {
                Response.Redirect("Gerente.aspx");
            }
        }

        protected void btnVolverGrilla_Click(object sender, EventArgs e)
        {
            Response.Redirect("Gerente.aspx");
        }


        private void CargarEmpleadoEnFormulario(Empleados e)
        {
            txtId.Text = e.id_empleado.ToString();
            txtNombre.Text = e.Nombre;
            txtApellido.Text = e.Apellido;
            txtLegajo.Text = e.Nro_Legajo.ToString();
            txtUsuario.Text = e.UsuarioEmpleado?.Usuario;
            txtContraseña.Text = e.UsuarioEmpleado?.Contraseña;
            ddlRoles.SelectedValue = e.RolEmpleado.id_rol.ToString();
            chkBaja.Checked = e.baja;
        }


        private Empleados ObtenerEmpleadoDelFormulario(int idUsuario)
        {
            return new Empleados
            {
                id_empleado = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Nro_Legajo = int.TryParse(txtLegajo.Text, out int leg) ? leg : 0,
                baja = chkBaja.Checked,
                UsuarioEmpleado = new Usuarios
                {
                    Id_Usuario = idUsuario,
                    Usuario = txtUsuario.Text,
                    Contraseña = txtContraseña.Text
                },
                RolEmpleado = new Rol
                {
                    id_rol = int.Parse(ddlRoles.SelectedValue)
                }
            };
        }



        private Usuarios ObtenerUsuarioDelFormulario()
        {
            return new Usuarios
            {
                Usuario = txtUsuario.Text,
                Contraseña = txtContraseña.Text
            };
        }

        private void CargarRoles()
        {
            L_Rol logicaRol = new L_Rol();
            ddlRoles.DataSource = logicaRol.ListarRoles();
            ddlRoles.DataTextField = "Nombre_rol";
            ddlRoles.DataValueField = "id_rol";
            ddlRoles.DataBind();
        }

        protected void btnAceptarConfirmacion_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            btnGuardar.Visible = false;
            btnVolver.Visible = false;
            btnAceptarConfirmacion.Visible = false;
            btnGuardar.Enabled = true;
            lblMensaje.Text = "";
            MostrarFormulario(true);
        }

        private void LimpiarFormulario()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtLegajo.Text = "";
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            lblMensaje.Text = "";
        }

        private void MostrarFormulario(bool visible)
        {
            PanelFormulario.Visible = visible;
            btnGuardar.Visible = true;
            btnVolver.Visible = true;
        }

        private void MostrarCamposEmpleado(bool visible)
        {
            txtNombre.Visible = visible;
            txtApellido.Visible = visible;
            txtLegajo.Visible = visible;
        }

        private void HabilitarCamposEmpleado(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtLegajo.Enabled = habilitar;
        }

        private void MostrarCamposUsuario(bool visible)
        {
            txtUsuario.Visible = visible;
            txtContraseña.Visible = visible;
        }
        private void HabilitarCamposUsuario(bool habilitar)
        {
            ddlRoles.Enabled = habilitar;   
            txtUsuario.Enabled = habilitar;
            txtContraseña.Enabled = habilitar;
        }

    }
}
