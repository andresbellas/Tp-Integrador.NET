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
    public partial class Mesero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Empleados emp = Session["empleado"] as Empleados;

                if(emp == null || emp.RolEmpleado.id_rol != 2)
                {
                    Response.Redirect("Default.aspx");
                }
                lblNombreMesero.Text = emp.Nombre + " " + emp.Apellido;

                if (emp != null)
                {
                    int legajoMesero = emp.Nro_Legajo;

                    L_Mesa l_Mesa = new L_Mesa();
                    List<Mesa> Mesas = l_Mesa.ListarMesas();

                    // filtramo las mesas
                    List<Mesa> mesasDelMesero = Mesas
                        .Where(m => m.Nro_Legajo == legajoMesero)
                        .ToList();

                    gvMesas.DataSource = mesasDelMesero;
                    gvMesas.DataBind();
                }
                else
                {
                    //empleado no cargado en session
                    Response.Redirect("Default.aspx");
                }
            }

        }

        protected void Accion_Click(object sender, EventArgs e)
        {
            var boton = (System.Web.UI.WebControls.LinkButton)sender;
            string accion = boton.CommandArgument;

            switch (accion)
            {
                case "abrirPed":
                    
                    break;
                case "cancelarPed":
                    
                    break;
                case "cerrarPed":
                    
                    break;
                case "listarInsumos":
                    
                    break;
                default:
                    break;
            }
        }













    }
}


