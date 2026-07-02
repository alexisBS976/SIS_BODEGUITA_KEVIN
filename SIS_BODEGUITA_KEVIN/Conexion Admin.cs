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
            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                string query = "SELECT ISNULL(SUM(total), 0) FROM Ventas WHERE CAST(fecha_venta AS DATE) = CAST(GETDATE() AS DATE)";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    conexion.Open();
                    decimal totalHoy = Convert.ToDecimal(cmd.ExecuteScalar());
                    return totalHoy;
                }
            }
        }

        /// <summary>
        /// Obtiene los 5 productos más vendidos basados en la cantidad total de unidades.
        /// </summary>
        /// <returns>Un objeto DataTable que contiene el ranking de los 5 productos más vendidos.</returns>
        public static DataTable ObtenerTopProductos()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = new SqlConnection(Conexion_BD.Cadena))
            {
                string query = @"SELECT TOP 5 
                            v.id_producto AS [Código Producto], 
                            p.nombre AS [Nombre del Producto], 
                            SUM(cantidad) AS [Total Unidades Vendidas]
                         FROM DetalleVenta v
                         INNER JOIN Productos p ON v.id_producto = p.id_producto
                         GROUP BY v.id_producto, p.nombre
                         ORDER BY SUM(cantidad) DESC";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        adaptador.Fill(tabla);
                        return tabla;
                    }
                }
            }
        }
    }
}
