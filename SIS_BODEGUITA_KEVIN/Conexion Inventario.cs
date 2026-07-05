using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;

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
            using (SqliteConnection conexion = new SqliteConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                string query = "SELECT stock_actual FROM Productos WHERE nombre = @nom";
                using (SqliteCommand cmd = new SqliteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nom", nombreProducto);

                    using (SqliteDataReader lector = cmd.ExecuteReader())
                    {
                        int stock = 0;

                        if (lector.Read())
                        {
                            stock = Convert.ToInt32(lector["stock_actual"]);
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
            using (SqliteConnection conexion = new SqliteConnection(Conexion_BD.Cadena))
            {
                conexion.Open();

                string querySelect = "SELECT stock_actual FROM Productos WHERE nombre = @nom";
                using (SqliteCommand cmdSelect = new SqliteCommand(querySelect, conexion))
                {
                    cmdSelect.Parameters.AddWithValue("@nom", nombreProducto);

                    int stockActual = 0;
                    bool productoExiste = false;

                    using (SqliteDataReader lector = cmdSelect.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            stockActual = Convert.ToInt32(lector["stock_actual"]);
                            productoExiste = true;
                        }
                    } // El lector se cierra automáticamente aquí

                    if (productoExiste)
                    {
                        int nuevoStockTotal = stockActual + cantidadNueva;
                        string queryUpdate = "UPDATE Productos SET stock_actual = @nuevoStock WHERE nombre = @nom";

                        using (SqliteCommand cmdUpdate = new SqliteCommand(queryUpdate, conexion))
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