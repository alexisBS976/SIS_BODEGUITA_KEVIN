using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SIS_BODEGUITA_KEVIN
{
    public class Conexion_Admin
    {
        /// <summary>
        /// Obtiene el total recaudado por ventas en el día actual utilizando funciones de fecha de SQLite.
        /// </summary>
        public static decimal ObtenerVentasDelDia()
        {
            using (SqliteConnection conexion = new SqliteConnection(Conexion_BD.Cadena))
            {
                string query = "SELECT IFNULL(SUM(total), 0) FROM Ventas WHERE date(fecha_venta) = date('now', 'localtime')";

                using (SqliteCommand cmd = new SqliteCommand(query, conexion))
                {
                    conexion.Open();
                    object resultado = cmd.ExecuteScalar();

                    // CORREGIDO: Se cambia el '0UI' por '0M' (sufijo correcto para tipos decimales)
                    return resultado != null ? Convert.ToDecimal(resultado) : 0M;
                }
            }
        }

        /// <summary>
        /// Obtiene los 5 productos más vendidos basados en la cantidad total de unidades (Sintaxis SQLite).
        /// </summary>
        public static DataTable ObtenerTopProductos()
        {
            DataTable tabla = new DataTable();

            using (SqliteConnection conexion = new SqliteConnection(Conexion_BD.Cadena))
            {
                string query = @"SELECT 
                                    v.id_producto AS [Código Producto], 
                                    p.nombre AS [Nombre del Producto], 
                                    SUM(v.cantidad) AS [Total Unidades Vendidas]
                                 FROM DetalleVenta v
                                 INNER JOIN Productos p ON v.id_producto = p.id_producto
                                 GROUP BY v.id_producto, p.nombre
                                 ORDER BY SUM(v.cantidad) DESC
                                 LIMIT 5";

                using (SqliteCommand cmd = new SqliteCommand(query, conexion))
                {
                    conexion.Open();
                    using (SqliteDataReader lector = cmd.ExecuteReader())
                    {
                        tabla.Load(lector);
                        return tabla;
                    }
                }
            }
        }
        public static DataTable ObtenerDetalleVenta()
        {
            DataTable tabla = new DataTable();

            using (SqliteConnection conexion = new SqliteConnection(Conexion_BD.Cadena))
            {
                // Al usar SELECT * evitamos poner nombres de columnas que puedan fallar
                string query = "SELECT * FROM DetalleVenta";

                using (SqliteCommand cmd = new SqliteCommand(query, conexion))
                {
                    conexion.Open();
                    using (SqliteDataReader lector = cmd.ExecuteReader())
                    {
                        tabla.Load(lector);
                        return tabla;
                    }
                }
            }
        }
    }
}