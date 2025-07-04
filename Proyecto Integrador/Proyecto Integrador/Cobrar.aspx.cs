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
        protected void Page_Load(object sender, EventArgs e)
        {

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