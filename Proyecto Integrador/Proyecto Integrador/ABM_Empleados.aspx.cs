using System;
using Entidades;
using Logica;
using System.Web.UI.WebControls;

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
                PanelBuscarEmpleado.Visible = false;

                string modo = Request.QueryString["modo"];
                if (!string.IsNullOrEmpty(modo))
                {
                    ModoABM = modo;
                    EjecutarAccion(modo);
                }
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

            // Mostrar u ocultar panel de búsqueda según la acción
            PanelBuscarEmpleado.Visible = accion == "modificacionEmpl" || accion == "bajaEmpl";

            switch (accion)
            {
                case "altaEmpl":
                    HabilitarCamposEmpleado(true);
                    MostrarCamposEmpleado(true);
                    MostrarCamposUsuario(false);
                    break;

                case "modificacionEmpl":
                    HabilitarCamposEmpleado(true);
                    MostrarCamposEmpleado(true);
                    MostrarCamposUsuario(true);
                    break;

                case "bajaEmpl":
                    HabilitarCamposEmpleado(false);
                    MostrarCamposEmpleado(true);
                    MostrarCamposUsuario(true);
                    break;

                case "altaLogeoEmpl":
                    HabilitarCamposEmpleado(false);
                    MostrarCamposEmpleado(false);
                    MostrarCamposUsuario(true);
                    break;

                default:
                    MostrarFormulario(false);
                    PanelBuscarEmpleado.Visible = false;
                    break;
            }
        }

        protected void btnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            string nombre = txtBuscarNombre.Text.Trim();
            L_Empleados logica = new L_Empleados();
            Empleados encontrado = logica.ListarEmpleados().Find(emp => emp.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (encontrado != null)
            {
                CargarEmpleadoEnFormulario(encontrado);
                lblMensaje.Text = "";

                if (ModoABM == "bajaEmpl")
                {
                    HabilitarCamposEmpleado(false);
                }
                else if (ModoABM == "modificacionEmpl")
                {
                    HabilitarCamposEmpleado(true);
                }
            }
            else
            {
                lblMensaje.Text = "Empleado no encontrado.";
                LimpiarFormulario();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            L_Empleados logica = new L_Empleados();
            Empleados emp = ObtenerEmpleadoDelFormulario();

            try
            {
                switch (ModoABM)
                {
                    case "altaEmpl":
                        logica.AgregarEmpleado(emp);
                        lblMensaje.Text = "Empleado agregado correctamente.";
                        break;

                    case "modificacionEmpl":
                        logica.ModificarEmpleado(emp);
                        lblMensaje.Text = "Empleado modificado correctamente.";
                        break;

                    case "bajaEmpl":
                        logica.BajaLogicaEmpleado(emp.id_empleado);
                        lblMensaje.Text = "Empleado dado de baja lógicamente.";
                        break;

                    case "altaLogeoEmpl":
                        // Agregar la lógica real para guardar usuario
                        lblMensaje.Text = "Alta de usuario registrada (lógica no implementada).";
                        break;

                    default:
                        lblMensaje.Text = "Acción inválida.";
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
            LimpiarFormulario();
            ModoABM = null;
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
            // Cargar rol si usas DropDownList para roles
        }

        private Empleados ObtenerEmpleadoDelFormulario()
        {
            return new Empleados
            {
                id_empleado = string.IsNullOrEmpty(txtId.Text) ? 0 : int.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Nro_Legajo = int.TryParse(txtLegajo.Text, out int leg) ? leg : 0,
                baja = false,
                UsuarioEmpleado = new Usuarios
                {
                    Usuario = txtUsuario.Text,
                    Contraseña = txtContraseña.Text
                },
                RolEmpleado = new Rol
                {
                    id_rol = 1 // O tomar del DropDownList si tienes
                }
            };
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
            PanelBuscarEmpleado.Visible = false;
        }

        private void MostrarFormulario(bool visible)
        {
            PanelFormulario.Visible = visible;
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
    }
}
