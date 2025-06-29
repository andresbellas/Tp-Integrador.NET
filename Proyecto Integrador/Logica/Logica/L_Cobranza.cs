using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Logica
{
    public class L_Cobranza
    {

        public List<Cobranza> ListarCobranzas()
        {
            List<Cobranza> lista = new List<Cobranza>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta(@"SELECT c.id_cobranza, c.Id_Pago, c.Id_Pedido, c.Total, 
                                           m.Nombre_Pago, m.Descripcion, m.Habilitado
                                    FROM Cobranza c
                                    JOIN MedioDePago m ON c.Id_Pago = m.Id_Pago
                                    WHERE m.Habilitado = 1");

                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Cobranza aux = new Cobranza();
                    aux.id_cobranza = (int)conexion.Lector["id_cobranza"];
                    aux.Id_Pedido = (int)conexion.Lector["Id_Pedido"];
                    aux.Total = Convert.ToSingle(conexion.Lector["Total"]);

                    aux.MedioDePago = new MedioDePago();
                    aux.MedioDePago.Id_Pago = (int)conexion.Lector["Id_Pago"];
                    aux.MedioDePago.Nombre_Pago = (string)conexion.Lector["Nombre_Pago"];
                    aux.MedioDePago.Descripcion = conexion.Lector["Descripcion"] == DBNull.Value ? null : (string)conexion.Lector["Descripcion"];
                    aux.MedioDePago.habilitado = (bool)conexion.Lector["Habilitado"];

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

        public void AgregarCobranza(Cobranza cobranza)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("INSERT INTO Cobranza (Id_Pago, Id_Pedido, Total) VALUES (@IdPago, @IdPedido, @Total)");
                conexion.SetParametros("@IdPago", cobranza.MedioDePago.Id_Pago);
                conexion.SetParametros("@IdPedido", cobranza.Id_Pedido);
                conexion.SetParametros("@Total", cobranza.Total);
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

        public void ModificarCobranza(Cobranza cobranza)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("UPDATE Cobranza SET Id_Pago = @IdPago, Id_Pedido = @IdPedido, Total = @Total WHERE id_cobranza = @IdCobranza");
                conexion.SetParametros("@IdPago", cobranza.MedioDePago.Id_Pago);
                conexion.SetParametros("@IdPedido", cobranza.Id_Pedido);
                conexion.SetParametros("@Total", cobranza.Total);
                conexion.SetParametros("@IdCobranza", cobranza.id_cobranza);
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

        public void EliminarCobranza(int id)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("DELETE FROM Cobranza WHERE id_cobranza = @IdCobranza");
                conexion.SetParametros("@IdCobranza", id);
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
