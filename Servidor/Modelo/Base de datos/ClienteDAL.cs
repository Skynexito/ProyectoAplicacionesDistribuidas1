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

        /** 
         * Este método obtiene todas las localidades de la base de datos utilizando el procedimiento almacenado 'ObtenerLocalidades', 
         * lee los resultados y los convierte en objetos Localidad, que luego agrega a una lista y retorna.
         */
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

        /**
         * Este método recibe el ID de una localidad y devuelve la cantidad de mesas asociadas a esa localidad, 
         * ejecutando el procedimiento almacenado 'CantidadMesasDeLocalidad' y leyendo el resultado del primer campo del resultado.
         */
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

        /**
         * Este método cuenta la cantidad de mesas para una localidad específica usando el procedimiento almacenado 'ContarMesasPorLocalidad', 
         * y retorna el valor leído del primer campo del resultado.
         */
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

        /**
         * Este método registra una nueva mesa en la base de datos, asociándola a una localidad específica, 
         * utilizando el procedimiento almacenado 'RegistrarMesa' y estableciendo el estado inicial de la mesa como activa (1).
         */
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

        /**
         * Este método verifica si una mesa específica está activa en una localidad, 
         * ejecutando el procedimiento almacenado 'VerificarEstadoMesa' y devolviendo el valor booleano correspondiente al estado.
         */
        public bool MesaEstaActiva(int numeroMesa, int idLocalidad)
        {
            bool estado = false;
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "dbo.VerificarEstadoMesa";
            comando.Parameters.AddWithValue("@NumeroMesa", numeroMesa);
            comando.Parameters.AddWithValue("@IdLocalidad", idLocalidad);
            comando.CommandType = CommandType.StoredProcedure;
            reader = comando.ExecuteReader();
            while (reader.Read())
            {
                estado = reader.GetBoolean(0); // ✅ si el campo es tipo BIT
            }
            comando.Parameters.Clear();
            conexion.CerrarConexion();
            return estado;
        }

        /**
         * Este método obtiene el ID de una mesa específica a partir de su número y localidad, 
         * ejecutando el procedimiento almacenado 'ObtenerIdMesa' y devolviendo el resultado como entero.
         */
        public int ObtenerIdMesa(int numeroMesa, int idLocalidad)
        {
            SqlCommand getMesaId = new SqlCommand("dbo.ObtenerIdMesa", conexion.AbrirConexion());
            getMesaId.CommandType = CommandType.StoredProcedure; // ✅ esto es obligatorio
            getMesaId.Parameters.AddWithValue("@NumeroMesa", numeroMesa);
            getMesaId.Parameters.AddWithValue("@IdLocalidad", idLocalidad);
            int idMesa = Convert.ToInt32(getMesaId.ExecuteScalar());
            getMesaId.Parameters.Clear();
            conexion.CerrarConexion();
            return idMesa;
        }

        /**
         * Este método inserta o actualiza la cantidad de votos para una opción en una mesa específica; 
         * primero verifica si ya existe un registro de votos para esa mesa y opción, 
         * si existe actualiza la cantidad, de lo contrario inserta un nuevo registro utilizando los procedimientos almacenados correspondientes.
         */
        public void InsertarOActualizarVoto(int numeroMesa, int idLocalidad, int idOpcion, int cantidad)
        {
            int idMesa = ObtenerIdMesa(numeroMesa, idLocalidad);

            SqlCommand check = new SqlCommand("dbo.ExisteVoto", conexion.AbrirConexion());
            check.CommandType = CommandType.StoredProcedure; // ✅ esto es obligatorio
            check.Parameters.AddWithValue("@IdMesa", idMesa);
            check.Parameters.AddWithValue("@IdOpcion", idOpcion);
            int existe = (int)check.ExecuteScalar();
            check.Parameters.Clear();
            conexion.CerrarConexion();

            if (existe > 0)
            {
                SqlCommand update = new SqlCommand("dbo.ActualizarVotos", conexion.AbrirConexion());
                update.CommandType = CommandType.StoredProcedure;
                update.Parameters.AddWithValue("@Cantidad", cantidad);
                update.Parameters.AddWithValue("@IdMesa", idMesa);
                update.Parameters.AddWithValue("@IdOpcion", idOpcion);
                update.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
            else
            {
                SqlCommand insert = new SqlCommand("dbo.RegistrarVotos", conexion.AbrirConexion());
                insert.CommandType = CommandType.StoredProcedure;
                insert.Parameters.AddWithValue("@Cantidad", cantidad);
                insert.Parameters.AddWithValue("@NumeroMesa", numeroMesa);
                insert.Parameters.AddWithValue("@IdLocalidad", idLocalidad);
                insert.Parameters.AddWithValue("@IdOpcion", idOpcion);
                insert.ExecuteNonQuery();
                conexion.CerrarConexion();
            }
        }

        /**
         * Este método cierra una mesa específica en la base de datos, 
         * ejecutando el procedimiento almacenado 'CerrarMesa' con el ID de la mesa correspondiente.
         */
        public void CerrarMesa(int idMesa)
        {
            SqlCommand cmd = new SqlCommand("dbo.CerrarMesa", conexion.AbrirConexion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", idMesa);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            conexion.CerrarConexion();
        }

        /**
         * Este método obtiene la lista de opciones (por ejemplo, candidatos) desde la base de datos 
         * utilizando el procedimiento almacenado 'ObtenerOpciones', construye objetos Opcion con los datos obtenidos y los retorna en una lista.
         */
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
