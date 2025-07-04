﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.SQL
{
    internal class Sql
    {

        private SqlConnection conexion = new SqlConnection(); // instanciamos la conexion a la base
        private SqlCommand comando = new SqlCommand();  // instanciamos el comando para ejecutar las query
        private SqlDataReader lector; // No se instancia

        public SqlDataReader Lector
        {
            get { return lector; } // Generamos el get para leer desde afuera el lector ya que es private
        }

        //conexion a la base
        public Sql()
        {
            try
            {

                string nombreEquipo = Environment.MachineName;
                if (nombreEquipo == "ULARIAGA-BRAIAN")
                {
                    //Conexion para base de datos con declaracion de usuario especifico con login de sql
                    conexion.ConnectionString = "Server=ULARIAGA-BRAIAN\\LOCALHOST; Database= Restaurante; User Id= sa; Password=Super123.adm "; /// Brian
                }
                else
                {

                    //Conexion para base de datos local con login de wind
                    conexion.ConnectionString = "Server=.\\SQLEXPRESS;  Database= Restaurante; integrated security= true"; /// Andres & Nico

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        //Metodo para la consulta
        public void Consulta(string consulta)
        {
            try
            {
                comando.Parameters.Clear(); // Limpiar parámetros anteriores
                comando.CommandType = System.Data.CommandType.Text; // Tipo de comando a ajecutar en el SQL, text(query), storeproceduire(SP)
                comando.CommandText = consulta; //Pasamos la consulta por parametro

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        //Metodo para ejecutar consulta
        public void Ejecutar()
        {

            comando.Connection = conexion; //Nos conectamos

            try
            {
                conexion.Open(); // abrimos conexion
                lector = comando.ExecuteReader(CommandBehavior.CloseConnection); // Leemos el reusltado del query

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public void EjecutarAccion()
        {

            comando.Connection = conexion; //Nos conectamos

            try
            {
                conexion.Open(); // abrimos conexion
                comando.ExecuteNonQuery(); // Leemos el reusltado del query sin guardarlo

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conexion.Close();
            }


        }

        public void cerrarConexion()
        {

            try
            {

                if (lector != null)
                {
                    lector.Close();
                }
                conexion.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conexion != null && conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }

            }
        }

        public void SetParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public object EjecutarScalar()
        {
            comando.Connection = conexion;

            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

    }
}
