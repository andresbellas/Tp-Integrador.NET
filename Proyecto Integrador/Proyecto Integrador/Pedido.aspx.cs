using Entidades;
using Logica;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_Integrador
{
    public partial class Pedido : System.Web.UI.Page
    {
        private int IdMesa;
        private int IdPedido;
        L_Insumos l_Insumos = new L_Insumos();
        L_ItemPedidos l_ItemPedidos = new L_ItemPedidos();
        L_Pedidos l_Pedidos = new L_Pedidos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Mesa mesa = Session["MesaSeleccionada"] as Mesa;
                if (mesa == null)
                {
                    RedirigirAlInicio();
                    return;
                }

                IdMesa = mesa.Id_mesa;
                Session["IdMesa"] = IdMesa;
                lblNroMesa.Text = mesa.Nro_Mesa.ToString();

                Empleados empleadoActual = Session["empleado"] as Empleados;
                if (empleadoActual == null)
                {
                    RedirigirAlInicio();
                    return;
                }

                lblNombreMesero.Text = $"{empleadoActual.Nombre} {empleadoActual.Apellido}";

                if (Session["nroLegajo"] == null)
                    Session["nroLegajo"] = empleadoActual.Nro_Legajo;

                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

                // buscar si ya hay pedido activo
                L_Pedidos logicaPedidos = new L_Pedidos();
                IdPedido = logicaPedidos.ObtenerPedidoActivo(IdMesa);

                if (IdPedido != 0)
                {
                    Session["IdPedido"] = IdPedido;
                    btnCancelarPedido.Visible = true;
                }
                else
                {
                    btnCancelarPedido.Visible = false;
                }

                CargarInsumosDisponibles();
                CargarItemsPedido();
            }
            else
            {
                if (Session["IdPedido"] != null)
                    IdPedido = (int)Session["IdPedido"];

                if (Session["IdMesa"] != null)
                    IdMesa = (int)Session["IdMesa"];

                btnCancelarPedido.Visible = Session["IdPedido"] != null;
            }
        }

        private void RedirigirAlInicio()
        {
            Response.Redirect("Default.aspx");
        }

        private void CargarInsumosDisponibles()
        {
            List<Insumos> insumos = l_Insumos.ListarInsumos();
            ddlInsumos.DataSource = insumos;
            ddlInsumos.DataTextField = "Nombre";
            ddlInsumos.DataValueField = "Sku";
            ddlInsumos.DataBind();
            ddlInsumos.Items.Insert(0, new ListItem("--Seleccione un insumo--", "0"));
        }

        private void CargarItemsPedido()
        {
            if (IdPedido != 0)
            {
                List<ItemPedidos> items = l_ItemPedidos.ListarPorPedido(IdPedido);
                gvItemsPedido.DataSource = items;
                gvItemsPedido.DataBind();
            }
            else
            {
                gvItemsPedido.DataSource = null;
                gvItemsPedido.DataBind();
            }
        }

        protected void gvItemsPedido_RowEdit(object sender, GridViewEditEventArgs e)
        {
            gvItemsPedido.EditIndex = e.NewEditIndex;
            CargarItemsPedido();
        }

        protected void gvItemsPedido_RowCancel(object sender, GridViewCancelEditEventArgs e)
        {
            gvItemsPedido.EditIndex = -1;
            CargarItemsPedido();
        }

        protected void gvItemsPedido_RowUpdate(object sender, GridViewUpdateEventArgs e)
        {
            int idItem = Convert.ToInt32(gvItemsPedido.DataKeys[e.RowIndex].Value);
            GridViewRow fila = gvItemsPedido.Rows[e.RowIndex];
            TextBox txtCantidad = (TextBox)fila.Cells[2].Controls[0];

            if (int.TryParse(txtCantidad.Text, out int nuevaCantidad) && nuevaCantidad > 0)
            {
                string sku = fila.Cells[0].Text;

                ItemPedidos itemActual = l_ItemPedidos.ObtenerPorId(idItem);
                int cantidadAnterior = itemActual.Cantidad;
                int diferencia = nuevaCantidad - cantidadAnterior;

                if (diferencia > 0)
                {
                    bool hayStock = l_Insumos.HayStockSuficiente(sku, diferencia);
                    if (!hayStock)
                    {
                        lblMensaje.Text = "No hay stock suficiente para aumentar la cantidad.";
                        lblMensaje.Visible = true;
                        return;
                    }
                }

                double precio = l_Insumos.ObtenerPrecioInsumo(sku);
                float nuevoTotal = (float)(nuevaCantidad * precio);

                ItemPedidos itemModificado = new ItemPedidos
                {
                    Id_item = idItem,
                    Id_Pedido = IdPedido,
                    Sku = sku,
                    Cantidad = nuevaCantidad,
                    Total = nuevoTotal
                };

                try
                {
                    l_ItemPedidos.Modificar(itemModificado);

                    if (diferencia != 0)
                        l_Insumos.ActualizarStockInsumo(sku, -diferencia);

                    gvItemsPedido.EditIndex = -1;
                    CargarItemsPedido();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al actualizar el ítem: " + ex.Message;
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                lblMensaje.Text = "La cantidad ingresada no es válida.";
                lblMensaje.Visible = true;
            }
        }


        protected void gvItemsPedido_RowDelete(object sender, GridViewDeleteEventArgs e)
        {
            int idItem = Convert.ToInt32(gvItemsPedido.DataKeys[e.RowIndex].Value);

            try
            {
                // 1) obtener el item antes de borrarlo
                ItemPedidos item = l_ItemPedidos.ObtenerPorId(idItem);

                // 2) devolver stock
                l_Insumos.ActualizarStockInsumo(item.Sku, -item.Cantidad);

                // 3) eliminar el ítem
                l_ItemPedidos.Eliminar(idItem, IdPedido);

                CargarItemsPedido();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el insumo del pedido: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }




        protected void btnAgregarInsumo_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (ddlInsumos.SelectedValue == "0")
            {
                lblMensaje.Text = "Seleccione un insumo válido.";
                lblMensaje.Visible = true;
                return;
            }

            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                lblMensaje.Text = "Ingrese una cantidad válida mayor a 0.";
                lblMensaje.Visible = true;
                return;
            }

            string skuSeleccionado = ddlInsumos.SelectedValue;
            bool haystock = l_Insumos.HayStockSuficiente(skuSeleccionado, cantidad);

            if (!haystock)
            {
                lblMensaje.Text = "No hay stock suficiente para el insumo seleccionado.";
                lblMensaje.Visible = true;
                return;
            }

            double precio = l_Insumos.ObtenerPrecioInsumo(skuSeleccionado);
            if (precio <= 0)
            {
                lblMensaje.Text = "Error al obtener el precio del insumo.";
                lblMensaje.Visible = true;
                return;
            }

            // Crear el pedido si no existe todavía
            if (Session["IdPedido"] == null)
            {
                int nroLegajo = (int)Session["nroLegajo"];
                IdPedido = CrearPedidoNuevo(IdMesa, nroLegajo);
                Session["IdPedido"] = IdPedido;
                btnCancelarPedido.Visible = true;
            }
            else
            {
                IdPedido = (int)Session["IdPedido"];
            }

            ItemPedidos nuevoItem = new ItemPedidos
            {
                Id_Pedido = IdPedido,
                Sku = skuSeleccionado,
                Cantidad = cantidad,
                Total = (float)(cantidad * precio)
            };

            try
            {
                l_ItemPedidos.Agregar(nuevoItem);
                l_Insumos.ActualizarStockInsumo(skuSeleccionado, cantidad);
                CargarItemsPedido();
                ddlInsumos.SelectedIndex = 0;
                txtCantidad.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar el insumo al pedido: " + ex.Message;
                lblMensaje.Visible = true;
            }
        }

        private int CrearPedidoNuevo(int idMesa, int nroLegajo)
        {
            Pedidos nuevo = new Pedidos
            {
                Fecha_Pedido = DateTime.Now,
                MesaAsignada = new Mesa { Id_mesa = idMesa },
                Id_Estado = 3, // Activo
                Nro_Legajo = nroLegajo
            };

            Pedidos pedidoCreado = l_Pedidos.Agregar(nuevo);

            L_Mesa l_mesa = new L_Mesa();
            l_mesa.CambiarEstadoMesa(idMesa, 2); // Ocupado

            return pedidoCreado.Id_pedido;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Empleados empleado = Session["empleado"] as Empleados;
            if (empleado != null)
            {
                string rol = empleado.RolEmpleado.Nombre_rol.ToLower();
                if (rol == "gerente")
                    Response.Redirect("Gerente.aspx");
                else if (rol == "mesero")
                    Response.Redirect("Mesero.aspx");
                else
                    Response.Redirect("Default.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }



        protected void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            if (Session["IdPedido"] != null)
            {
                int idPedido = (int)Session["IdPedido"];

                try
                {
                    
                    List<ItemPedidos> items = l_ItemPedidos.ListarPorPedido(idPedido);

                   
                    foreach (ItemPedidos item in items)
                    {
                        l_Insumos.ActualizarStockInsumo(item.Sku, -item.Cantidad);
                    }

                   
                    foreach (ItemPedidos item in items)
                    {
                        l_ItemPedidos.Eliminar(item.Id_item, idPedido);
                    }

                   
                    l_Pedidos.Eliminar(idPedido);

                    
                    if (Session["IdMesa"] != null)
                    {
                        int idMesa = (int)Session["IdMesa"];
                        L_Mesa l_mesa = new L_Mesa();
                        l_mesa.CambiarEstadoMesa(idMesa, 1);
                    }

                    Session.Remove("IdPedido");
                    btnCancelarPedido.Visible = false;

                    Empleados empleadoActual = Session["empleado"] as Empleados;

                    if (empleadoActual.RolEmpleado.Nombre_rol.ToUpper() == "MESERO")
                        Response.Redirect("Mesero.aspx", false);
                    else if (empleadoActual.RolEmpleado.Nombre_rol.ToUpper() == "GERENTE")
                        Response.Redirect("Gerente.aspx", false);
                    else
                        Response.Redirect("Default.aspx", false);
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al cancelar el pedido: " + ex.Message;
                    lblMensaje.Visible = true;
                }
            }
        }









    }
}