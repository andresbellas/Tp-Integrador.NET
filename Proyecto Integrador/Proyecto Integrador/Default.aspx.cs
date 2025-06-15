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

                if (logicaUsuario.Login(usuario, contraseña, out idUsuario))
                {
                    
                    Session["usuario"] = usuario;
                    Session["idUsuario"] = idUsuario;

                    // Tendria que redirigir a gerente si es un gerente.aspx y a mesero.aspx si es mesero
                    Response.Redirect("");
                    lblError.Text = "Ingreso correcto, en construccion";
                }
                else
                {
                    lblError.Text = "Usuario o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error inesperado al iniciar sesion: " + ex.Message;
            }
        }
    }
}