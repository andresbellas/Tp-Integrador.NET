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


            lblMensaje.Text = "Botón Aceptar presionado.";
            lblMensaje.Visible = true;
        }

    }
}