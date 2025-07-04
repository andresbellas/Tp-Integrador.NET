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
                conexion.Consulta("SELECT p.Id_pedido, p.Nro_Pedido, p.Fecha_Pedido, p.Id_Estado, m.Id_mesa, m.Nro_Mesa FROM Pedidos p JOIN Mesa m ON p.Id_Mesa = m.Id_mesa");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Pedidos aux = new Pedidos();
                    aux.Id_pedido = (int)conexion.Lector["Id_pedido"];
                    aux.Nro_Pedido = Convert.ToInt32(conexion.Lector["Nro_Pedido"]);
                    aux.Fecha_Pedido = Convert.ToDateTime(conexion.Lector["Fecha_Pedido"]);
                    aux.Id_Estado = (int)conexion.Lector["Id_Estado"];

                    aux.MesaAsignada = new Mesa();
                    aux.MesaAsignada.Id_mesa = (int)conexion.Lector["Id_mesa"];
                    aux.MesaAsignada.Nro_Mesa = Convert.ToInt32(conexion.Lector["Nro_Mesa"]);

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

        public Pedidos Agregar(Pedidos pedido)
        {
            Sql conexion = new Sql();
            try
            {
                // Obtener el próximo número de pedido desde la SEQUENCE
                conexion.Consulta("SELECT NEXT VALUE FOR Seq_NroPedido AS NroPedido");
                conexion.Ejecutar();

                long nroPedidoGenerado = 0;
                if (conexion.Lector.Read())
                {
                    nroPedidoGenerado = Convert.ToInt64(conexion.Lector["NroPedido"]);
                }
                conexion.Lector.Close();

                // Insertar pedido y obtener el Id_Pedido autogenerado
                conexion.Consulta(@"INSERT INTO Pedidos (Nro_Pedido, Fecha_Pedido, Id_Mesa, Id_Estado, Nro_Legajo)
                            OUTPUT INSERTED.Id_Pedido
                            VALUES (@Nro_Pedido, @Fecha_Pedido, @Id_Mesa, @Id_Estado, @Nro_Legajo)");

                conexion.SetParametros("@Nro_Pedido", nroPedidoGenerado);
                conexion.SetParametros("@Fecha_Pedido", pedido.Fecha_Pedido);
                conexion.SetParametros("@Id_Mesa", pedido.MesaAsignada.Id_mesa);
                conexion.SetParametros("@Id_Estado", pedido.Id_Estado);
                conexion.SetParametros("@Nro_Legajo", pedido.Nro_Legajo);  // nuevo parámetro

                int idGenerado = (int)conexion.EjecutarScalar();

                // Actualizar el objeto con los datos generados
                pedido.Id_pedido = idGenerado;
                pedido.Nro_Pedido = nroPedidoGenerado;

                return pedido;
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



        public int ObtenerPedidoActivo(int idMesa)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("SELECT TOP 1 Id_Pedido FROM Pedidos WHERE Id_Mesa = @idMesa AND Id_Estado = 3 ORDER BY Fecha_Pedido DESC");
                conexion.SetParametros("@idMesa", idMesa);
                conexion.Ejecutar();

                if (conexion.Lector.Read())
                {
                    return Convert.ToInt32(conexion.Lector["Id_Pedido"]);
                }
                return 0;
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
