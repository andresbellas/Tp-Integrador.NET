using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_Usuario
    {

        public bool Login(string usuario, string contraseña, out int idUsuario)
        {
            idUsuario = 0;

            try
            {
                Sql sql = new Sql();
                sql.Consulta("SELECT Id_Usuario FROM Usuarios WHERE Usuario = @Usuario AND Contraseña = @Contraseña");
                sql.SetParametros("@Usuario", usuario);
                sql.SetParametros("@Contraseña", contraseña);
                sql.Ejecutar();

                if (sql.Lector.Read())
                {
                    idUsuario = (int)sql.Lector["Id_Usuario"];
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Sql sql = new Sql();
                sql.cerrarConexion();
            }
        }
    }
}
