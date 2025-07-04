using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Integrador
{
    public partial class Cobrar : System.Web.UI.Page
    {


        L_MedioDePago lMediosPago = new L_MedioDePago();
        Pedidos Vigente = new Pedidos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["IdPedido"] != null)
                {
                    int idPedido = (int)Session["IdPedido"];
                    L_Pedidos pago = new L_Pedidos();
                    Vigente = pago.BuscarPorId(idPedido);
                    lblNroPedido.Text = Vigente.Nro_Pedido.ToString();

                    lblNroPedido.Text = idPedido.ToString();


                    List<MedioDePago> medios = lMediosPago.ListarMedios();

                    ddlMedioPago.DataSource = medios;
                    ddlMedioPago.DataTextField = "Nombre_Pago";
                    ddlMedioPago.DataValueField = "Id_Pago";
                    ddlMedioPago.DataBind();

                    ddlMedioPago.Items.Insert(0, new ListItem("--Seleccione método--", "0"));


                    float total = CalcularTotalPedido(idPedido);
                    txtMontoTotal.Text = total.ToString("C");


                }
                else
                {
                    Response.Redirect("Pedido.aspx");
                }
            }
        }

        public float CalcularTotalPedido(int idPedido)
        {

            L_ItemPedidos lItemPedidos = new L_ItemPedidos();
            List<ItemPedidos> items = lItemPedidos.ListarPorPedido(idPedido);
            float total = 0;

            foreach (ItemPedidos item in items)
            {
                total += item.Total;
            }

            return total;
        }




        protected void btnVolver_Click(object sender, EventArgs e)
        {

            Response.Redirect("Pedido.aspx");
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            // Validar método de pago
            if (ddlMedioPago.SelectedValue == "0")
            {
                lblMensaje.Text = "Debe seleccionar un método de pago.";
                lblMensaje.Visible = true;
                return;
            }

            try
            {
                int idPago = int.Parse(ddlMedioPago.SelectedValue);
                int idPedido = (int)Session["IdPedido"];
                float total = CalcularTotalPedido(idPedido);

           
                Cobranza nuevaCobranza = new Cobranza
                {
                    Id_Pedido = idPedido,
                    Total = total,
                    MedioDePago = new MedioDePago
                    {
                        Id_Pago = idPago
                    }
                };

                L_Cobranza logicaCobranza = new L_Cobranza();
                logicaCobranza.AgregarCobranza(nuevaCobranza);

                // Cambiar estado del pedido a Finalizado
                L_Pedidos lPedidos = new L_Pedidos();
                Pedidos pedido = lPedidos.BuscarPorId(idPedido);
                pedido.Id_Estado = 4;
                lPedidos.Modificar(pedido);

                // Cambiar estado de la mesa a Libre 
                int idMesa = pedido.MesaAsignada.Id_mesa;
                L_Mesa lMesa = new L_Mesa();
                lMesa.CambiarEstadoMesa(idMesa, 1);

                // Limpiar Session
                Session.Remove("IdPedido");

                
                Empleados empleado = Session["empleado"] as Empleados;
                if (empleado != null)
                {
                    string rol = empleado.RolEmpleado.Nombre_rol.ToLower();
                    if (rol == "gerente")
                        Response.Redirect("Gerente.aspx");
                    else if (rol == "mesero")
                        Response.Redirect("Mesero.aspx");
                    else
                        Response.Redirect("Default.aspx");
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al procesar el cobro: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }






    }
}