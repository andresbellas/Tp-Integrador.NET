using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Integrador
{
    public partial class ABM_Insumos : System.Web.UI.Page
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
                    if (ModoABM == "listarInsumos")
                    {
                        txtSku.Enabled = false;
                        txtNombre.Enabled = false;
                        txtPrecio.Enabled = false;
                        txtCantidad.Enabled = false;
                        btnGuardar.Visible = false;
                    }
                    else
                    {
                        txtSku.Enabled = true;
                        txtNombre.Enabled = true;
                        txtPrecio.Enabled = true;
                        txtCantidad.Enabled = true;
                        btnGuardar.Visible = true;
                    }

                }
            }
        }

        private void EjecutarAccion(string accion)
        {
            switch (accion)
            {
                case "altaInsumo":
                    btnVolverGrilla.Visible = false;    
                    MostrarFormulario(true);
                    LimpiarFormulario();
                    break;

                case "modificacionInsumo":
                case "listarInsumos":
                    CargarInsumos();
                    gvInsumos.Visible = true;
                    PanelFormulario.Visible = false;
                    break;
            }
        }

        private void CargarInsumos()
        {
            L_Insumos logica = new L_Insumos();
            gvInsumos.DataSource = logica.ListarInsumos();
            gvInsumos.DataBind();
        }

        protected void gvInsumos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            btnVolverGrilla.Visible = false;
            if (e.CommandName == "Seleccionar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idInsumo = (int)gvInsumos.DataKeys[index].Value;

                L_Insumos logica = new L_Insumos();
                Insumos ins = logica.ListarInsumos().FirstOrDefault(x => x.Id_insumo == idInsumo);

                if (ins != null)
                {
                    txtIdInsumo.Text = ins.Id_insumo.ToString();
                    txtSku.Text = ins.Sku;
                    txtNombre.Text = ins.Nombre;
                    txtPrecio.Text = ins.Precio.ToString();
                    txtCantidad.Text = ins.Cantidad.ToString();

                    MostrarFormulario(true);
                    gvInsumos.Visible = false;

                    if (ModoABM == "listarInsumos")
                    {
                        txtSku.Enabled = false;
                        txtNombre.Enabled = false;
                        txtPrecio.Enabled = false;
                        txtCantidad.Enabled = false;
                        btnGuardar.Visible = false;
                    }
                    else
                    {
                        txtSku.Enabled = true;
                        txtNombre.Enabled = true;
                        txtPrecio.Enabled = true;
                        txtCantidad.Enabled = true;
                        btnGuardar.Visible = true;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            L_Insumos logica = new L_Insumos();

            try
            {
                string sku = txtSku.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                double precio = double.TryParse(txtPrecio.Text, out double p) ? p : -1;
                int cantidad = int.TryParse(txtCantidad.Text, out int c) ? c : -1;

                if (string.IsNullOrEmpty(sku) || string.IsNullOrEmpty(nombre) || precio < 0 || cantidad < 0)
                {
                    lblMensaje.Text = "Todos los campos son obligatorios y deben tener valores válidos.";
                    return;
                }

                Insumos insumo = new Insumos
                {
                    Sku = sku,
                    Nombre = nombre,
                    Precio = precio,
                    Cantidad = cantidad
                };

                switch (ModoABM)
                {
                    case "altaInsumo":
                        logica.AgregarInsumo(insumo);
                        MostrarMensajeYRedirect("Insumo agregado correctamente.", "Gerente.aspx");
                        break;

                    case "modificacionInsumo":
                        insumo.Id_insumo = int.Parse(txtIdInsumo.Text);
                        logica.ModificarInsumo(insumo);
                        MostrarMensajeYRedirect("Insumo modificado correctamente.", "Gerente.aspx");
                        break;

                    case "eliminarInsumo":
                        int idEliminar = int.Parse(txtIdInsumo.Text);
                        logica.EliminarInsumo(idEliminar);
                        MostrarMensajeYRedirect("Insumo eliminado correctamente.", "Gerente.aspx");
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

            if (modo == "altaInsumo")
            {
                Response.Redirect("Gerente.aspx");
            }
            else if (modo == "modificacionInsumo" || modo == "listarInsumos")
            {
                Response.Redirect($"ABM_Insumos.aspx?modo={modo}");
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
            txtIdInsumo.Text = "";
            txtSku.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        private void MostrarFormulario(bool visible)
        {
            PanelFormulario.Visible = visible;
        }

        private void MostrarMensajeYRedirect(string mensaje, string url)
        {
            string script = $"alert('{mensaje}'); window.location='{url}';";
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", script, true);
        }
    }
}
