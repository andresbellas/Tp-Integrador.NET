﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

namespace Proyecto_Integrador
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                // Ocultar header en la página de login
                string currentPage = System.IO.Path.GetFileName(Request.Path);
                if (currentPage.ToLower() == "default.aspx")
                {
                    pnlHeader.Visible = false;
                }
                else
                {
                    // Mostrar solo si hay sesión
                    if (Session["usuario"] != null)
                    {

                        Empleados Logueado = (Empleados)Session["empleado"];
                        lblUsuario.Text = "Hola, " + Logueado.Nombre.ToString();
                        pnlHeader.Visible = true;
                        // Obtener el rol y mostrar imagen correspondiente
                        string rol = Session["nombre_rol"] as string ?? "default";

                        switch (rol.ToLower())
                        {
                            case "mesero":
                                imgUsuario.ImageUrl = "~/Icons/UserMesero.png";
                                break;
                            case "gerente":
                                imgUsuario.ImageUrl = "~/Icons/UserGerente.png";
                                break;
                            default:
                                imgUsuario.ImageUrl = "~/Icons/UserDefault.svg";
                                break;
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}