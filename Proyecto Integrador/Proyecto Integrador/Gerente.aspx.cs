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
                    // Alta insumo
                    break;
                case "modificacionIns":
                    // Modificación insumo
                    break;

                case "altaRol":
                    Response.Redirect("ABM_Roles.aspx?modo=altaRol");
                    break;
                case "modificacionRol":
                    Response.Redirect("ABM_Roles.aspx?modo=modificacionRol");
                    break;
                case "eliminarRol":
                    Response.Redirect("ABM_Roles.aspx?modo=eliminarRol");
                    break;



                case "altaMedPago":
                    // Alta medio de pago
                    break;
                case "modificacionMedPago":
                    // Modificación medio de pago
                    break;
                case "eliminarMedPago":
                    // Eliminar medio de pago
                    break;

                default:
                    break;
            }
        }

    }
}

