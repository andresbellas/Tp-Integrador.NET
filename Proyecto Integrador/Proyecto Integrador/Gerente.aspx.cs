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

    }
}

