using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos local

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

            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataReader lector = cmd.ExecuteReader())
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
        /// <returns></returns>
        public static decimal ObtenerPrecioProducto(string nombreProducto)
        {
            decimal precio = 0;

            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT precio FROM Productos WHERE nombre = @nombre",
                    conexion);

                cmd.Parameters.AddWithValue("@nombre", nombreProducto);

                object resultado = cmd.ExecuteScalar();

                if (resultado != null && resultado != DBNull.Value)
                    precio = Convert.ToDecimal(resultado);
            }

            return precio;
        }
    }
}