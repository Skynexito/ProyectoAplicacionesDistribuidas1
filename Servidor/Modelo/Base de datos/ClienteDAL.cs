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

        public bool MesaEstaActiva(int numeroMesa, int idLocalidad)
        {
            SqlCommand cmd = new SqlCommand("SELECT Estado FROM MESA WHERE NumeroMesa = @Numero AND IdLocalidad = @Loc", conexion.AbrirConexion());
            cmd.Parameters.AddWithValue("@Numero", numeroMesa);
            cmd.Parameters.AddWithValue("@Loc", idLocalidad);

            object result = cmd.ExecuteScalar();
            conexion.CerrarConexion();
            return result != null && Convert.ToBoolean(result);
        }

        public void InsertarOActualizarVoto(int numeroMesa, int idLocalidad, int idOpcion, int cantidad)
        {
            SqlCommand getMesaId = new SqlCommand("SELECT Id FROM MESA WHERE NumeroMesa = @Num AND IdLocalidad = @Loc", conexion.AbrirConexion());
            getMesaId.Parameters.AddWithValue("@Num", numeroMesa);
            getMesaId.Parameters.AddWithValue("@Loc", idLocalidad);
            int idMesa = Convert.ToInt32(getMesaId.ExecuteScalar());
            conexion.CerrarConexion();

            SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM VOTOS WHERE IdMesa = @IdMesa AND IdOpcion = @IdOpcion", conexion.AbrirConexion());
            check.Parameters.AddWithValue("@IdMesa", idMesa);
            check.Parameters.AddWithValue("@IdOpcion", idOpcion);
            int existe = (int)check.ExecuteScalar();
            conexion.CerrarConexion();

            if (existe > 0)
            {
                SqlCommand update = new SqlCommand("UPDATE VOTOS SET Cantidad = @Cantidad WHERE IdMesa = @IdMesa AND IdOpcion = @IdOpcion", conexion.AbrirConexion());
                update.Parameters.AddWithValue("@Cantidad", cantidad);
                update.Parameters.AddWithValue("@IdMesa", idMesa);
                update.Parameters.AddWithValue("@IdOpcion", idOpcion);
                update.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            else
            {
                SqlCommand insert = new SqlCommand("RegistrarVotos", conexion.AbrirConexion());
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.AddWithValue("@Cantidad", cantidad);
                insert.Parameters.AddWithValue("@NumeroMesa", numeroMesa);
                insert.Parameters.AddWithValue("@IdLocalidad", idLocalidad);
                insert.Parameters.AddWithValue("@IdOpcion", idOpcion);
                insert.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
        }

        public List<Opcion> ObtenerOpciones()
        {
            List<Opcion> lista = new List<Opcion>();

            try
            {
                SqlCommand cmd = new SqlCommand("ObtenerOpciones", conexion.AbrirConexion());
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int id = Convert.ToInt32(dr["Id"]);
                    string nombreCandidato = dr["NombreCandidato"].ToString();

                    Opcion opcion = new Opcion(id, "Lista", nombreCandidato); // Puedes cambiar "Lista" si lo quieres dinámico
                    lista.Add(opcion);
                }

                dr.Close();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                conexion.CerrarConexion();
                throw new Exception("Error al obtener las opciones: " + ex.Message);
            }

            return lista;
        }


    }
}
