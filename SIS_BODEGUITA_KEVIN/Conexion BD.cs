using System;
using System.Collections.Generic;
using System.Text;

namespace SIS_BODEGUITA_KEVIN
{
    public class Conexion_BD
    {
        /// <summary>
        /// Clase centralizada para gestionar la conexión a la base de datos local
        /// </summary>
        public static string Cadena =
       @"Data Source=(LocalDB)\MSSQLLocalDB;
        AttachDbFilename=C:\Users\alebr\Downloads\pruebas\SIS_BODEGUITA_KEVIN\BD_BodeguitaKevin.mdf;
        Integrated Security=True;
        Connect Timeout=30;";
    }
}
