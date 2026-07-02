using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos local

namespace SIS_BODEGUITA_KEVIN
{
    public class Conexion_Inventario
    {
        /// <summary>
        /// Consulta el stock disponible de un producto específico mediante su nombre.
        /// </summary>
        /// <returns>La cantidad en stock del producto, o 0 si no se encuentra.</returns>
        public static int ConsultarStockActual(string nombreProducto)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                string query = "SELECT * FROM Productos WHERE nombre = @nom";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nom", nombreProducto);

                    using (SqlDataReader lector = cmd.ExecuteReader())
                    {
                        int stock = 0;

                        if (lector.Read())
                        {
                            stock = Convert.ToInt32(lector[3]);
                        }

                        return stock;
                    }
                }
            }
        }

        /// <summary>
        /// Incrementa el stock de un producto sumando una nueva cantidad al inventario existente.
        /// </summary>
        public static void SurtirMercaderia(string nombreProducto, int cantidadNueva)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                string querySelect = "SELECT * FROM Productos WHERE nombre = @nom";
                using (SqlCommand cmdSelect = new SqlCommand(querySelect, conexion))
                {
                    cmdSelect.Parameters.AddWithValue("@nom", nombreProducto);

                    using (SqlDataReader lector = cmdSelect.ExecuteReader())
                    {
                        int stockActual = 0;
                        string nombreColumnaCantidad = "";

                        if (lector.Read())
                        {
                            int posicionColumna = 3;
                            stockActual = Convert.ToInt32(lector[posicionColumna]);
                            nombreColumnaCantidad = lector.GetName(posicionColumna);
                        }

                        lector.Close();

                        if (!string.IsNullOrEmpty(nombreColumnaCantidad))
                        {
                            int nuevoStockTotal = stockActual + cantidadNueva;

                            string queryUpdate = $"UPDATE Productos SET {nombreColumnaCantidad} = @nuevoStock WHERE nombre = @nom";
                            using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion))
                            {
                                cmdUpdate.Parameters.AddWithValue("@nuevoStock", nuevoStockTotal);
                                cmdUpdate.Parameters.AddWithValue("@nom", nombreProducto);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
    }
}