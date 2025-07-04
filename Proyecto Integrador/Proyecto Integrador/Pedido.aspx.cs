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
    public partial class Pedido : System.Web.UI.Page
    {
        // Ejemplo para almacenar el pedido actual (puede ser Session o DB)
        private int IdMesa;
        private int IdPedido;
        L_Insumos l_Insumos = new L_Insumos();
        L_ItemPedidos l_ItemPedidos = new L_ItemPedidos();
        L_Pedidos l_Pedidos = new L_Pedidos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validación de sesión de mesa
                Mesa mesa = Session["MesaSeleccionada"] as Mesa;
                if (mesa == null)
                {
                    RedirigirAlInicio();
                    return;
                }

                // Guardar y mostrar mesa
                IdMesa = mesa.Id_mesa;
                Session["IdMesa"] = IdMesa;
                lblNroMesa.Text = mesa.Nro_Mesa.ToString();

                // Obtener empleado (mesero o gerente)
                Empleados empleadoActual = Session["empleado"] as Empleados;
                if (empleadoActual == null)
                {
                    RedirigirAlInicio();
                    return;
                }

                lblNombreMesero.Text = $"{empleadoActual.Nombre} {empleadoActual.Apellido}";

                // Guardar legajo (si no está ya en sesión)
                if (Session["nroLegajo"] == null)
                    Session["nroLegajo"] = empleadoActual.Nro_Legajo;

                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

                // Abrir o gestionar pedido
                string modo = Request.QueryString["modo"];
                L_Pedidos logicaPedidos = new L_Pedidos();

                if (modo == "abrir")
                {
                    IdPedido = CrearPedidoNuevo(IdMesa, empleadoActual.Nro_Legajo);
                }
                else if (modo == "gestionar")
                {
                    IdPedido = logicaPedidos.ObtenerPedidoActivo(IdMesa);
                    if (IdPedido == 0)
                    {
                        int nroLegajo = empleadoActual.Nro_Legajo; // asegurate que tenés el legajo del empleado actual
                        IdPedido = CrearPedidoNuevo(IdMesa, nroLegajo);
                    }
                }
                else
                {
                    RedirigirAlInicio();
                    return;
                }

                Session["IdPedido"] = IdPedido;

                CargarInsumosDisponibles();
                CargarItemsPedido();
            }
            else
            {
                // Postback: recuperar valores
                if (Session["IdPedido"] != null)
                    IdPedido = (int)Session["IdPedido"];

                if (Session["IdMesa"] != null)
                    IdMesa = (int)Session["IdMesa"];
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

            List<ItemPedidos> items = l_ItemPedidos.ListarPorPedido(IdPedido);

            gvItemsPedido.DataSource = items;
            gvItemsPedido.DataBind();
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
            TextBox txtCantidad = (TextBox)fila.Cells[2].Controls[0]; // Asegúrate que sea la columna de cantidad

            if (int.TryParse(txtCantidad.Text, out int nuevaCantidad) && nuevaCantidad > 0)
            {
                string sku = fila.Cells[0].Text;

                // 1. Obtener la cantidad actual desde BD
                ItemPedidos itemActual = l_ItemPedidos.ObtenerPorId(idItem); // Tenés que tener este método en tu lógica
                int cantidadAnterior = itemActual.Cantidad;

                int diferencia = nuevaCantidad - cantidadAnterior;

                // 2. Si se quiere aumentar la cantidad, validamos el stock disponible
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

                // 3. Obtener precio actual del insumo
                double precio = l_Insumos.ObtenerPrecioInsumo(sku);
                float nuevoTotal = (float)(nuevaCantidad * precio);

                // 4. Actualizar ítem
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

                    // 5. Actualizar stock:
                    if (diferencia != 0)
                        l_Insumos.ActualizarStockInsumo(sku, -diferencia); // si diferencia < 0, suma stock

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
            // Verificar stock suficiente antes de agregar
            if (!haystock)
            {
                lblMensaje.Text = "No hay stock suficiente para el insumo seleccionado.";
                lblMensaje.Visible = true;
                return;
            }

            // Obtener el precio actual del insumo para calcular total
            double precio = l_Insumos.ObtenerPrecioInsumo(skuSeleccionado);
            if (precio <= 0)
            {
                lblMensaje.Text = "Error al obtener el precio del insumo.";
                lblMensaje.Visible = true;
                return;
            }

            // Crear el item a agregar
            ItemPedidos nuevoItem = new ItemPedidos
            {
                Id_Pedido = IdPedido,
                Sku = skuSeleccionado,
                Cantidad = cantidad,
                Total = (float)(cantidad * precio)
            };

            try
            {
                // Agregar item al pedido
                l_ItemPedidos.Agregar(nuevoItem);

                // Actualizar stock en base de datos (restar la cantidad)
                l_Insumos.ActualizarStockInsumo(skuSeleccionado, cantidad);

                // Recargar lista y limpiar controles
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
            Pedidos nuevo = new Pedidos();
            nuevo.Fecha_Pedido = DateTime.Now;
            nuevo.MesaAsignada = new Mesa { Id_mesa = idMesa };
            nuevo.Id_Estado = 3; // Estado Activo
            nuevo.Nro_Legajo = nroLegajo; // asigno el legajo

            Pedidos pedidoCreado = l_Pedidos.Agregar(nuevo); // Devuelve el objeto completo

            // Cambiar estado de la mesa
            L_Mesa l_mesa = new L_Mesa();
            l_mesa.CambiarEstadoMesa(idMesa, 2); // Estado Ocupado

            return pedidoCreado.Id_pedido;
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Empleados empleado = Session["empleado"] as Empleados;
            if (empleado != null)
            {
                string rol = empleado.RolEmpleado.Nombre_rol.ToLower();

                if (rol == "gerente")
                {
                    Response.Redirect("Gerente.aspx");
                }
                else if (rol == "mesero")
                {
                    Response.Redirect("Mesero.aspx");
                }
                else
                {
                    // Por si hay otro rol, redirigir a una página por defecto
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                // Si no hay empleado en sesión, también redirigir a login o página default
                Response.Redirect("Default.aspx");
            }
        }

    }

}
