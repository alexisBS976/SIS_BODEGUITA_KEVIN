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
            // Se inicializa y abre la conexión con la base de datos
            SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena);
            conexion.Open();

            // Consulta SQL parametrizada para evitar inyecciones SQL y buscar por nombre
            string query = "SELECT * FROM Productos WHERE nombre = @nom";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@nom", nombreProducto);

            // Se ejecuta el lector para recorrer los resultados obtenidos
            SqlDataReader lector = cmd.ExecuteReader();
            int stock = 0;

            // Si se encuentra el registro, se extrae el valor de la cuarta columna (índice 3), correspondiente al stock
            if (lector.Read())
            {
                stock = Convert.ToInt32(lector[3]);
            }

            // Cierre de lector y conexión para liberar recursos de la base de datos
            lector.Close();
            conexion.Close();
            return stock;
        }

        /// <summary>
        /// Incrementa el stock de un producto sumando una nueva cantidad al inventario existente.
        /// </summary>
        public static void SurtirMercaderia(string nombreProducto, int cantidadNueva)
        {
            // Se establece y abre la conexión a la base de datos
            SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena);
            conexion.Open();

            // Consulta inicial para recuperar los datos actuales del producto antes de actualizar
            string querySelect = "SELECT * FROM Productos WHERE nombre = @nom";
            SqlCommand cmdSelect = new SqlCommand(querySelect, conexion);
            cmdSelect.Parameters.AddWithValue("@nom", nombreProducto);

            SqlDataReader lector = cmdSelect.ExecuteReader();
            int stockActual = 0;
            string nombreColumnaCantidad = "";

            // Si el producto existe, obtiene dinámicamente el valor y el nombre de la columna en la posición de índice 3
            if (lector.Read())
            {
                int posicionColumna = 3;
                stockActual = Convert.ToInt32(lector[posicionColumna]);
                nombreColumnaCantidad = lector.GetName(posicionColumna); 
            }
            lector.Close();

            // Si se logró identificar el nombre de la columna, se procede con la actualización del inventario
            if (!string.IsNullOrEmpty(nombreColumnaCantidad))
            {
                int nuevoStockTotal = stockActual + cantidadNueva;

                // Consulta de actualización interpolando el nombre de la columna de manera dinámica
                string queryUpdate = $"UPDATE Productos SET {nombreColumnaCantidad} = @nuevoStock WHERE nombre = @nom";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion);
                cmdUpdate.Parameters.AddWithValue("@nuevoStock", nuevoStockTotal);
                cmdUpdate.Parameters.AddWithValue("@nom", nombreProducto);
                cmdUpdate.ExecuteNonQuery();
            }
            conexion.Close();
        }
    }
}