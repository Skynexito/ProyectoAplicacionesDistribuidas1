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


    }
}
