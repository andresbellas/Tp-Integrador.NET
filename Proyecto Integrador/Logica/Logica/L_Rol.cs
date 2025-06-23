using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Logica.SQL;



namespace Logica
{
    public class L_Rol
    {
        public List<Rol> ListarRoles()
        {
            List<Rol> lista = new List<Rol>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("SELECT Id_Rol, Nombre_Rol, Descripcion FROM Rol where baja = 0");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Rol aux = new Rol();
                    aux.id_rol = (int)conexion.Lector["Id_Rol"];
                    aux.Nombre_rol = (string)conexion.Lector["Nombre_Rol"];
                    aux.Descripcion = (string)conexion.Lector["Descripcion"];

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

        public void AgregarRol(Rol rol)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("INSERT INTO Rol (Nombre_Rol, Descripcion) VALUES (@Nombre, @Descripcion)");
                conexion.SetParametros("@Nombre", rol.Nombre_rol);
                conexion.SetParametros("@Descripcion", rol.Descripcion);
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

        public void ModificarRol(Rol rol)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("UPDATE Rol SET Nombre_Rol = @Nombre, Descripcion = @Descripcion WHERE Id_Rol = @Id");
                conexion.SetParametros("@Nombre", rol.Nombre_rol);
                conexion.SetParametros("@Descripcion", rol.Descripcion);
                conexion.SetParametros("@Id", rol.id_rol);
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

        public void EliminarRol(int idRol)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("update rol set baja = 1 FROM Rol WHERE Id_Rol = @Id");
                conexion.SetParametros("@Id", idRol);
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
