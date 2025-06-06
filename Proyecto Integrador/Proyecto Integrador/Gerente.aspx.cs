using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Integrador
{
    public partial class Gerente : Page
    {
        public class Mesa
        {
            public int MesaId { get; set; }
            public int NumeroMesa { get; set; }

            public string NumeroLegajo { get; set; }
            public string Estado { get; set; }
           
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var mesas = new List<Mesa>
                {
                    new Mesa { MesaId = 1, NumeroMesa = 1, Estado = "Libre", NumeroLegajo = "6656" },
                    new Mesa { MesaId = 2, NumeroMesa = 2, Estado = "Ocupada", NumeroLegajo = "7657" },
                    new Mesa { MesaId = 3, NumeroMesa = 3, Estado = "Reservada", NumeroLegajo = "7657" }
                };

                gvMesas.DataSource = mesas;
                gvMesas.DataBind();
            }
        }
    }
}