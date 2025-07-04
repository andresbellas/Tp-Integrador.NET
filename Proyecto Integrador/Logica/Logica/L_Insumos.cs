using Entidades;
using Logica.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_Insumos
    {

        public List<Insumos> ListarInsumos()
        {
            List<Insumos> lista = new List<Insumos>();
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("SELECT Id_Insumos, Sku, Nombre, Precio, Cantidad FROM Insumos");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Insumos aux = new Insumos();
                    aux.Id_insumo = (int)conexion.Lector["Id_Insumos"];
                    aux.Sku = (string)conexion.Lector["Sku"];
                    aux.Nombre = (string)conexion.Lector["Nombre"];
                    aux.Precio = (double)(conexion.Lector["Precio"]);
                    aux.Cantidad = Convert.ToInt32(conexion.Lector["Cantidad"]);

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

        public void AgregarInsumo(Insumos insumo)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("INSERT INTO Insumos (Sku, Nombre, Precio, Cantidad) VALUES (@Sku, @Nombre, @Precio, @Cantidad)");
                conexion.SetParametros("@Sku", insumo.Sku);
                conexion.SetParametros("@Nombre", insumo.Nombre);
                conexion.SetParametros("@Precio", insumo.Precio);
                conexion.SetParametros("@Cantidad", insumo.Cantidad);

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

        public void ModificarInsumo(Insumos insumo)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("UPDATE Insumos SET Sku = @Sku, Nombre = @Nombre, Precio = @Precio, Cantidad = @Cantidad WHERE Id_Insumos = @Id_Insumo");
                conexion.SetParametros("@Sku", insumo.Sku);
                conexion.SetParametros("@Nombre", insumo.Nombre);
                conexion.SetParametros("@Precio", insumo.Precio);
                conexion.SetParametros("@Cantidad", insumo.Cantidad);
                conexion.SetParametros("@Id_Insumo", insumo.Id_insumo);

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

        public void EliminarInsumo(int idInsumo)
        {
            Sql conexion = new Sql();

            try
            {
                conexion.Consulta("DELETE FROM Insumos WHERE Id_Insumos = @Id_Insumo");
                conexion.SetParametros("@Id_Insumo", idInsumo);

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

        public List<Insumos> ListarInsumosDisponibles()
        {
            List<Insumos> lista = new List<Insumos>();
            Sql conexion = new Sql();

            try
            {
                // Traemos solo insumos con stock > 0
                conexion.Consulta("SELECT Id_Insumos, Sku, Nombre, Precio, Cantidad FROM Insumos WHERE Cantidad > 0");
                conexion.Ejecutar();

                while (conexion.Lector.Read())
                {
                    Insumos aux = new Insumos();
                    aux.Id_insumo = (int)conexion.Lector["Id_Insumos"];
                    aux.Sku = conexion.Lector["Sku"].ToString();
                    aux.Nombre = conexion.Lector["Nombre"].ToString();
                    aux.Precio = Convert.ToDouble(conexion.Lector["Precio"]);
                    aux.Cantidad = Convert.ToInt32(conexion.Lector["Cantidad"]);

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


        public bool HayStockSuficiente(string sku, int cantidadSolicitada)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("SELECT Cantidad FROM Insumos WHERE Sku = @Sku");
                conexion.SetParametros("@Sku", sku);
                conexion.Ejecutar();

                if (conexion.Lector.Read())
                {
                    int stockActual = Convert.ToInt32(conexion.Lector["Cantidad"]);
                    return stockActual >= cantidadSolicitada;
                }
                return false;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public double ObtenerPrecioInsumo(string sku)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("SELECT Precio FROM Insumos WHERE Sku = @Sku");
                conexion.SetParametros("@Sku", sku);
                conexion.Ejecutar();

                if (conexion.Lector.Read())
                {
                    return Convert.ToDouble(conexion.Lector["Precio"]);
                }
                return 0;
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }

        public void ActualizarStockInsumo(string sku, int cantidadDescontar)
        {
            Sql conexion = new Sql();
            try
            {
                conexion.Consulta("UPDATE Insumos SET Cantidad = Cantidad - @Cantidad WHERE Sku = @Sku");
                conexion.SetParametros("@Cantidad", cantidadDescontar);
                conexion.SetParametros("@Sku", sku);
                conexion.EjecutarAccion();
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
    }
}
