using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servidor.Modelo.Clases;

namespace Servidor.Modelo.Base_de_datos
{

    public class ClienteDAL
    {
        private ConexionDB conexion = new ConexionDB();
        private SqlCommand comando = new SqlCommand();
        SqlDataReader reader;

        public List<Localidad> ObtenerLocalidades()
        {
            List<Localidad> listaLocalidades = new List<Localidad>();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.ObtenerLocalidades";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            reader = comando.ExecuteReader();
            while (reader.Read())
            {
                Localidad localidad = new Localidad();
                localidad.Id = reader.GetInt32(0);
                localidad.Nombre = reader.GetString(1);
                localidad.CantidadMesas = reader.GetInt32(2);
                listaLocalidades.Add(localidad);
            }
            
            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return listaLocalidades;
        }

        public int ObtenerCantidadMesasPorLocalidad(int idlocalidad)
        {
            int cantidad = 0;
            List<Localidad> listaLocalidades = new List<Localidad>();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.CantidadMesasDeLocalidad";
            comando.Parameters.AddWithValue("@IdLocalidad", idlocalidad); 
            comando.CommandType = CommandType.StoredProcedure;
            reader = comando.ExecuteReader();
            while (reader.Read())
            {
                
                cantidad = reader.GetInt32(0);

            }

            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return cantidad;
        }

        public int ContarMesasPorLocalidad(int idlocalidad)
        {
            int cantidad = 0;
            List<Localidad> listaLocalidades = new List<Localidad>();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.ContarMesasPorLocalidad";
            comando.Parameters.AddWithValue("@IdLocalidad", idlocalidad);
            comando.CommandType = CommandType.StoredProcedure;
            reader = comando.ExecuteReader();
            while (reader.Read())
            {
                cantidad = reader.GetInt32(0);
            }
            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return cantidad;
        }
        public void RegistrarMesa(int numeroMesa, int idLocalidad)
        {
            SqlCommand cmd = new SqlCommand("RegistrarMesa", conexion.AbrirConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@NumeroMesa", numeroMesa);
            cmd.Parameters.AddWithValue("@IdLocalidad", idLocalidad);
            cmd.Parameters.AddWithValue("@Estado", 1);
            cmd.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

    }
}
