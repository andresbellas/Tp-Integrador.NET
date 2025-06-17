using Entidades;
using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_ItemPedidos
    {

        public List<ItemPedidos> ListarPorPedido(int idPedido)
        {
            List<ItemPedidos> lista = new List<ItemPedidos>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("SELECT Id_item, Id_Pedido, Sku, Cantidad, Total FROM ItemPedidos WHERE Id_Pedido = @IdPedido");
                conexion.SetParametros("@IdPedido", idPedido);
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    ItemPedidos aux = new ItemPedidos();
                    aux.Id_item = (int)conexion.Lector["Id_item"];
                    aux.Id_Pedido = (int)conexion.Lector["Id_Pedido"];
                    aux.Sku = (int)conexion.Lector["Sku"];
                    aux.Cantidad = (int)conexion.Lector["Cantidad"];
                    aux.Total = (float)Convert.ToDouble(conexion.Lector["Total"]);
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void Agregar(ItemPedidos item)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("INSERT INTO ItemPedidos (Id_Pedido, Sku, Cantidad, Total) VALUES (@Id_Pedido, @Sku, @Cantidad, @Total)");
                conexion.SetParametros("@Id_Pedido", item.Id_Pedido);
                conexion.SetParametros("@Sku", item.Sku);
                conexion.SetParametros("@Cantidad", item.Cantidad);
                conexion.SetParametros("@Total", item.Total);
                conexion.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void Modificar(ItemPedidos item)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("UPDATE ItemPedidos SET Sku = @Sku, Cantidad = @Cantidad, Total = @Total WHERE Id_item = @Id_item AND Id_Pedido = @Id_Pedido");
                conexion.SetParametros("@Sku", item.Sku);
                conexion.SetParametros("@Cantidad", item.Cantidad);
                conexion.SetParametros("@Total", item.Total);
                conexion.SetParametros("@Id_item", item.Id_item);
                conexion.SetParametros("@Id_Pedido", item.Id_Pedido);
                conexion.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void Eliminar(int id_item, int idPedido)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("DELETE FROM ItemPedidos WHERE Id_item = @Id_item AND Id_Pedido = @Id_Pedido");
                conexion.SetParametros("@Id_item", id_item);
                conexion.SetParametros("@Id_Pedido", idPedido);
                conexion.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

    }
}
