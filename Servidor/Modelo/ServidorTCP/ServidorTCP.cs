using System;
using System.Collections.Generic;   // List es una colección genérica que permite almacenar elementos de un tipo específico y proporciona métodos para manipularlos.
using System.Linq;
using System.Net.Sockets;   // TcpListener y TcpClient son clases que permiten crear un servidor TCP y manejar conexiones de clientes respectivamente.
using System.Net;   // IPAddress es una clase que representa una dirección IP, que puede ser IPv4 o IPv6.
using System.Text;  // Encoding es una clase que proporciona métodos para convertir cadenas de texto a arreglos de bytes y viceversa, utilizando diferentes esquemas de codificación como UTF-8.
using System.Threading.Tasks;   // Task es una clase que representa una operación asíncrona, permitiendo ejecutar código de forma no bloqueante y esperar su finalización sin bloquear el hilo actual.
using Servidor.Modelo.Base_de_datos;    // ClienteDAL y ServidorDAL son clases que interactúan con la base de datos para realizar operaciones relacionadas con clientes y servidores respectivamente.
using System.Windows.Forms; 
using Servidor.Modelo.Clases;   // Localidad y Opcion son clases que representan entidades del dominio, como localidades y opciones de voto en una elección.
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace Servidor.Modelo.ServidorTCP
{
    public class ServidorTCP
    {
        private TcpListener servidor;   // TcpListener es una clase que permite escuchar conexiones entrantes en un puerto específico.
        private bool activo = true;     // Indica si el servidor está activo y aceptando conexiones.
        /**
         * Método que inicia el servidor TCP en el puerto 6000,
         * acepta conexiones entrantes y atiende cada cliente en un hilo separado.
         * Muestra un mensaje en caso de error al iniciar.
         */
        public void IniciarServidor()
        {
            try
            {
                servidor = new TcpListener(IPAddress.Any, 6000);    // Escucha en todas las interfaces de red en el puerto 6000
                servidor.Start();   // Inicia el servidor TCP
                while (activo)
                {
                    TcpClient cliente = servidor.AcceptTcpClient(); // Acepta una conexión entrante
                    Task.Run(() => AtenderCliente(cliente));        // Atiende al cliente en un hilo separado
                }
            }
            catch (Exception ex)    // Captura cualquier excepción que ocurra al iniciar el servidor
            {
                MessageBox.Show("Error al iniciar el servidor: " + ex.Message); // Muestra un mensaje de error si no se puede iniciar el servidor
            }
        }
        /**
         * Método asíncrono que atiende la comunicación con un cliente conectado.
         * Lee comandos enviados por el cliente y ejecuta la acción correspondiente,
         * enviando respuestas según el tipo de comando recibido.
         * Maneja comandos como EnviarLocalidades, EnviarParametrosControl, AsignarMesa, RegistrarVotos,
         * EnviarOpciones, CerrarMesa y un ping para verificar conexión.
         * En caso de error, muestra el mensaje en consola y cierra la conexión.
         */
        public async Task AtenderCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream(); // Obtiene el stream de red asociado al cliente conectado, el stream es el canal de comunicación entre el servidor y el cliente.

            try
            {
                byte[] buffer = new byte[1024]; // Buffer para almacenar los datos leídos del cliente

                while (cliente.Connected)   // Mientras el cliente esté conectado, se mantiene en un bucle para leer comandos
                {
                    int leidos = await stream.ReadAsync(buffer, 0, buffer.Length);  // Lee datos del cliente de forma asíncrona

                    if (leidos == 0)    // Si no se han leído datos, significa que el cliente ha cerrado la conexión
                        break; // Cliente cerró conexión

                    string comando = Encoding.UTF8.GetString(buffer, 0, leidos).Trim(); // Convierte los bytes leídos a una cadena de texto y elimina espacios en blanco al inicio y al final

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
                        string respuesta = "PONG\n";    // Respuesta al comando PING
                        byte[] datos = Encoding.UTF8.GetBytes(respuesta);   // Convertir la respuesta a bytes
                        await stream.WriteAsync(datos, 0, datos.Length);    // Enviar la respuesta al cliente
                    }
                    else
                    {
                        // Mensaje no reconocido, enviar algo opcional
                        string mensaje = "Comando no válido\n";
                        byte[] datos = Encoding.UTF8.GetBytes(mensaje);
                        await stream.WriteAsync(datos, 0, datos.Length);
                    }

                    // Reiniciar buffer por seguridad (opcional)
                    Array.Clear(buffer, 0, buffer.Length);  // El buffer se reinicia para evitar datos residuales de lecturas anteriores
                }
            }
            catch (Exception ex)    // Captura cualquier excepción que ocurra al atender al cliente
            {
                Console.WriteLine("Error al atender cliente: " + ex.Message);
            }
            finally // Siempre se ejecuta al finalizar la atención del cliente, ya sea por error o porque se cerró la conexión
            {
                stream.Close(); // Cierra el stream de red asociado al cliente
                cliente.Close();    // Cierra el TcpClient para liberar recursos
                Console.WriteLine("Conexión cerrada con cliente.");
            }
        }
        /**
         * Envía la lista de localidades al cliente.
         * Obtiene las localidades desde la base de datos y las envía formateadas.
         */

        private async void EnviarLocalidades(NetworkStream stream, TcpClient cliente)
        {
            ClienteDAL clienteDAL = new ClienteDAL();   // Obtiene una instancia de ClienteDAL para interactuar con la base de datos
            List<Localidad> localidades = clienteDAL.ObtenerLocalidades();  // método que llama al procedimiento ObtenerLocalidades de la base de datos

            StringBuilder sb = new StringBuilder(); // Crea un StringBuilder para construir el mensaje a enviar
            foreach (var loc in localidades)    // Itera sobre cada localidad obtenida
                sb.Append($"{loc.Id},{loc.Nombre},{loc.CantidadMesas};");   // Formatea cada localidad como "Id,Nombre,CantidadMesas" y las separa con punto y coma, Append es un método de la clase StringBuilder que agrega una cadena al final del contenido actual del objeto StringBuilder.

            string mensaje = sb.ToString() + "\n";  // Convierte el StringBuilder a una cadena y agrega un salto de línea al final
            byte[] datos = Encoding.UTF8.GetBytes(mensaje); // Convierte el mensaje a un arreglo de bytes usando UTF8
            await stream.WriteAsync(datos, 0, datos.Length);    // Envía el mensaje al cliente de forma asíncrona
        }
        /**
         * Envía los parámetros de control (número de votantes y fecha de elección) al cliente.
         * Obtiene los datos desde la base de datos y los envía formateados.
         */
        private async void EnviarParametrosControl(NetworkStream stream,TcpClient cliente)
        {
            ServidorDAL servidor = new ServidorDAL();   // Obtiene una instancia de ServidorDAL para interactuar con la base de datos
            (int numeroVotantes, DateTime fecha) = servidor.ObtenerDatosControl();  // método que llama al procedimiento ObtenerDatosControl de la base de datos

            string mensaje = $"{numeroVotantes},{fecha:yyyy-MM-dd}\n";  // Formatea los datos como "numeroVotantes,fecha" y agrega un salto de línea al final
            byte[] datos = Encoding.UTF8.GetBytes(mensaje); // Convierte el mensaje a un arreglo de bytes usando UTF8
            await stream.WriteAsync(datos, 0, datos.Length);    // Envía el mensaje al cliente de forma asíncrona
        }
        /**
         * Maneja la asignación de una nueva mesa para una localidad.
         * Verifica si hay mesas disponibles y registra una nueva si es posible,
         * enviando el número de mesa asignado o 0 si no hay disponibilidad.
         */
        private async void EnviarAsignacionMesa(string comando, NetworkStream stream, TcpClient cliente)
        {
            string idStr = comando.Split('|')[1];   // Extrae el ID de localidad del comando recibido
            int idLocalidad = int.Parse(idStr);     // Convierte el ID de localidad a entero

            ClienteDAL clienteDAL = new ClienteDAL();   // Obtiene una instancia de ClienteDAL para interactuar con la base de datos

            int mesasExistentes = clienteDAL.ContarMesasPorLocalidad(idLocalidad);  // método que llama al procedimiento ContarMesasPorLocalidad para obtener la cantidad de mesas existentes en la localidad
            int cantidadMaxima = clienteDAL.ObtenerCantidadMesasPorLocalidad(idLocalidad);  // método que llama al procedimiento ObtenerCantidadMesasPorLocalidad para obtener la cantidad máxima de mesas permitidas en la localidad

            if (mesasExistentes < cantidadMaxima)
            {
                int nuevoNumeroMesa = mesasExistentes + 1;  // Calcula el nuevo número de mesa como el siguiente número disponible
                clienteDAL.RegistrarMesa(nuevoNumeroMesa, idLocalidad); // método que llama al procedimiento RegistrarMesa para registrar la nueva mesa en la base de datos
                byte[] datos = Encoding.UTF8.GetBytes($"{nuevoNumeroMesa}\n");  // Envía el número de mesa asignado al cliente, formateado como cadena y convertido a bytes
                await stream.WriteAsync(datos, 0, datos.Length);    // Envía el número de mesa al cliente de forma asíncrona
            }
            else
            {
                byte[] datos = Encoding.UTF8.GetBytes("0\n");   // No hay mesas disponibles, envía 0 al cliente
                await stream.WriteAsync(datos, 0, datos.Length);    // Envía el mensaje al cliente de forma asíncrona
            }
        }
        /**
         * Envía las opciones de voto disponibles al cliente.
         * Obtiene las opciones desde la base de datos y las envía formateadas.
         */
        private async void EnviarOpcionesVoto(NetworkStream stream, TcpClient cliente)
        {
            ClienteDAL clienteDAL = new ClienteDAL();   // Obtiene una instancia de ClienteDAL para interactuar con la base de datos
            List<Opcion> opciones = clienteDAL.ObtenerOpciones(); // método que llama al procedimiento ObtenerOpciones para obtener las opciones de voto disponibles

            StringBuilder sb = new StringBuilder(); // Crea un StringBuilder para construir el mensaje a enviar
            foreach (var o in opciones) // Itera sobre cada opción obtenida
            {
                sb.Append($"{o.Id},{o.Candidato};");    // Formatea cada opción como "Id,Candidato" y las separa con punto y coma
            }

            byte[] datos = Encoding.UTF8.GetBytes(sb.ToString() + "\n");    // Convierte el mensaje a un arreglo de bytes usando UTF8 y agrega un salto de línea al final
            await stream.WriteAsync(datos, 0, datos.Length);    // Envía el mensaje al cliente de forma asíncrona
        }
        /**
         * Registra o actualiza los votos enviados por el cliente para una mesa específica.
         * Valida la existencia y estado de la mesa antes de insertar o actualizar votos.
         * Envía respuesta OK o mensajes de error según corresponda.
         */
        private async void RegistrarOpcionesVoto(string comando, NetworkStream stream, TcpClient cliente)
        {
            try
            {
                // Extraer los datos del comando
                string datos = comando.Substring("RegistrarVotos|".Length); // Elimina el prefijo del comando para obtener los datos relevantes
                string[] partes = datos.Split('|'); // formato: NumeroMesa,IdLocalidad|IdOpcion,Cantidad;IdOpcion,Cantidad;...

                if (partes.Length < 2)  // Comprobar si hay suficientes partes en el comando
                {
                    string errorMsg = "Comando incompleto\n";   //  Mensaje de error si el comando no tiene el formato esperado
                    byte[] errorBytes = Encoding.UTF8.GetBytes(errorMsg);   // Convertir el mensaje de error a bytes
                    await stream.WriteAsync(errorBytes, 0, errorBytes.Length);  // Enviar el mensaje de error al cliente
                    return; // Salir del método si el formato es incorrecto
                }

                string[] mesaData = partes[0].Split(','); // formato: NumeroMesa,IdLocalidad
                int numeroMesa = int.Parse(mesaData[0]);    // Convertir el número de mesa a entero
                int idLocalidad = int.Parse(mesaData[1]);   // Convertir el ID de localidad a entero

                string[] votosPartes = partes[1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);   // formato: IdOpcion,Cantidad;IdOpcion,Cantidad;...

                ClienteDAL clienteDAL = new ClienteDAL();   // Obtiene una instancia de ClienteDAL para interactuar con la base de datos

                int idMesa = clienteDAL.ObtenerIdMesa(numeroMesa, idLocalidad); // método que llama al procedimiento ObtenerIdMesa para obtener el ID de la mesa según el número y localidad

                if (idMesa == 0)    // Mesa no existe
                {
                    byte[] datosNoExiste = Encoding.UTF8.GetBytes("Mesa no encontrada\n");
                    await stream.WriteAsync(datosNoExiste, 0, datosNoExiste.Length);    //  Enviar mensaje de error al cliente
                    return;
                }

                // Validar si la mesa está activa
                if (!clienteDAL.MesaEstaActiva(numeroMesa, idLocalidad))    // Mesa no está activa
                {
                    byte[] datosError = Encoding.UTF8.GetBytes("Mesa no activa\n"); // Enviar mensaje de error al cliente
                    await stream.WriteAsync(datosError, 0, datosError.Length);  // Enviar mensaje de error al cliente
                    return;
                }

                // Insertar o actualizar cada voto
                foreach (string voto in votosPartes)
                {
                    string[] campos = voto.Split(',');  // formato: IdOpcion,Cantidad
                    int idOpcion = int.Parse(campos[0]);    // Convertir el ID de opción a entero
                    int cantidad = int.Parse(campos[1]);    // Convertir la cantidad de votos a entero

                    clienteDAL.InsertarOActualizarVoto(numeroMesa, idLocalidad, idOpcion, cantidad);    // método que llama al procedimiento InsertarOActualizarVoto para insertar o actualizar el voto en la base de datos
                }

                // Confirmar al cliente
                byte[] datosOK = Encoding.UTF8.GetBytes("OK\n");    // Mensaje de confirmación de que los votos fueron registrados correctamente
                await stream.WriteAsync(datosOK, 0, datosOK.Length);    // Enviar mensaje de confirmación al cliente
            }
            catch (Exception ex)    // Captura cualquier excepción que ocurra durante el procesamiento del comando
            {
                string msg = "ERROR: " + ex.Message + "\n";     // Mensaje de error que incluye la descripción de la excepción
                byte[] errorBytes = Encoding.UTF8.GetBytes(msg);    // Convertir el mensaje de error a bytes
                await stream.WriteAsync(errorBytes, 0, errorBytes.Length);  // Enviar mensaje de error al cliente
            }
        }
        /**
         * Cierra una mesa específica actualizando su estado en la base de datos.
         * Valida el formato del comando, existencia y estado de la mesa antes de cerrar.
         * Envía códigos de respuesta según el resultado:
         * "1" = cerrada correctamente,
         * "0" = ya estaba cerrada,
         * "2" = mesa no existe,
         * o mensaje de error en caso de excepción.
         */
        private async void CerrarMesa(string comando, NetworkStream stream, TcpClient cliente)
        {
            try
            {
                string datos = comando.Substring("CerrarMesa|".Length); // Elimina el prefijo del comando para obtener los datos relevantes, substring es un método de la clase String que devuelve una subcadena de la cadena original, comenzando en el índice especificado hasta el final de la cadena.
                string[] partes = datos.Split(','); // formato: NumeroMesa,IdLocalidad

                if (partes.Length != 2) // Verifica que el comando tenga el formato correcto
                {
                    byte[] errorFormato = Encoding.UTF8.GetBytes("Formato inválido\n"); // Mensaje de error si el formato del comando es incorrecto
                    await stream.WriteAsync(errorFormato, 0, errorFormato.Length);  // Enviar mensaje de error al cliente
                    return;
                }

                int numeroMesa = int.Parse(partes[0]);  // Convertir el número de mesa a entero
                int idLocalidad = int.Parse(partes[1]); // Convertir el ID de localidad a entero

                ClienteDAL clienteDAL = new ClienteDAL();   // Obtiene una instancia de ClienteDAL para interactuar con la base de datos
                int idMesa = clienteDAL.ObtenerIdMesa(numeroMesa, idLocalidad); // método que llama al procedimiento ObtenerIdMesa para obtener el ID de la mesa según el número y localidad

                if (idMesa == 0)    // Mesa no existe
                {
                    byte[] noExiste = Encoding.UTF8.GetBytes("2\n"); // Mesa no existe
                    await stream.WriteAsync(noExiste, 0, noExiste.Length);
                    return;
                }

                if (!clienteDAL.MesaEstaActiva(numeroMesa, idLocalidad))
                {
                    byte[] yaCerrada = Encoding.UTF8.GetBytes("0\n"); // La mesa ya estaba cerrada
                    await stream.WriteAsync(yaCerrada, 0, yaCerrada.Length);    // Enviar mensaje de que la mesa ya estaba cerrada
                    return;
                }

                clienteDAL.CerrarMesa(idMesa); // método que llama al procedimiento CerrarMesa para actualizar el estado de la mesa en la base de datos

                byte[] cerrada = Encoding.UTF8.GetBytes("1\n"); //  Mesa cerrada correctamente
                await stream.WriteAsync(cerrada, 0, cerrada.Length);    // Enviar mensaje de que la mesa fue cerrada correctamente
            }
            catch (Exception ex)    // Captura cualquier excepción que ocurra durante el procesamiento del comando
            {
                byte[] error = Encoding.UTF8.GetBytes("ERROR: " + ex.Message + "\n");   // Mensaje de error que incluye la descripción de la excepción
                await stream.WriteAsync(error, 0, error.Length);    // Enviar mensaje de error al cliente
            }
        }
    }
}
