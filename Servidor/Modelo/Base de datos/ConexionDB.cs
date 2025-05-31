using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Servidor.Modelo.Base_de_datos
{
    public class ConexionDB
    {

        // Cadena de conexión a la base de datos SQL Server

        private SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-4RJH76T;Initial Catalog=ProyectoDistri; Integrated Security=True");

        // Métodos para abrir y cerrar la conexión a la base de datos

        public SqlConnection AbrirConexion()
        {
            if (conexion.State == ConnectionState.Closed) conexion.Open(); return conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (conexion.State == ConnectionState.Open) conexion.Close(); return conexion;
        }

    }

}
