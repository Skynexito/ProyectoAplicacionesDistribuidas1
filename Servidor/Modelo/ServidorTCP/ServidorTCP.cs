using System;
using System.Collections.Generic;
using System.Net.Sockets;   // .sockets sirve para trabajar con TCP/IP
using System.Net;   // Importar las clases necesarias para trabajar con direcciones IP
using System.Text;
using System.Threading.Tasks;   // Importar las clases necesarias para el servidor TCP
using Servidor.Modelo.Base_de_datos;    // Importar las clases de acceso a datos
using Servidor.Modelo.Clases;   // Importar las clases necesarias
using System.IO;

namespace Servidor.Modelo.ServidorTCP
{
    public class ServidorTCP
    {
        private TcpListener servidor;

        private bool activo = true;
        // Evento para notificar mensajes al formulario
        public event Action<string> OnMensajeRecibido;

        // Método para invocar el evento de forma segura
        protected void Informar(string mensaje)
        {
            OnMensajeRecibido?.Invoke(mensaje);
        }
        /* este método se encarga de iniciar el servidor TCP.
         * Crea un TcpListener que escucha en el puerto 6000 y acepta conexiones entrantes.
         * Cada vez que un cliente se conecta, se crea un nuevo hilo para atenderlo.
         * Los comandos recibidos del cliente son procesados y se envían respuestas según corresponda.
         */
        public void IniciarServidor()
        {
            try
            {
                servidor = new TcpListener(IPAddress.Any, 6000);    // Escucha en todas las interfaces de red en el puerto 6000
                servidor.Start();
                Informar("Servidor iniciado, esperando conexiones...");

                while (activo)
                {
                    TcpClient cliente = servidor.AcceptTcpClient();
                    Informar($"Cliente conectado: {cliente.Client.RemoteEndPoint}");    // Notifica que un cliente se ha conectado
                    Task.Run(() => AtenderCliente(cliente));    // Inicia un nuevo hilo para atender al cliente conectado, task.run es una forma de ejecutar código asíncrono en un hilo separado.
                }
            }
            catch (Exception ex)
            {
                // Notificar error, sin usar MessageBox para evitar problemas en hilos secundarios
                Informar("Error al iniciar el servidor: " + ex.Message);
            }
        }
        /* este metodo se encarga de detener el servidor TCP.
         * Cambia el estado del servidor a inactivo y detiene el TcpListener si está activo.
         * Esto permite que el servidor deje de aceptar nuevas conexiones y cierre las existentes.
         */
        public void DetenerServidor()
        {
            activo = false;
            if (servidor != null)
            {
                servidor.Stop();
                servidor = null;
            }
        }

        /* este método se encarga de atender a cada cliente conectado al servidor.
         * Se ejecuta en un hilo separado para permitir múltiples conexiones simultáneas.
         * Lee los comandos enviados por el cliente y responde según el comando recibido.
         */
        public async Task AtenderCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream(); // Obtiene el flujo de red del cliente para leer y escribir datos.

            try
            {
                byte[] buffer = new byte[1024]; // Tamaño del buffer para leer datos del cliente

                while (cliente.Connected)
                {
                    int leidos = await stream.ReadAsync(buffer, 0, buffer.Length);  // Lee datos del cliente de forma asíncrona

                    if (leidos == 0) break;

                    string comando = Encoding.UTF8.GetString(buffer, 0, leidos).Trim(); // Convierte los bytes leídos en una cadena de texto.
                    Informar($"Mensaje recibido de {cliente.Client.RemoteEndPoint}: {comando}");    // Notifica el comando recibido.
                    
                    if (comando.Equals("CLOSE_CONNECTION")) // esto permite al cliente solicitar una desconexión ordenada del servidor.
                    {
                        Informar($"Cliente {cliente.Client.RemoteEndPoint} solicitó desconexión ordenada");
                        break; // Salir del bucle para cerrar conexión
                    }

                    if (comando.Equals("EnviarLocalidades"))
                    {
                        await EnviarLocalidades(stream, cliente);
                    }
                    else if (comando.Equals("EnviarParametrosControl"))
                    {
                        await EnviarParametrosControl(stream, cliente);
                    }
                    else if (comando.StartsWith("AsignarMesa|"))
                    {
                        await EnviarAsignacionMesa(comando, stream, cliente);
                    }
                    else if (comando.StartsWith("RegistrarVotos|"))
                    {
                        await RegistrarOpcionesVoto(comando, stream, cliente);
                    }
                    else if (comando.Equals("EnviarOpciones"))
                    {
                        await EnviarOpcionesVoto(stream, cliente);
                    }
                    else if (comando.StartsWith("CerrarMesa|"))
                    {
                        await CerrarMesa(comando, stream, cliente);
                    }
                    else if (comando.Equals("PING"))    // verifica si el cliente envía un comando PING, lo que indica que está activo y esperando una respuesta.
                    {
                        string respuesta = "PONG\n";
                        byte[] datos = Encoding.UTF8.GetBytes(respuesta);
                        await stream.WriteAsync(datos, 0, datos.Length);
                        Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {respuesta.Trim()}");
                    }
                    else
                    {
                        string mensaje = "Comando no válido\n";
                        byte[] datos = Encoding.UTF8.GetBytes(mensaje);
                        await stream.WriteAsync(datos, 0, datos.Length);
                        Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {mensaje.Trim()}");
                    }

                    Array.Clear(buffer, 0, buffer.Length);  // Limpia el buffer para la próxima lectura
                }
            }
            // esta excepción se lanza cuando el cliente se desconecta inesperadamente, como al cerrar la aplicación o perder la conexión de red.
            catch (IOException ex) when (ex.InnerException is SocketException se && se.SocketErrorCode == SocketError.ConnectionReset)  
            {
                Informar($"Desconexion por el cliente: {cliente.Client.RemoteEndPoint}");   // Notifica que el cliente se ha desconectado inesperadamente.
            }
            catch (Exception ex)
            {
                Informar($"Error al atender cliente {cliente.Client.RemoteEndPoint}: {ex.Message}");    // Notifica cualquier error ocurrido al atender al cliente.
            }
            finally
            {
                stream.Close();
                cliente.Close();
                Informar($"Cliente desconectado: {cliente.Client.RemoteEndPoint}"); // Notifica que el cliente se ha desconectado.
            }
        }
        /* este metodo se encarga de enviar la lista de localidades al cliente.
         * Obtiene las localidades desde la base de datos y las envía en un formato específico.
         * Cada localidad se envía como una cadena con su ID, nombre y cantidad de mesas.
         */
        private async Task EnviarLocalidades(NetworkStream stream, TcpClient cliente)
        {
            ClienteDAL clienteDAL = new ClienteDAL();
            List<Localidad> localidades = clienteDAL.ObtenerLocalidades();

            StringBuilder sb = new StringBuilder(); // Construye el mensaje a enviar al cliente.
            foreach (var loc in localidades)    // Recorre cada localidad y agrega sus datos al mensaje.
                sb.Append($"{loc.Id},{loc.Nombre},{loc.CantidadMesas};");   // Formato: ID,Nombre,CantidadMesas;

            string mensaje = sb.ToString() + "\n";  // Agrega un salto de línea al final del mensaje para indicar el final de los datos.
            byte[] datos = Encoding.UTF8.GetBytes(mensaje); // Convierte el mensaje a bytes para enviarlo al cliente.
            await stream.WriteAsync(datos, 0, datos.Length);    // Envía los datos al cliente de forma asíncrona.
            Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {mensaje.Trim()}");   // Notifica que el mensaje ha sido enviado al cliente.
        }
        /* este método se encarga de enviar los parámetros de control al cliente.
         * Obtiene el número de votantes y la fecha desde la base de datos y los envía en un formato específico.
         * El mensaje enviado contiene el número de votantes y la fecha en formato "yyyy-MM-dd".
         */
        private async Task EnviarParametrosControl(NetworkStream stream, TcpClient cliente)
        {
            ServidorDAL servidor = new ServidorDAL();
            (int numeroVotantes, DateTime fecha) = servidor.ObtenerDatosControl();

            string mensaje = $"{numeroVotantes},{fecha:yyyy-MM-dd}\n";
            byte[] datos = Encoding.UTF8.GetBytes(mensaje);
            await stream.WriteAsync(datos, 0, datos.Length);
            Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {mensaje.Trim()}");
        }
        /* este metodo se encarga de asignar una mesa a una localidad específica.
         * Recibe un comando con el ID de la localidad y verifica si se puede asignar una nueva mesa.
         * Si es posible, registra la nueva mesa y envía su número al cliente; si no, envía "0".
         */
        private async Task EnviarAsignacionMesa(string comando, NetworkStream stream, TcpClient cliente)
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
                Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {nuevoNumeroMesa}");
            }
            else
            {
                byte[] datos = Encoding.UTF8.GetBytes("0\n");
                await stream.WriteAsync(datos, 0, datos.Length);
                Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: 0");
            }
        }
        /* este metodo se encarga de enviar las opciones de voto disponibles al cliente.
         * Obtiene las opciones desde la base de datos y las envía en un formato específico.
         * Cada opción se envía como una cadena con su ID y el nombre del candidato, separados por comas.
         */
        private async Task EnviarOpcionesVoto(NetworkStream stream, TcpClient cliente)
        {
            ClienteDAL clienteDAL = new ClienteDAL();
            List<Opcion> opciones = clienteDAL.ObtenerOpciones();

            StringBuilder sb = new StringBuilder();
            foreach (var o in opciones)
            {
                sb.Append($"{o.Id},{o.Candidato};");
            }

            string mensaje = sb.ToString() + "\n";
            byte[] datos = Encoding.UTF8.GetBytes(mensaje);
            await stream.WriteAsync(datos, 0, datos.Length);
            Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {mensaje.Trim()}");
        }
        /* este metodo se encarga de registrar los votos recibidos para una mesa específica.
         * Recibe un comando con el número de mesa, ID de localidad y los votos por opción.
         * Verifica si la mesa existe y está activa, luego registra los votos en la base de datos.
         * Envía "OK" al cliente si el registro fue exitoso, o un mensaje de error si hubo problemas.
         */
        private async Task RegistrarOpcionesVoto(string comando, NetworkStream stream, TcpClient cliente)
        {
            try
            {
                string datos = comando.Substring("RegistrarVotos|".Length);
                string[] partes = datos.Split('|');

                if (partes.Length < 2)
                {
                    string errorMsg = "Comando incompleto\n";
                    byte[] errorBytes = Encoding.UTF8.GetBytes(errorMsg);
                    await stream.WriteAsync(errorBytes, 0, errorBytes.Length);
                    Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {errorMsg.Trim()}");
                    return;
                }

                string[] mesaData = partes[0].Split(',');
                int numeroMesa = int.Parse(mesaData[0]);
                int idLocalidad = int.Parse(mesaData[1]);

                string[] votosPartes = partes[1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                ClienteDAL clienteDAL = new ClienteDAL();

                int idMesa = clienteDAL.ObtenerIdMesa(numeroMesa, idLocalidad);

                if (idMesa == 0)
                {
                    byte[] datosNoExiste = Encoding.UTF8.GetBytes("Mesa no encontrada\n");
                    await stream.WriteAsync(datosNoExiste, 0, datosNoExiste.Length);
                    Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: Mesa no encontrada");
                    return;
                }

                if (!clienteDAL.MesaEstaActiva(numeroMesa, idLocalidad))
                {
                    byte[] datosError = Encoding.UTF8.GetBytes("Mesa no activa\n");
                    await stream.WriteAsync(datosError, 0, datosError.Length);
                    Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: Mesa no activa");
                    return;
                }

                foreach (string voto in votosPartes)
                {
                    string[] campos = voto.Split(',');
                    int idOpcion = int.Parse(campos[0]);
                    int cantidad = int.Parse(campos[1]);

                    clienteDAL.InsertarOActualizarVoto(numeroMesa, idLocalidad, idOpcion, cantidad);
                }

                byte[] datosOK = Encoding.UTF8.GetBytes("OK\n");
                await stream.WriteAsync(datosOK, 0, datosOK.Length);
                Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: OK");
            }
            catch (Exception ex)
            {
                string msg = "ERROR: " + ex.Message + "\n";
                byte[] errorBytes = Encoding.UTF8.GetBytes(msg);
                await stream.WriteAsync(errorBytes, 0, errorBytes.Length);
                Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: {msg.Trim()}");
            }
        }
        /* este metodo se encarga de cerrar una mesa específica.
         * Recibe un comando con el número de mesa y ID de localidad.
         * Verifica si la mesa existe y está activa, luego la cierra en la base de datos.
         * Envía "1" al cliente si el cierre fue exitoso, "0" si ya estaba cerrada, o "2" si la mesa no existe.
         */
        private async Task CerrarMesa(string comando, NetworkStream stream, TcpClient cliente)
        {
            try
            {
                string datos = comando.Substring("CerrarMesa|".Length); // Extrae los datos del comando, eliminando el prefijo "CerrarMesa|".
                string[] partes = datos.Split(',');

                if (partes.Length != 2)
                {
                    byte[] errorFormato = Encoding.UTF8.GetBytes("Formato inválido\n");
                    await stream.WriteAsync(errorFormato, 0, errorFormato.Length);
                    Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: Formato inválido");
                    return;
                }

                int numeroMesa = int.Parse(partes[0]);
                int idLocalidad = int.Parse(partes[1]);

                ClienteDAL clienteDAL = new ClienteDAL();
                int idMesa = clienteDAL.ObtenerIdMesa(numeroMesa, idLocalidad);

                if (idMesa == 0)
                {
                    byte[] noExiste = Encoding.UTF8.GetBytes("2\n");
                    await stream.WriteAsync(noExiste, 0, noExiste.Length);
                    Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: Mesa no existe");
                    return;
                }

                if (!clienteDAL.MesaEstaActiva(numeroMesa, idLocalidad))
                {
                    byte[] yaCerrada = Encoding.UTF8.GetBytes("0\n");
                    await stream.WriteAsync(yaCerrada, 0, yaCerrada.Length);
                    Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: Mesa ya cerrada");
                    return;
                }

                clienteDAL.CerrarMesa(idMesa);

                byte[] cerrada = Encoding.UTF8.GetBytes("1\n");
                await stream.WriteAsync(cerrada, 0, cerrada.Length);
                Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: Mesa cerrada correctamente");
            }
            catch (Exception ex)
            {
                byte[] error = Encoding.UTF8.GetBytes("ERROR: " + ex.Message + "\n");
                await stream.WriteAsync(error, 0, error.Length);
                Informar($"Mensaje enviado a {cliente.Client.RemoteEndPoint}: ERROR: {ex.Message}");
            }
        }
    }
}
