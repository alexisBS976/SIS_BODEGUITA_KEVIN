using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos local

namespace SIS_BODEGUITA_KEVIN
{
    internal class Conexion_Ventas
    {
        /// <summary>
        /// Genera de forma automática el identificador consecutivo para la siguiente venta.
        /// Cuenta los registros actuales y le suma 1, anteponiendo la letra 'V'.
        /// </summary>
        /// <returns>Una cadena de texto con el formato "V" seguido del número correlativo (ej. "V15").</returns>
        public static string ObtenerSiguienteId()
        {
            // Se inicializa el prefijo por defecto en caso de que la tabla se encuentre vacía
            string nuevoId = "V001";

            // Consulta SQL para obtener el valor máximo actual de la columna id_venta
            string query = "SELECT MAX(id_venta) FROM Ventas";

            // Uso de bloque using para garantizar el cierre automático de la conexión al finalizar
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                // Se inicializa el comando de ejecución junto con la consulta y la conexión establecida
                SqlCommand cmd = new SqlCommand(query, conexion);

                // Se abre de forma explícita la conexión con la base de datos para iniciar la lectura
                conexion.Open();

                // ExecuteScalar devuelve un objeto con la primera columna de la primera fila obtenida
                object resultado = cmd.ExecuteScalar();

                // Condición lógica para verificar que el resultado obtenido de la base de datos no sea nulo
                if (resultado != null && resultado != DBNull.Value)
                {
                    // Se convierte el objeto a cadena usando Convert.ToString para evitar advertencias de nulos
                    string maxId = Convert.ToString(resultado) ?? "";

                    // Validación: Verifica que el texto sea válido e intenta extraer la parte numérica secuencial
                    if (maxId.Length > 1 && int.TryParse(maxId.Substring(1), out int numeroActual))
                    {
                        // Operación aritmética simple para incrementar el índice del registro correlativo
                        int siguienteNumero = numeroActual + 1;

                        // Retorna el prefijo estático concatenado con el número incremental formateado a tres dígitos
                        nuevoId = "V" + siguienteNumero.ToString("D3");
                    }
                }
            }

            // Retorna la cadena de texto final que contiene el identificador único calculado
            return nuevoId;
        }
   
        /// <summary>
        /// Inserta un nuevo registro de venta con sus totales acumulados en la base de datos.
        /// </summary>
        public static void InsertarVenta(string id, decimal total, int cantidad)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                string query = "INSERT INTO Ventas (id_venta, fecha_venta, total, cantidad_productos) VALUES (@id, GETDATE(), @total, @cantidad)";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Obtiene todos los registros de la tabla Ventas estructurados para la generación de reportes.
        /// </summary>
        /// <returns>Un objeto DataTable que contiene el listado histórico de todas las ventas.</returns>
        public static DataTable TraerReporte()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                string query = "SELECT id_venta, fecha_venta, total, cantidad_productos FROM Ventas";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }

        /// <summary>
        /// Consulta el stock actual disponible de un producto específico mediante su nombre.
        /// </summary>
        /// <returns>La cantidad de unidades disponibles en stock. Retorna -1 si el producto no existe.</returns>
        public static int ObtenerStockProducto(string nombreProducto)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                conexion.Open();
                string query = "SELECT stock_actual FROM Productos WHERE nombre = @nom";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nom", nombreProducto);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        return Convert.ToInt32(resultado);
                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// Obtiene el total de ventas realizadas en el día actual.
        /// </summary>
        /// <returns></returns>
        public static decimal ObtenerCierreCajaDiario()
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                string query = @"SELECT ISNULL(SUM(total), 0) 
                         FROM Ventas 
                         WHERE DATEDIFF(day, fecha_venta, GETDATE()) = 0";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    decimal totalCobradoHoy = Convert.ToDecimal(cmd.ExecuteScalar());
                    return totalCobradoHoy;
                }
            }
        }
        public static void InsertarDetalleVenta(string idVenta, string idProducto, int cantidad, decimal subtotal)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                // NOTA: 'id_detalle' no lo ponemos en el INSERT si es IDENTITY (autoincrementable numérico).
                // Si tu id_detalle es un string tipo 'D146', necesitarás generar el ID primero.
                // Asumiendo que es autoincremental en la base de datos:
                string query = @"INSERT INTO DetalleVenta (id_venta, id_producto, cantidad, subtotal) 
                         VALUES (@idVenta, @idProducto, @cantidad, @subtotal)";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idVenta", idVenta);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@subtotal", subtotal);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}