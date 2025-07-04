using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using Entidades;

namespace Proyecto_Integrador
{
    public partial class Gerente : Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                List<Mesa> mesas = new List<Mesa>();
                L_Mesa l_Mesa = new L_Mesa();
                mesas = l_Mesa.ListarMesas();

                Empleados emp = Session["empleado"] as Empleados;
                if (emp == null || emp.RolEmpleado.id_rol != 1)
                {
                    Response.Redirect("Default.aspx");
                }

                gvMesas.DataSource = mesas;
                gvMesas.DataBind();
                 
                // cargar métodos de pago
                L_MedioDePago logicaMedios = new L_MedioDePago();
                List<MedioDePago> listaMedios = logicaMedios.ListarMedios();

                ddlFiltroMedioPago.DataSource = listaMedios;
                ddlFiltroMedioPago.DataTextField = "Nombre_Pago";
                ddlFiltroMedioPago.DataValueField = "Id_Pago";
                ddlFiltroMedioPago.DataBind();
                ddlFiltroMedioPago.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }


        }

        protected void Accion_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string accion = btn.CommandArgument;

            switch (accion)
            {

                
                //Empleados
                case "altaEmpl":
                    Response.Redirect("ABM_Empleados.aspx?modo=altaEmpl");
                    break;
                case "modificacionEmpl":
                    Response.Redirect("ABM_Empleados.aspx?modo=modificacionEmpl");
                    break;
                case "bajaEmpl":
                    Response.Redirect("ABM_Empleados.aspx?modo=bajaEmpl");
                    break;

                case "altaIns":
                    Response.Redirect("ABM_Insumos.aspx?modo=altaInsumo");
                    break;

                case "modificacionIns":
                    Response.Redirect("ABM_Insumos.aspx?modo=modificacionInsumo");
                    break;

                case "listarInsumos":
                    Response.Redirect("ABM_Insumos.aspx?modo=listarInsumos");
                    break;


                //ROL
                case "altaRol":
                    Response.Redirect("ABM_Roles.aspx?modo=altaRol");
                    break;
                case "modificacionRol":
                    Response.Redirect("ABM_Roles.aspx?modo=modificacionRol");
                    break;
                case "eliminarRol":
                    Response.Redirect("ABM_Roles.aspx?modo=eliminarRol");
                    break;
                //Pagos
                case "altaMedPago":
                    Response.Redirect("ABM_MediosDePago.aspx?modo=altaMedio");
                    break;

                case "modificacionMedPago":
                    Response.Redirect("ABM_MediosDePago.aspx?modo=modificacionMedio");
                    break;

                case "eliminarMedPago":
                    Response.Redirect("ABM_MediosDePago.aspx?modo=eliminarMedio");
                    break;

                default:
                    break;
            }
        }


        protected void btnGestionGeneral_Click(object sender, EventArgs e)
        {
            MostrarMesas();

            divMesas.Visible = true;
            divHistorial.Visible = false;
            divBotones.Visible = true;
            tituloGestion.Visible = true;
            divHistorialPedidos.Visible = false;

            btnGestionGeneral.CssClass = "nav-link active";
            btnHistorial.CssClass = "nav-link";
            btnHistorialPedidos.CssClass = "nav-link";
        }

        protected void btnHistorial_Click(object sender, EventArgs e)
        {
            divMesas.Visible = false;
            divHistorial.Visible = true;
            divBotones.Visible = false;
            tituloGestion.Visible = false;
            divHistorialPedidos.Visible = false;

            btnHistorial.CssClass = "nav-link active";
            btnGestionGeneral.CssClass = "nav-link";
            btnHistorialPedidos.CssClass = "nav-link";

            L_Cobranza l_cobranza = new L_Cobranza();
            List<Cobranza> listaCobranzas = l_cobranza.ListarCobranzas();

            gvCobranzas.DataSource = listaCobranzas;
            gvCobranzas.DataBind();

            ddlFiltroMedioPago.ClearSelection();
        }

        private void MostrarMesas()
        {
            L_Mesa l_Mesa = new L_Mesa();
            var mesas = l_Mesa.ListarMesas();

            gvMesas.DataSource = mesas;
            gvMesas.DataBind();
        }


        protected void btnHistorialPedidos_Click(object sender, EventArgs e)
        {
            divMesas.Visible = false;
            divHistorial.Visible = false;
            divBotones.Visible = false;
            tituloGestion.Visible = false;

            divHistorialPedidos.Visible = true;

            btnHistorialPedidos.CssClass = "nav-link active";
            btnGestionGeneral.CssClass = "nav-link";
            btnHistorial.CssClass = "nav-link";

           
            L_Pedidos l_pedido = new L_Pedidos();
            List<Pedidos> listaPedidos = l_pedido.Listar();

            gvPedidos.DataSource = listaPedidos;
            gvPedidos.DataBind();



        }

        protected void Informacion_Click(object sender, EventArgs e)
        {
            L_Empleados logica = new L_Empleados();
            Button btn = (Button)sender;


            int legajo = Convert.ToInt32(btn.CommandArgument);


            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int idMesa = Convert.ToInt32(gvMesas.DataKeys[row.RowIndex].Value);

            Empleados empleado = logica.EmpleadoPorLegajo(legajo);
            Session["MeseroSolicitado"] = empleado;

            L_Mesa logicaMesa = new L_Mesa();
            Mesa mesa = logicaMesa.BuscarPorIdMesa(idMesa);
            Session["MesaSeleccionada"] = mesa;

            Response.Redirect("InformacionMesero.aspx", false);

        }

        protected void gvMesas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "accionMesa")
            {
                
                int idMesa = Convert.ToInt32(e.CommandArgument);

                L_Mesa logicaMesa = new L_Mesa();
                Mesa mesa = logicaMesa.BuscarPorIdMesa(idMesa);
                Session["MesaSeleccionada"] = mesa;


                int estadoMesa = mesa.Id_Estado;

                if (estadoMesa == 1)
                {

                    Response.Redirect("Pedido.aspx?modo=abrir");
                }
                else
                {

                    Response.Redirect("Pedido.aspx?modo=gestionar");
                }
            }
        }

        protected void btnFiltrarMedioPago_Click(object sender, EventArgs e)
        {
            string filtroMedio = ddlFiltroMedioPago.SelectedValue; 

            L_Cobranza l_cobranza = new L_Cobranza();
            List<Cobranza> listaCobranzas = l_cobranza.ListarCobranzas();

            List<Cobranza> filtradas;

            if (string.IsNullOrEmpty(filtroMedio))
            {
                filtradas = listaCobranzas;
            }
            else
            {
               
                int idPago = int.Parse(filtroMedio);
                filtradas = listaCobranzas
                    .Where(c => c.MedioDePago != null && c.MedioDePago.Id_Pago == idPago)
                    .ToList();
            }

            gvCobranzas.DataSource = filtradas;
            gvCobranzas.DataBind();
        }

        protected void btnFiltrarPedido_Click(object sender, EventArgs e)
        {
            string textoPedido = txtFiltroPedido.Text.Trim();
            int numeroPedido;

            if (int.TryParse(textoPedido, out numeroPedido))
            {
                L_Cobranza l_cobranza = new L_Cobranza();
                List<Cobranza> listaCobranzas = l_cobranza.ListarCobranzas();

                var filtradas = listaCobranzas
                    .Where(c => c.Nro_Pedido == numeroPedido)
                    .ToList();

                gvCobranzas.DataSource = filtradas;
                gvCobranzas.DataBind();
            }
            else
            {
                
            }
        }



    }

}