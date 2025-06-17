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

                gvMesas.DataSource = mesas;
                gvMesas.DataBind();
            }
        }
    }
}

