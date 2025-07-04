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
            }


        }

        protected void Accion_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string accion = btn.CommandArgument;

            switch (accion)
            {

                case "asignarMesero":
                    // Lógica para asignar mesero
                    break;
                case "desasignarMesero":
                    // Lógica para desasignar mesero
                    break;
                case "abrirPed":
                    // Lógica para abrir pedido
                    break;
                case "cancelarPed":
                    // Lógica para cancelar pedido
                    break;
                case "cerrarPed":
                    // Lógica para cerrar pedido
                    break;
                case "gestionMesero":
                    // Lógica para gestionar meseros
                    break;
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

            txtFiltroMedioPago.Text = "";
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

            // ejemplo con pedidos
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
                // Obtenemos el Id_mesa desde CommandArgument
                int idMesa = Convert.ToInt32(e.CommandArgument);

                L_Mesa logicaMesa = new L_Mesa();
                Mesa mesa = logicaMesa.BuscarPorIdMesa(idMesa);
                Session["MesaSeleccionada"] = mesa;

                // Obtenemos el estado actual para decidir qué acción hacer
                int estadoMesa = mesa.Id_Estado;

                if (estadoMesa == 1) // 1 = Libre, Abrir Pedido
                {
                    // Podés crear el pedido aquí o derivar para crearlo
                    Response.Redirect("Pedido.aspx?modo=abrir");
                }
                else
                {
                    // Mesa ocupada, gestionamos pedido
                    Response.Redirect("Pedido.aspx?modo=gestionar");
                }
            }
        }


    }

}