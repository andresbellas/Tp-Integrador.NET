using Entidades;
using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_MedioDePago
    {

        public List<MedioDePago> ListarMedios()
        {
            List<MedioDePago> lista = new List<MedioDePago>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("SELECT Id_Pago, Nombre_Pago, Descripcion, Habilitado FROM MedioDePago WHERE Habilitado = 1");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    MedioDePago aux = new MedioDePago();
                    aux.Id_Pago = (int)conexion.Lector["Id_Pago"];
                    aux.Nombre_Pago = (string)conexion.Lector["Nombre_Pago"];
                    aux.Descripcion = conexion.Lector["Descripcion"] == DBNull.Value ? null : (string)conexion.Lector["Descripcion"];
                    aux.habilitado = (bool)conexion.Lector["Habilitado"];
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

        public void AgregarMedio(MedioDePago medio)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("INSERT INTO MedioDePago (Nombre_Pago, Descripcion, Habilitado) VALUES (@Nombre, @Descripcion, @Habilitado)");
                conexion.SetParametros("@Nombre", medio.Nombre_Pago);
                conexion.SetParametros("@Descripcion", medio.Descripcion ?? (object)DBNull.Value);
                conexion.SetParametros("@Habilitado", medio.habilitado);
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

        public void ModificarMedio(MedioDePago medio)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("UPDATE MedioDePago SET Nombre_Pago = @Nombre, Descripcion = @Descripcion, Habilitado = @Habilitado WHERE Id_Pago = @Id");
                conexion.SetParametros("@Nombre", medio.Nombre_Pago);
                conexion.SetParametros("@Descripcion", medio.Descripcion ?? (object)DBNull.Value);
                conexion.SetParametros("@Habilitado", medio.habilitado);
                conexion.SetParametros("@Id", medio.Id_Pago);
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

        public void EliminarMedioLogico(int idMedio)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("UPDATE MedioDePago SET Habilitado = 0 WHERE Id_Pago = @Id");
                conexion.SetParametros("@Id", idMedio);
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
