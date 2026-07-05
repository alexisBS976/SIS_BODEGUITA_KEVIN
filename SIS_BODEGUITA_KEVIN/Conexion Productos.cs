using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SIS_BODEGUITA_KEVIN
{
    public class Conexion_Productos
    {
        /// <summary>
        /// Consulta la base de datos y extrae una lista con los nombres de todos los productos.
        /// </summary>
        public static List<string> ObtenerNombresProductos()
        {
            List<string> listaProductos = new List<string>();
            string query = "SELECT nombre FROM Productos";

            using (SqliteConnection conexion = new SqliteConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                using (SqliteCommand cmd = new SqliteCommand(query, conexion))
                {
                    using (SqliteDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            string nombreProd = lector["nombre"]?.ToString() ?? "";
                            if (!string.IsNullOrEmpty(nombreProd))
                            {
                                listaProductos.Add(nombreProd);
                            }
                        }
                    }
                }
            }

            return listaProductos;
        }

        /// <summary>
        /// Obtiene el precio de un producto específico dado su nombre.
        /// </summary>
        public static decimal ObtenerPrecioProducto(string nombreProducto)
        {
            decimal precio = 0;

            using (SqliteConnection conexion = new SqliteConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                using (SqliteCommand cmd = new SqliteCommand("SELECT precio FROM Productos WHERE nombre = @nombre", conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreProducto);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        precio = Convert.ToDecimal(resultado);
                    }
                }
            }

            return precio;
        }
    }
}