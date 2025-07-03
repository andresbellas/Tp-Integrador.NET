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
    public partial class InfomormacionMesero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Empleados empleado = Session["MeseroSolicitado"] as Empleados;
                if (empleado != null)
                {
                    txtLegajo.Text = empleado.Nro_Legajo.ToString();
                    txtNombre.Text = empleado.Nombre;
                    txtApellido.Text = empleado.Apellido;
                    txtUsuario.Text = empleado.UsuarioEmpleado.Usuario;
                    txtRol.Text = empleado.RolEmpleado.Nombre_rol;

                    pnlDatosMesero.Visible = true;
                    pnlAsignarMesero.Visible = false;
                }
                else
                {
                    // If no employee, show the assign panel
                    pnlDatosMesero.Visible = false;
                    pnlAsignarMesero.Visible = true;
                   
                }
            }
        }


        protected void btnDesasignar_Click(object sender, EventArgs e)
        {
            Empleados empleado = Session["MeseroSolicitado"] as Empleados;
            Mesa mesa = Session["MesaSeleccionada"] as Mesa;

            if (empleado != null && mesa != null)
            {
                LogicaAdicional logica = new LogicaAdicional();
                logica.DesasignarLegajoEnMesa(mesa.Id_mesa, empleado.Nro_Legajo);

                
                Response.Redirect("Gerente.aspx");
            }
        }



        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            Mesa mesa = Session["MesaSeleccionada"] as Mesa;
            LogicaAdicional logicaAdd = new LogicaAdicional();
            int legajoAsignar;

            if (int.TryParse(txtLegajoAsignar.Text, out legajoAsignar))
            {
                L_Empleados logica = new L_Empleados();
                Empleados empleado = new Empleados();
                
                if (!(logica.ExiseEmpleado(legajoAsignar)))
                {
                    lblMensaje.Text = "Por favor, ingrese un número de legajo válido.";
                    lblMensaje.Visible = true; 

                }
                else
                {

                    logicaAdd.AsignarLegajoAMesa(mesa.Id_mesa, legajoAsignar);
                    Response.Redirect("Gerente.aspx", false);

                }
                 
            }

        }




        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Volver a la página anterior o a la lista
            Response.Redirect("Gerente.aspx");
        }
    }
}