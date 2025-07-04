using Entidades;
using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logica
{
    public class L_Empleados
    {

        public List<Empleados> ListarEmpleados()
        {
            List<Empleados> lista = new List<Empleados>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta(@"
            SELECT e.Id_Empleado, e.Nro_Legajo, e.Nombre, e.Apellido, e.Id_Usuario, e.Id_Rol, e.Baja, 
                   u.Usuario AS NombreUsuario, u.Contraseña, 
                   r.Nombre_Rol, r.Descripcion 
            FROM Empleados e 
            JOIN Usuarios u ON e.Id_Usuario = u.Id_Usuario 
            JOIN Rol r ON e.Id_Rol = r.Id_Rol
            WHERE e.Baja = 0 AND r.Baja = 0
        ");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Empleados aux = new Empleados();
                    aux.id_empleado = (int)conexion.Lector["Id_Empleado"];
                    aux.Nro_Legajo = Convert.ToInt32(conexion.Lector["Nro_Legajo"]);
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Apellido = (string)conexion.Lector["Apellido"];
                    aux.baja = (bool)conexion.Lector["Baja"];

                    aux.UsuarioEmpleado = new Usuarios
                    {
                        Id_Usuario = (int)conexion.Lector["Id_Usuario"],
                        Usuario = (string)conexion.Lector["NombreUsuario"],
                        Contraseña = (string)conexion.Lector["Contraseña"]
                    };

                    aux.RolEmpleado = new Rol
                    {
                        id_rol = (int)conexion.Lector["Id_Rol"],
                        Nombre_rol = (string)conexion.Lector["Nombre_Rol"],
                        Descripcion = (string)conexion.Lector["Descripcion"]
                    };

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

        public void AgregarEmpleado(Empleados empleado)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("INSERT INTO Empleados (Nro_Legajo, Nombre, Apellido, Id_Usuario, Id_Rol, Baja) VALUES (@Nro_Legajo, @Nombre, @Apellido, @Id_Usuario, @Id_Rol, @Baja)");
                conexion.SetParametros("@Nro_Legajo", empleado.Nro_Legajo);
                conexion.SetParametros("@Nombre", empleado.Nombre);
                conexion.SetParametros("@Apellido", empleado.Apellido);
                conexion.SetParametros("@Id_Usuario", empleado.UsuarioEmpleado.Id_Usuario);
                conexion.SetParametros("@Id_Rol", empleado.RolEmpleado.id_rol);
                conexion.SetParametros("@Baja", empleado.baja);
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

        public void ModificarEmpleado(Empleados empleado)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("UPDATE Empleados SET Nro_Legajo = @Nro_Legajo, Nombre = @Nombre, Apellido = @Apellido, Id_Usuario = @Id_Usuario, Id_Rol = @Id_Rol, Baja = @Baja WHERE Id_Empleado = @Id_Empleado");
                conexion.SetParametros("@Id_Empleado", empleado.id_empleado);
                conexion.SetParametros("@Nro_Legajo", empleado.Nro_Legajo);
                conexion.SetParametros("@Nombre", empleado.Nombre);
                conexion.SetParametros("@Apellido", empleado.Apellido);
                conexion.SetParametros("@Id_Usuario", empleado.UsuarioEmpleado.Id_Usuario);
                conexion.SetParametros("@Id_Rol", empleado.RolEmpleado.id_rol);
                conexion.SetParametros("@Baja", empleado.baja);
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

        public void EliminarEmpleado(int id)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("DELETE FROM Empleados WHERE Id_Empleado = @Id");
                conexion.SetParametros("@Id", id);
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

        public void BajaLogicaEmpleado(int id)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("UPDATE Empleados SET Baja = 1 WHERE Id_Empleado = @Id");
                conexion.SetParametros("@Id", id);
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




        public Empleados EmpleadoPorIdUsuario(int id_usuario)
        {
            Empleados aux = null;
            Sql sql = new Sql();

            try
            {
                sql.Consulta(@"SELECT e.Id_Empleado, e.Nro_Legajo, e.Nombre, e.Apellido, e.Baja,
                              e.Id_Usuario, e.Id_Rol, u.Usuario AS NombreUsuario, u.Contraseña,
                              r.Nombre_Rol, r.Descripcion
                       FROM Empleados e
                       JOIN Usuarios u ON e.Id_Usuario = u.Id_Usuario
                       JOIN Rol r ON e.Id_Rol = r.Id_Rol
                       WHERE e.Id_Usuario = @Id_Usuario");

                sql.SetParametros("@Id_Usuario", id_usuario);
                sql.Ejecutar();

                if (sql.Lector.Read())
                {
                    aux = new Empleados();
                    aux.id_empleado = (int)sql.Lector["Id_Empleado"];
                    aux.Nro_Legajo = Convert.ToInt32(sql.Lector["Nro_Legajo"]);
                    aux.Nombre = (string)sql.Lector["Nombre"];
                    aux.Apellido = (string)sql.Lector["Apellido"];
                    aux.baja = (bool)sql.Lector["Baja"];

                    aux.UsuarioEmpleado = new Usuarios
                    {
                        Id_Usuario = (int)sql.Lector["Id_Usuario"],
                        Usuario = (string)sql.Lector["NombreUsuario"],
                        Contraseña = (string)sql.Lector["Contraseña"]
                    };

                    aux.RolEmpleado = new Rol
                    {
                        id_rol = (int)sql.Lector["Id_Rol"],
                        Nombre_rol = (string)sql.Lector["Nombre_Rol"],
                        Descripcion = (string)sql.Lector["Descripcion"]
                    };
                }

                return aux;
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



        public Empleados EmpleadoPorLegajo(int nroLegajo)
        {
            Empleados aux = null;
            Sql sql = new Sql();

            try
            {
                sql.Consulta(@"SELECT e.Id_Empleado, e.Nro_Legajo, e.Nombre, e.Apellido, e.Baja,
                               e.Id_Usuario, e.Id_Rol, u.Usuario AS NombreUsuario, u.Contraseña,
                               r.Nombre_Rol, r.Descripcion
                        FROM Empleados e
                        JOIN Usuarios u ON e.Id_Usuario = u.Id_Usuario
                        JOIN Rol r ON e.Id_Rol = r.Id_Rol
                        WHERE e.Nro_Legajo = @Nro_Legajo");

                sql.SetParametros("@Nro_Legajo", nroLegajo);
                sql.Ejecutar();

                if (sql.Lector.Read())
                {
                    aux = new Empleados();
                    aux.id_empleado = (int)sql.Lector["Id_Empleado"];
                    aux.Nro_Legajo = Convert.ToInt32(sql.Lector["Nro_Legajo"]);
                    aux.Nombre = (string)sql.Lector["Nombre"];
                    aux.Apellido = (string)sql.Lector["Apellido"];
                    aux.baja = (bool)sql.Lector["Baja"];

                    aux.UsuarioEmpleado = new Usuarios
                    {
                        Id_Usuario = (int)sql.Lector["Id_Usuario"],
                        Usuario = (string)sql.Lector["NombreUsuario"],
                        Contraseña = (string)sql.Lector["Contraseña"]
                    };

                    aux.RolEmpleado = new Rol
                    {
                        id_rol = (int)sql.Lector["Id_Rol"],
                        Nombre_rol = (string)sql.Lector["Nombre_Rol"],
                        Descripcion = (string)sql.Lector["Descripcion"]
                    };
                }

                return aux;
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




        public bool ExiseEmpleado(int nroLegajo)
        {
            Sql sql = new Sql();
            bool found = false; // Initialize to false

            try
            {
                // We only need to check for existence, so selecting a single column is enough
                sql.Consulta(@"SELECT 1 FROM Empleados WHERE Nro_Legajo = @Nro_Legajo");
                sql.SetParametros("@Nro_Legajo", nroLegajo);
                sql.Ejecutar(); // Execute the query

                // If Lector.Read() returns true, it means a row was found
                if (sql.Lector.Read())
                {
                    found = true;
                }
            }
            catch (Exception ex)
            {
                // Re-throw the exception or log it if necessary
                throw new Exception("Error al verificar empleado por legajo: " + ex.Message, ex);
            }
            finally
            {
                sql.cerrarConexion(); // Always ensure the connection is closed
            }

            return found; // Return true if found, false otherwise
        }



        public List<Empleados> ListarMeserosDisponibles()
        {
            List<Empleados> meseros = new List<Empleados>();
            Sql sql = new Sql();

            try
            {
                sql.Consulta(@"SELECT e.Id_Empleado, e.Nro_Legajo, e.Nombre, e.Apellido, e.Id_Usuario, e.Id_Rol, e.Baja, 
                   u.Usuario AS NombreUsuario, u.Contraseña, 
                   r.Nombre_Rol, r.Descripcion
                   FROM Empleados e
                   JOIN Usuarios u ON e.Id_Usuario = u.Id_Usuario
                   JOIN Rol r ON e.Id_Rol = r.Id_Rol
                   WHERE r.Nombre_Rol = 'mesero'
                   AND e.Baja = 0
                   AND (SELECT COUNT(*) FROM Mesa m WHERE m.Nro_Legajo = e.Nro_Legajo ) < 5");

                sql.Ejecutar();
                while (sql.Lector.Read())
                {
                    Empleados emp = new Empleados();

                    emp.Nro_Legajo = Convert.ToInt32(sql.Lector["Nro_Legajo"]);
                    emp.Nombre = $"{sql.Lector["Nombre"]} {sql.Lector["Apellido"]}";

                    meseros.Add(emp);
                }


                return meseros;
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
