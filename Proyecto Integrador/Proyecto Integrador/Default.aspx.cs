using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;

namespace Proyecto_Integrador
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsername.Text.Trim();
            string contraseña = txtPassword.Text.Trim();
            int idUsuario;

            try
            {
                L_Usuario logicaUsuario = new L_Usuario();

                if (usuario == "gerente" && contraseña == "123")
                {
                    Session["usuario"] = "gerente";
                    Session["idUsuario"] = 0;
                    Session["nombre_rol"] = "gerente";


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
                    Session["idUsuario"] = idUsuario;

                    // Acá podrías validar el rol y redirigir según corresponda
                    Response.Redirect("Gerente.aspx");
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