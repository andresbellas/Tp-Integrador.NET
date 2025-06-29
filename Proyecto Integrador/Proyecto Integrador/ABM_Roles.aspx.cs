using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Integrador
{
    public partial class ABM_Roles : System.Web.UI.Page
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
                string modo = Request.QueryString["modo"];
                if (!string.IsNullOrEmpty(modo))
                {
                    ModoABM = modo;
                    EjecutarAccion(modo);
                }
            }
        }

        private void EjecutarAccion(string accion)
        {
            switch (accion)
            {
                case "altaRol":
                    MostrarFormulario(true);
                    LimpiarFormulario();
                    chkBajaRol.Visible = false;
                    break;

                case "modificacionRol":
                case "eliminarRol":
                    gvRoles.Visible = true;
                    CargarRoles();
                    PanelFormulario.Visible = false;
                    break;
            }
        }

        private void CargarRoles()
        {
            L_Rol logica = new L_Rol();
            gvRoles.DataSource = logica.ListarRoles();
            gvRoles.DataBind();
        }

        protected void gvRoles_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            btnVolverGrilla.Visible = false;
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idRol = (int)gvRoles.DataKeys[index].Value;

                L_Rol logica = new L_Rol();
                Rol seleccionado = logica.ListarRoles().FirstOrDefault(r => r.id_rol == idRol);

                if (seleccionado != null)
                {
                    txtIdRol.Text = seleccionado.id_rol.ToString();
                    txtNombreRol.Text = seleccionado.Nombre_rol;
                    txtDescripcion.Text = seleccionado.Descripcion;

                    MostrarFormulario(true);
                    gvRoles.Visible = false;

                    if (ModoABM == "eliminarRol")
                    {
                        txtNombreRol.Enabled = false;
                        txtDescripcion.Enabled = false;
                        chkBajaRol.Visible = true;
                        chkBajaRol.Checked = true;
                    }
                    else
                    {
                        txtNombreRol.Enabled = true;
                        txtDescripcion.Enabled = true;
                        chkBajaRol.Visible = false;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            L_Rol logicaRol = new L_Rol();
            string nombreIngresado = txtNombreRol.Text.Trim();
            string descripcionIngresada = txtDescripcion.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(nombreIngresado))
                {
                    lblMensaje.Text = "El nombre del rol es obligatorio.";
                    return;
                }

                if (ModoABM == "altaRol")
                {
                    // Obtener todos los roles
                    List<Rol> rolesExistentes = logicaRol.ListarRoles();

                    // Buscar si ya existe uno con el mismo nombre (ignorando mayúsculas y espacios)
                    bool existe = rolesExistentes.Find(r => r.Nombre_rol.Trim().ToLower() == nombreIngresado.ToLower()) != null;

                    if (existe)
                    {
                        lblMensaje.Text = "El rol ya existe. Ingrese un nombre diferente.";
                        return;
                    }

                    Rol nuevo = new Rol
                    {
                        Nombre_rol = nombreIngresado,
                        Descripcion = descripcionIngresada
                    };

                    logicaRol.AgregarRol(nuevo);
                    MostrarMensajeYRedirect("Rol agregado correctamente.", "Gerente.aspx");
                }
                else if (ModoABM == "modificacionRol")
                {
                    Rol modificado = new Rol
                    {
                        id_rol = int.Parse(txtIdRol.Text),
                        Nombre_rol = nombreIngresado,
                        Descripcion = descripcionIngresada
                    };

                    logicaRol.ModificarRol(modificado);
                    MostrarMensajeYRedirect("Rol modificado correctamente.", "Gerente.aspx");

                }

                else if(ModoABM == "eliminarRol")
                {
                    int id_rol = int.Parse(txtIdRol.Text);
                    logicaRol.EliminarRol(id_rol);
                    MostrarMensajeYRedirect("Se ha borrado el rol.", "Gerente.aspx");
                }

                    LimpiarFormulario();
                PanelFormulario.Visible = false;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar el rol: " + ex.Message;
                
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            string modo = ModoABM;  // Guardamos el valor antes de limpiar

            LimpiarFormulario();
            ModoABM = null;
            lblMensaje.Text = "";

            if (modo == "altaRol")
            {
                Response.Redirect("Gerente.aspx");
            }
            else if (modo == "modificacionRol" || modo == "eliminarRol")
            {
                Response.Redirect($"ABM_Roles.aspx?modo={modo}");
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


        private void LimpiarFormulario()
        {
            txtIdRol.Text = "";
            txtNombreRol.Text = "";
            txtDescripcion.Text = "";
            chkBajaRol.Checked = false;
        }

        private void MostrarFormulario(bool visible)
        {
            PanelFormulario.Visible = visible;
        }

        private void MostrarMensajeYRedirect(string mensaje, string urlRedirect)
        {
            string script = $"alert('{mensaje}'); window.location='{urlRedirect}';";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertRedirect", script, true);
        }

    }
}