using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Logica
{
    public class L_Usuario
    {

        public List<Usuarios> ListarUsuarios()
        {
            List<Usuarios> lista = new List<Usuarios>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("SELECT Id_Usuario, Usuario, Contraseña FROM Usuarios");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Usuarios aux = new Usuarios();
                    aux.Id_Usuario = (int)conexion.Lector["Id_Usuario"];
                    aux.Usuario = (string)conexion.Lector["Usuario"];
                    aux.Contraseña = (string)conexion.Lector["Contraseña"];

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

        //public void AgregarUsuario(Usuarios usuario)
        //{
        //    Sql conexion = new Sql();
        //    try
        //    {
        //        conexion.Consulta("INSERT INTO Usuarios (Usuario, Contraseña) VALUES (@Usuario, @Contraseña)");
        //        conexion.SetParametros("@Usuario", usuario.Usuario);
        //        conexion.SetParametros("@Contraseña", usuario.Contraseña);
        //        conexion.EjecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        conexion.cerrarConexion();
        //    }
        //}

        public int AgregarUsuario(Usuarios usuario)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("INSERT INTO Usuarios (Usuario, Contraseña) VALUES (@Usuario, @Contraseña); SELECT SCOPE_IDENTITY();");
                conexion.SetParametros("@Usuario", usuario.Usuario);
                conexion.SetParametros("@Contraseña", usuario.Contraseña);

                object resultado = conexion.EjecutarScalar(); // Usamos Scalar para obtener el ID
                return Convert.ToInt32(resultado);
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


        public void ModificarUsuario(Usuarios usuario)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("UPDATE Usuarios SET Usuario = @Usuario, Contraseña = @Contraseña WHERE Id_Usuario = @Id");
                conexion.SetParametros("@Usuario", usuario.Usuario);
                conexion.SetParametros("@Contraseña", usuario.Contraseña);
                conexion.SetParametros("@Id", usuario.Id_Usuario);
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

        public void EliminarUsuario(int idUsuario)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("DELETE FROM Usuarios WHERE Id_Usuario = @Id");
                conexion.SetParametros("@Id", idUsuario);
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
