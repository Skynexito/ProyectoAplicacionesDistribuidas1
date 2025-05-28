using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using Servidor.Modelo.Clases;

namespace Servidor.Modelo.Base_de_datos
{
    public class ServidorDAL
    {
        private ConexionDB conexion = new ConexionDB();
        private SqlCommand comando = new SqlCommand();

        public void registrarCantidadMesasPorLocalidad(int cantidad)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.ActualizarCantidadVotantesPorMesa";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Cantidad", cantidad);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void ActualizarFechaEleccion(DateTime fecha)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.ActualizarFechaEleccion";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Fecha", fecha);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void registrarLocalidad(Localidad localidad) {
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.RegistrarLocalidad";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Nombre", localidad.Nombre);
            comando.Parameters.AddWithValue("@CantidadMesas", localidad.CantidadMesas);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public (int CantidadVotantes, DateTime FechaEleccion) ObtenerDatosControl()
        {
            var datos = (CantidadVotantes: 0, FechaEleccion: DateTime.MinValue);

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.ObtenerControlElecciones";
            comando.CommandType = CommandType.StoredProcedure;

            using (var reader = comando.ExecuteReader())
            {
                if (reader.Read())
                {
                    datos.CantidadVotantes = reader.GetInt32(0);
                    datos.FechaEleccion = reader.GetDateTime(1);
                }
            }

            comando.Parameters.Clear();
            conexion.CerrarConexion();

            return datos;

        }

        public List<(string Nombre, int Cantidad)> ObtenerResumenVotos(int? idLocalidad = null, int? numeroMesa = null)
        {
            List<(string, int)> resultado = new List<(string, int)>();
            SqlCommand cmd = new SqlCommand("ObtenerResumenVotos", conexion.AbrirConexion());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdLocalidad", idLocalidad.HasValue ? (object)idLocalidad.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@NumeroMesa", numeroMesa.HasValue ? (object)numeroMesa.Value : DBNull.Value);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string nombre = reader["NombreOpcion"].ToString();
                int votos = Convert.ToInt32(reader["TotalVotos"]);
                resultado.Add((nombre, votos));
            }

            reader.Close();
            conexion.CerrarConexion();
            return resultado;
        }



    }

}
