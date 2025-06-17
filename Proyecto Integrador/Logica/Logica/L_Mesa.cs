using Entidades;
using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Logica
{
    


        public class L_Mesa
        {
            public List<Mesa> ListarMesas()
            {
                List<Mesa> lista = new List<Mesa>();
                Sql conexion = new Sql();

                try
                {
                    conexion.Consulta("SELECT Id_Mesa, Nro_Mesa, Nro_Legajo, Id_Estado FROM Mesa");
                    conexion.Ejecutar();

                    while (conexion.Lector.Read())
                    {
                        Mesa aux = new Mesa();
                        aux.Id_mesa = (int)conexion.Lector["Id_Mesa"];
                    aux.Nro_Mesa = Convert.ToInt32(conexion.Lector["Nro_Mesa"]);
                    aux.Nro_Legajo = conexion.Lector["Nro_Legajo"] == DBNull.Value ? 0 : Convert.ToInt32(conexion.Lector["Nro_Legajo"]);
                    aux.Id_Estado = (int)conexion.Lector["Id_Estado"];

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

            public void AgregarMesa(Mesa mesa)
            {
                Sql conexion = new Sql();

                try
                {
                    conexion.Consulta("INSERT INTO Mesa (Nro_Mesa, Nro_Legajo, Id_Estado) VALUES (@Nro_Mesa, @Nro_Legajo, @Id_Estado)");
                    conexion.SetParametros("@Nro_Mesa", mesa.Nro_Mesa);
                    conexion.SetParametros("@Nro_Legajo", mesa.Nro_Legajo);
                    conexion.SetParametros("@Id_Estado", mesa.Id_Estado);
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

            public void ModificarMesa(Mesa mesa)
            {
                Sql conexion = new Sql();

                try
                {
                    conexion.Consulta("UPDATE Mesa SET Nro_Mesa = @Nro_Mesa, Nro_Legajo = @Nro_Legajo, Id_Estado = @Id_Estado WHERE Id_Mesa = @Id_Mesa");
                    conexion.SetParametros("@Id_Mesa", mesa.Id_mesa);
                    conexion.SetParametros("@Nro_Mesa", mesa.Nro_Mesa);
                    conexion.SetParametros("@Nro_Legajo", mesa.Nro_Legajo);
                    conexion.SetParametros("@Id_Estado", mesa.Id_Estado);
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

            public void EliminarMesa(int id)
            {
                Sql conexion = new Sql();

                try
                {
                    conexion.Consulta("DELETE FROM Mesa WHERE Id_Mesa = @Id");
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



        }
    
}