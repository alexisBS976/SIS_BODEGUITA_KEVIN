using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos local
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SIS_BODEGUITA_KEVIN
{
    public class Conexion_Admin
    {
        /// <summary>
        /// Obtiene el total recaudado por ventas en el día actual.
        /// </summary>
        /// <returns>Un valor decimal que representa la suma total de las ventas de hoy.</returns>
        public static decimal ObtenerVentasDelDia()
        {
            // Inicializa la variable que almacenará el total de las ventas
            decimal totalHoy = 0;

            // Se crea y configura la conexión a la base de datos utilizando la cadena de conexión global
            SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena);
            conexion.Open();

            // Consulta SQL: Suma la columna 'total' de la tabla 'Ventas'
            // Filtra comparando solo la parte de la fecha (sin horas) con la fecha actual del servidor (GETDATE)
            // ISNULL asegura que si no hay ventas hoy, devuelva 0 en lugar de un valor NULL
            string query = "SELECT ISNULL(SUM(total), 0) FROM Ventas WHERE CAST(fecha_venta AS DATE) = CAST(GETDATE() AS DATE)";

            // Se prepara el comando con la consulta y la conexión activa
            SqlCommand cmd = new SqlCommand(query, conexion);

            // ExecuteScalar se usa porque la consulta devuelve un único valor (una fila, una columna)
            totalHoy = Convert.ToDecimal(cmd.ExecuteScalar());

            conexion.Close();
            return totalHoy;
        }

        /// <summary>
        /// Obtiene los 5 productos más vendidos basados en la cantidad total de unidades.
        /// </summary>
        /// <returns>Un objeto DataTable que contiene el ranking de los 5 productos más vendidos.</returns>
        public static DataTable ObtenerTopProductos()
        {
            // Se crea la estructura en memoria que almacenará los resultados de la consulta
            DataTable tabla = new DataTable();

            // Se establece y abre la conexión a la base de datos
            SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena);
            conexion.Open();

            // Consulta SQL estructurada con @ para permitir múltiples líneas:
            // 1. TOP 5: Limita el resultado a los primeros 5 registros.
            // 2. INNER JOIN: Une la tabla de detalles con la de productos usando el ID para obtener el nombre.
            // 3. GROUP BY: Agrupa por producto para poder sumar sus cantidades correspondientes.
            // 4. ORDER BY DESC: Ordena de mayor a menor según la suma de unidades vendidas.
            string query = @"SELECT TOP 5 
                            v.id_producto AS [Código Producto], 
                            p.nombre AS [Nombre del Producto], 
                            SUM(cantidad) AS [Total Unidades Vendidas]
                         FROM DetalleVenta v
                         INNER JOIN Productos p ON v.id_producto = p.id_producto
                         GROUP BY v.id_producto, p.nombre
                         ORDER BY SUM(cantidad) DESC";

            // Se crea el comando SQL
            SqlCommand cmd = new SqlCommand(query, conexion);

            // El SqlDataAdapter actúa como puente para extraer los datos de la BD y llenar el DataTable
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            adaptador.Fill(tabla); // Llena el DataTable con los registros devueltos
            conexion.Close();

            return tabla;
        }
    }
}
