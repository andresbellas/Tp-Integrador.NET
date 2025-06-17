using Entidades;
using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_Pedidos
    {

        public List<Pedidos> Listar()
        {
            List<Pedidos> lista = new List<Pedidos>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("SELECT p.Id_pedido, p.Nro_Pedido, p.Fecha_Pedido, p.Id_Estado, m.Id_mesa, m.Nro_Mesa FROM Pedidos p JOIN Mesas m ON p.Id_Mesa = m.Id_mesa");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Pedidos aux = new Pedidos();
                    aux.Id_pedido = (int)conexion.Lector["Id_pedido"];
                    aux.Nro_Pedido = (int)conexion.Lector["Nro_Pedido"];
                    aux.Fecha_Pedido = (DateTime)conexion.Lector["Fecha_Pedido"];
                    aux.Id_Estado = (int)conexion.Lector["Id_Estado"];

                    aux.MesaAsignada = new Mesa();
                    aux.MesaAsignada.Id_mesa = (int)conexion.Lector["Id_mesa"];
                    aux.MesaAsignada.Nro_Mesa = (int)conexion.Lector["Nro_Mesa"];

                    aux.ItemsPedidos = new List<ItemPedidos>();
                    L_ItemPedidos logicaItem = new L_ItemPedidos();
                    aux.ItemsPedidos = logicaItem.ListarPorPedido(aux.Id_pedido);

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

        public void Agregar(Pedidos pedido)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("INSERT INTO Pedidos (Nro_Pedido, Fecha_Pedido, Id_Mesa, Id_Estado) VALUES (@Nro_Pedido, @Fecha_Pedido, @Id_Mesa, @Id_Estado)");
                conexion.SetParametros("@Nro_Pedido", pedido.Nro_Pedido);
                conexion.SetParametros("@Fecha_Pedido", pedido.Fecha_Pedido);
                conexion.SetParametros("@Id_Mesa", pedido.MesaAsignada.Id_mesa);
                conexion.SetParametros("@Id_Estado", pedido.Id_Estado);
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

        public void Modificar(Pedidos pedido)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("UPDATE Pedidos SET Nro_Pedido = @Nro_Pedido, Fecha_Pedido = @Fecha_Pedido, Id_Mesa = @Id_Mesa, Id_Estado = @Id_Estado WHERE Id_pedido = @Id_pedido");
                conexion.SetParametros("@Nro_Pedido", pedido.Nro_Pedido);
                conexion.SetParametros("@Fecha_Pedido", pedido.Fecha_Pedido);
                conexion.SetParametros("@Id_Mesa", pedido.MesaAsignada.Id_mesa);
                conexion.SetParametros("@Id_Estado", pedido.Id_Estado);
                conexion.SetParametros("@Id_pedido", pedido.Id_pedido);
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

        public void Eliminar(int idPedido)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("DELETE FROM Pedidos WHERE Id_pedido = @Id_pedido");
                conexion.SetParametros("@Id_pedido", idPedido);
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
