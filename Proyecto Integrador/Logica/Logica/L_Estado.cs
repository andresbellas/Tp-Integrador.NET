using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Logica
{
    public class L_Estado
    {

        public List<Estado> ListarEstados()
        {
            List<Estado> lista = new List<Estado>();
            Sql sql = new Sql();

            try
            {
                sql.Consulta("SELECT Id_Estado, Nombre_Estado, Descripcion FROM Estado");
                sql.Ejecutar();

                while (sql.Lector.Read())
                {
                    Estado estado = new Estado
                    {
                        Id_Estado = (int)sql.Lector["Id_Estado"],
                        Nombre_Estado = sql.Lector["Nombre_Estado"].ToString(),
                        Descripcion = sql.Lector["Descripcion"] != DBNull.Value ? sql.Lector["Descripcion"].ToString() : null
                    };

                    lista.Add(estado);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql.cerrarConexion();
            }
        }
    }
}
