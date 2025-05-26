using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Servidor.Modelo.Base_de_datos;
using System.Windows.Forms;
using Servidor.Modelo.Clases;

namespace Servidor.Modelo.ServidorTCP
{
    public class ServidorTCP
    {
        private TcpListener servidor;
        private bool activo = true;
        public void IniciarServidor()
        {
            try
            {
                servidor = new TcpListener(IPAddress.Any, 6000);
                servidor.Start();
                while (activo)
                {
                    TcpClient cliente = servidor.AcceptTcpClient();
                    Task.Run(() => AtenderCliente(cliente));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar el servidor: " + ex.Message);
            }
        }

        public async Task AtenderCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();

            try
            {
                byte[] buffer = new byte[1024];

                while (cliente.Connected)
                {
                    int leidos = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (leidos == 0)
                        break; // Cliente cerró conexión

                    string comando = Encoding.UTF8.GetString(buffer, 0, leidos).Trim();

                    if (comando == "EnviarLocalidades")
                    {
                        ClienteDAL clienteDAL = new ClienteDAL();
                        List<Localidad> localidades = clienteDAL.ObtenerLocalidades();

                        StringBuilder sb = new StringBuilder();
                        foreach (var loc in localidades)
                            sb.Append($"{loc.Id},{loc.Nombre},{loc.CantidadMesas};");

                        string mensaje = sb.ToString() + "\n";
                        byte[] datos = Encoding.UTF8.GetBytes(mensaje);
                        await stream.WriteAsync(datos, 0, datos.Length);
                    }
                    else if (comando == "EnviarParametrosControl")
                    {
                        ServidorDAL servidor = new ServidorDAL();
                        (int numeroVotantes, DateTime fecha) = servidor.ObtenerDatosControl();

                        string mensaje = $"{numeroVotantes},{fecha:yyyy-MM-dd}\n";
                        byte[] datos = Encoding.UTF8.GetBytes(mensaje);
                        await stream.WriteAsync(datos, 0, datos.Length);
                    }
                    else if (comando.StartsWith("AsignarMesa|"))
                    {
                        string idStr = comando.Split('|')[1];
                        int idLocalidad = int.Parse(idStr);

                        ClienteDAL clienteDAL = new ClienteDAL();

                        int mesasExistentes = clienteDAL.ContarMesasPorLocalidad(idLocalidad);
                        int cantidadMaxima = clienteDAL.ObtenerCantidadMesasPorLocalidad(idLocalidad);

                        if (mesasExistentes < cantidadMaxima)
                        {
                            int nuevoNumeroMesa = mesasExistentes + 1;
                            clienteDAL.RegistrarMesa(nuevoNumeroMesa, idLocalidad);
                            byte[] datos = Encoding.UTF8.GetBytes($"{nuevoNumeroMesa}\n");
                            await stream.WriteAsync(datos, 0, datos.Length);
                        }
                        else
                        {
                            byte[] datos = Encoding.UTF8.GetBytes("0\n");
                            await stream.WriteAsync(datos, 0, datos.Length);
                        }
                    }
                    else
                    {
                        // Mensaje no reconocido, enviar algo opcional
                        string mensaje = "Comando no válido\n";
                        byte[] datos = Encoding.UTF8.GetBytes(mensaje);
                        await stream.WriteAsync(datos, 0, datos.Length);
                    }

                    // Reiniciar buffer por seguridad (opcional)
                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al atender cliente: " + ex.Message);
            }
            finally
            {
                stream.Close();
                cliente.Close();
                Console.WriteLine("Conexión cerrada con cliente.");
            }
        }

    }
}
