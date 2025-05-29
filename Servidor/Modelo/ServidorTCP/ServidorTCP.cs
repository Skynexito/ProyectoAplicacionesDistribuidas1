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
using System.IO;
using System.Runtime.InteropServices.ComTypes;

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
                        EnviarLocalidades(stream, cliente);
                    }
                    else if (comando == "EnviarParametrosControl")
                    {
                        EnviarParametrosControl(stream, cliente);
                    }
                    else if (comando.StartsWith("AsignarMesa|"))
                    {
                        EnviarAsignacionMesa(comando, stream, cliente);
                    }
                    else if (comando.StartsWith("RegistrarVotos|"))
                    {
                        RegistrarOpcionesVoto(comando, stream, cliente);
                    }
                    else if (comando == "EnviarOpciones")
                    {
                        EnviarOpcionesVoto(stream, cliente);
                    }
                    else if (comando.StartsWith("CerrarMesa|"))
                    {
                        CerrarMesa(comando, stream, cliente);
                    }
                    else if (comando == "PING") // <-- Aquí agregamos el manejo del PING
                    {
                        string respuesta = "PONG\n";
                        byte[] datos = Encoding.UTF8.GetBytes(respuesta);
                        await stream.WriteAsync(datos, 0, datos.Length);
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


        private async void EnviarLocalidades(NetworkStream stream, TcpClient cliente)
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

        private async void EnviarParametrosControl(NetworkStream stream,TcpClient cliente)
        {
            ServidorDAL servidor = new ServidorDAL();
            (int numeroVotantes, DateTime fecha) = servidor.ObtenerDatosControl();

            string mensaje = $"{numeroVotantes},{fecha:yyyy-MM-dd}\n";
            byte[] datos = Encoding.UTF8.GetBytes(mensaje);
            await stream.WriteAsync(datos, 0, datos.Length);
        }

        private async void EnviarAsignacionMesa(string comando, NetworkStream stream, TcpClient cliente)
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

        private async void EnviarOpcionesVoto(NetworkStream stream, TcpClient cliente)
        {
            ClienteDAL clienteDAL = new ClienteDAL();
            List<Opcion> opciones = clienteDAL.ObtenerOpciones(); // método que llama al procedimiento ObtenerOpciones

            StringBuilder sb = new StringBuilder();
            foreach (var o in opciones)
            {
                sb.Append($"{o.Id},{o.Candidato};");
            }

            byte[] datos = Encoding.UTF8.GetBytes(sb.ToString() + "\n");
            await stream.WriteAsync(datos, 0, datos.Length);
        }

        private async void RegistrarOpcionesVoto(string comando, NetworkStream stream, TcpClient cliente)
        {
            try
            {
                // Extraer los datos del comando
                string datos = comando.Substring("RegistrarVotos|".Length);
                string[] partes = datos.Split('|');

                if (partes.Length < 2)
                {
                    string errorMsg = "Comando incompleto\n";
                    byte[] errorBytes = Encoding.UTF8.GetBytes(errorMsg);
                    await stream.WriteAsync(errorBytes, 0, errorBytes.Length);
                    return;
                }

                string[] mesaData = partes[0].Split(','); // formato: NumeroMesa,IdLocalidad
                int numeroMesa = int.Parse(mesaData[0]);
                int idLocalidad = int.Parse(mesaData[1]);

                string[] votosPartes = partes[1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                ClienteDAL clienteDAL = new ClienteDAL();

                int idMesa = clienteDAL.ObtenerIdMesa(numeroMesa, idLocalidad);

                if (idMesa == 0)
                {
                    byte[] datosNoExiste = Encoding.UTF8.GetBytes("Mesa no encontrada\n");
                    await stream.WriteAsync(datosNoExiste, 0, datosNoExiste.Length);
                    return;
                }

                // Validar si la mesa está activa
                if (!clienteDAL.MesaEstaActiva(numeroMesa, idLocalidad))
                {
                    byte[] datosError = Encoding.UTF8.GetBytes("Mesa no activa\n");
                    await stream.WriteAsync(datosError, 0, datosError.Length);
                    return;
                }

                // Insertar o actualizar cada voto
                foreach (string voto in votosPartes)
                {
                    string[] campos = voto.Split(',');
                    int idOpcion = int.Parse(campos[0]);
                    int cantidad = int.Parse(campos[1]);

                    clienteDAL.InsertarOActualizarVoto(numeroMesa, idLocalidad, idOpcion, cantidad);
                }

                // Confirmar al cliente
                byte[] datosOK = Encoding.UTF8.GetBytes("OK\n");
                await stream.WriteAsync(datosOK, 0, datosOK.Length);
            }
            catch (Exception ex)
            {
                string msg = "ERROR: " + ex.Message + "\n";
                byte[] errorBytes = Encoding.UTF8.GetBytes(msg);
                await stream.WriteAsync(errorBytes, 0, errorBytes.Length);
            }
        }
         private async void CerrarMesa(string comando, NetworkStream stream, TcpClient cliente)
        {
            try
            {
                string datos = comando.Substring("CerrarMesa|".Length);
                string[] partes = datos.Split(',');

                if (partes.Length != 2)
                {
                    byte[] errorFormato = Encoding.UTF8.GetBytes("Formato inválido\n");
                    await stream.WriteAsync(errorFormato, 0, errorFormato.Length);
                    return;
                }

                //MessageBox.Show("Datos recibidos para CerrarMesa: " + datos);
                //MessageBox.Show("Parte 0: " + partes[0]);
                //MessageBox.Show("Parte 1: " + partes[1]);

                int numeroMesa = int.Parse(partes[0]);
                int idLocalidad = int.Parse(partes[1]);

                ClienteDAL clienteDAL = new ClienteDAL();
                int idMesa = clienteDAL.ObtenerIdMesa(numeroMesa, idLocalidad);

                if (idMesa == 0)
                {
                    byte[] noExiste = Encoding.UTF8.GetBytes("2\n"); // Mesa no existe
                    await stream.WriteAsync(noExiste, 0, noExiste.Length);
                    return;
                }

                if (!clienteDAL.MesaEstaActiva(numeroMesa, idLocalidad))
                {
                    byte[] yaCerrada = Encoding.UTF8.GetBytes("0\n"); // Ya estaba cerrada
                    await stream.WriteAsync(yaCerrada, 0, yaCerrada.Length);
                    return;
                }

                clienteDAL.CerrarMesa(idMesa); // Actualiza el estado en la base a 0 (cerrada)

                byte[] cerrada = Encoding.UTF8.GetBytes("1\n"); // Cerrada correctamente
                await stream.WriteAsync(cerrada, 0, cerrada.Length);
            }
            catch (Exception ex)
            {
                byte[] error = Encoding.UTF8.GetBytes("ERROR: " + ex.Message + "\n");
                await stream.WriteAsync(error, 0, error.Length);
            }
        }


    }
}
