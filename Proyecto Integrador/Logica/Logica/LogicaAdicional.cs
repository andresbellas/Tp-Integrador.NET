using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica.SQL;

namespace Logica
{
    public class LogicaAdicional
    {
        public void DesasignarLegajoEnMesa(int idMesa, int nroLegajo)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta(@"UPDATE Mesa 
                            SET Nro_Legajo = NULL 
                            WHERE Id_Mesa = @Id_Mesa AND Nro_Legajo = @Nro_Legajo");
                conexion.SetParametros("@Id_Mesa", idMesa);
                conexion.SetParametros("@Nro_Legajo", nroLegajo);
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


        public void AsignarLegajoAMesa(int idMesa, int nroLegajo)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta(@"UPDATE Mesa
                                    SET Nro_Legajo = @Nro_Legajo_Nuevo
                                    WHERE Id_Mesa = @Id_Mesa");
                conexion.SetParametros("@Nro_Legajo_Nuevo", nroLegajo);
                conexion.SetParametros("@Id_Mesa", idMesa);
                conexion.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al intentar asignar el legajo {nroLegajo} a la mesa {idMesa}: " + ex.Message, ex);
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }














    }
}
