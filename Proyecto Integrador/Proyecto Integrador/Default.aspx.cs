using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Logica;

namespace Proyecto_Integrador
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;

            }
           

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsername.Text.Trim();
            string contraseña = txtPassword.Text.Trim();
            int idUsuario;

            try
            {
                L_Usuario logicaUsuario = new L_Usuario();

                /* Para pruebaa */
                if (usuario == "gerente" && contraseña == "123")
                {
                    Session["usuario"] = "gerente";
                    Session["idUsuario"] = 0;
                    Session["nombre_rol"] = "gerente";

                    //CUIDADO BRAIAN, CAMBIE QUE EL CERRAR SESSION SEA CON EMPLEADO
                    Response.Redirect("Gerente.aspx");
                    return;
                }
                else if (usuario == "mesero" && contraseña == "123")
                {
                    Session["usuario"] = "mesero";
                    Session["idUsuario"] = 1;
                    Session["nombre_rol"] = "mesero";


                    Response.Redirect("Mesero.aspx");
                    return;
                }


                
                if (logicaUsuario.Login(usuario, contraseña, out idUsuario))
                {

                    Session["usuario"] = usuario;
                  
                   
                    //Busco el empleado por el ID usuario
                    L_Empleados logica = new L_Empleados();
                    Empleados empleado = new Empleados();
                    empleado = logica.EmpleadoPorIdUsuario(idUsuario);

                    Session["empleado"] = empleado;

                    if (empleado.RolEmpleado.Nombre_rol.ToUpper() == "GERENTE")
                    {
                        Response.Redirect("Gerente.aspx");
                    }
                    else if(empleado.RolEmpleado.Nombre_rol.ToUpper() == "MESERO")
                    {
                        Response.Redirect("Mesero.aspx");
                    }
                    else
                    {
                        lblError.Text = "El rol " + empleado.RolEmpleado.Nombre_rol.ToUpper() + " no es valido";
                    }
                    
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error inesperado al iniciar sesión: " + ex.Message;
            }
        }
    }
}