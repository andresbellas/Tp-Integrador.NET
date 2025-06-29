using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Proyecto_Integrador
{
    public partial class ABM_MediosDePago : System.Web.UI.Page
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
                
                Empleados emp = Session["empleado"] as Empleados;

                if (emp == null || emp.RolEmpleado.id_rol != 1)
                {
                    Response.Redirect("Default.aspx");
                }



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
                case "altaMedio":
                    MostrarFormulario(true);
                    LimpiarFormulario();
                    chkHabilitado.Visible = false;
                    btnVolverGrilla.Visible = false;
                    break;

                case "modificacionMedio":
                case "eliminarMedio":
                    gvMediosPago.Visible = true;
                    CargarMedios();
                    PanelFormulario.Visible = false;
                    break;
            }
        }

        private void CargarMedios()
        {
            L_MedioDePago logica = new L_MedioDePago();
            gvMediosPago.DataSource = logica.ListarMedios();
            gvMediosPago.DataBind();
        }

        protected void gvMediosPago_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            btnVolverGrilla.Visible = false;
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int id = (int)gvMediosPago.DataKeys[index].Value;

                L_MedioDePago logica = new L_MedioDePago();
                MedioDePago seleccionado = logica.ListarMedios().FirstOrDefault(m => m.Id_Pago == id);

                if (seleccionado != null)
                {
                    txtIdMedio.Text = seleccionado.Id_Pago.ToString();
                    txtNombre.Text = seleccionado.Nombre_Pago;
                    txtDescripcion.Text = seleccionado.Descripcion;

                    MostrarFormulario(true);
                    gvMediosPago.Visible = false;

                    if (ModoABM == "eliminarMedio")
                    {
                        txtNombre.Enabled = false;
                        txtDescripcion.Enabled = false;
                        chkHabilitado.Visible = true;
                        chkHabilitado.Checked = false;
                    }
                    else
                    {
                        txtNombre.Enabled = true;
                        txtDescripcion.Enabled = true;
                        chkHabilitado.Visible = true;
                        chkHabilitado.Checked = true;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            L_MedioDePago logica = new L_MedioDePago();
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    lblMensaje.Text = "El nombre es obligatorio.";
                    return;
                }

                if (ModoABM == "altaMedio")
                {
                    var existentes = logica.ListarMedios();
                    bool existe = existentes.Any(m => m.Nombre_Pago.Trim().ToLower() == nombre.ToLower());

                    if (existe)
                    {
                        lblMensaje.Text = "El medio de pago ya existe.";
                        return;
                    }

                    MedioDePago nuevo = new MedioDePago
                    {
                        Nombre_Pago = nombre,
                        Descripcion = descripcion,
                        habilitado = true
                    };
                    logica.AgregarMedio(nuevo);
                    MostrarMensajeYRedirect("Medio de pago agregado correctamente.", "Gerente.aspx");
                }
                else if (ModoABM == "modificacionMedio")
                {
                    MedioDePago modificado = new MedioDePago
                    {
                        Id_Pago = int.Parse(txtIdMedio.Text),
                        Nombre_Pago = nombre,
                        Descripcion = descripcion,
                        habilitado = chkHabilitado.Checked
                    };
                    logica.ModificarMedio(modificado);
                    MostrarMensajeYRedirect("Medio de pago modificado correctamente.", "Gerente.aspx");
                }
                else if (ModoABM == "eliminarMedio")
                {
                    int id = int.Parse(txtIdMedio.Text);
                    logica.EliminarMedioLogico(id);
                    MostrarMensajeYRedirect("Medio de pago dado de baja.", "Gerente.aspx");
                }

                LimpiarFormulario();
                PanelFormulario.Visible = false;
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

            if (modo == "altaMedio")
            {
                Response.Redirect("Gerente.aspx");
            }
            else if (modo == "modificacionMedio" || modo == "eliminarMedio")
            {
                Response.Redirect($"ABM_MediosDePago.aspx?modo={modo}");
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
            txtIdMedio.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            chkHabilitado.Checked = true;
        }

        private void MostrarFormulario(bool visible)
        {
            PanelFormulario.Visible = visible;
        }

        private void MostrarMensajeYRedirect(string mensaje, string url)
        {
            string script = $"alert('{mensaje}'); window.location='{url}';";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertRedirect", script, true);
        }
    }
}
