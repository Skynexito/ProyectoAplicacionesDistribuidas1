using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;   // TcpClient para manejar la conexión TCP
using System.Text;          // Encoding para convertir cadenas a bytes
using System.Threading.Tasks;   // Tareas asíncronas para operaciones de red

namespace Cliente.Modelo.ClienteTCP
{
    public class ClienteTCP
    {
        private TcpClient cliente;  // Instancia del cliente TCP que maneja la conexión con el servidor
        private NetworkStream stream;   // Instancia del stream de red para enviar y recibir datos
        /**
         * Propiedad que indica si el cliente TCP está conectado al servidor.
         * Retorna true si la instancia de TcpClient existe y la conexión está activa.
         */
        public bool Conectado => cliente != null && cliente.Connected;
        /**
         * Método asíncrono que intenta establecer una conexión TCP con el servidor
         * usando la IP y puerto especificados. Retorna true si la conexión fue exitosa,
         * o false si ocurrió algún error durante la conexión.
         */
        public async Task<bool> ConectarAsync(string ip, int puerto)
        {
            try // intenta establecer la conexión
            {
                cliente = new TcpClient();  // Crea una nueva instancia de TcpClient
                await cliente.ConnectAsync(ip, puerto); // Conecta al servidor especificado por IP y puerto
                stream = cliente.GetStream();   // Obtiene el NetworkStream asociado al cliente TCP
                return true;    // Indica que la conexión fue exitosa
            }
            catch   // Captura cualquier excepcion que ocurra durante la conexion
            {
                return false;   // Indica que la conexión falló
            }
        }
        /**
         * Método asíncrono para enviar un comando al servidor mediante el stream TCP.
         * Lanza una excepción si no está conectado.
         * Envía el comando codificado en UTF8 seguido de un salto de línea.
         * En caso de error, cierra la conexión y relanza la excepción.
         */
        public async Task EnviarComandoAsync(string comando)
        {
            // Verifica si el cliente está conectado antes de enviar el comando
            if (!Conectado) throw new InvalidOperationException("No conectado al servidor");    // InvalidOperationException es una excepción que indica que el método no se puede ejecutar en el estado actual del objeto.

            try // intenta enviar el comando al servidor
            {
                byte[] buffer = Encoding.UTF8.GetBytes(comando.Trim() + "\n");  // Convierte el comando a un arreglo de bytes usando UTF8 y agrega un salto de línea al final
                await stream.WriteAsync(buffer, 0, buffer.Length);  // Envía el comando al servidor de forma asíncrona
            }
            catch // Captura cualquier excepción que ocurra durante el envío del comando
            {
                CerrarConexion();   // Cierra la conexión TCP si ocurre un error al enviar el comando
                throw;  // Relanza la excepción para que pueda ser manejada por el llamador
            }
        }
        /**
         * Método asíncrono para leer la respuesta del servidor desde el stream TCP.
         * Lee byte a byte hasta encontrar un salto de línea o fin de stream.
         * En caso de error, cierra la conexión y relanza la excepción.
         * Retorna la cadena recibida sin espacios en blanco al inicio o final.
         */
        public async Task<string> LeerRespuestaAsync()
        {
            StringBuilder sb = new StringBuilder(); // Crea un StringBuilder para construir la respuesta de forma eficiente, un StringBuilder es una clase que permite construir cadenas de texto de manera eficiente, especialmente cuando se realizan múltiples concatenaciones o modificaciones en la cadena.
            byte[] buffer = new byte[1];    // Crea un buffer de un byte para leer la respuesta del servidor, el tamaño del buffer es 1 byte, lo que significa que se leerá un byte a la vez del stream.

            try // intenta leer la respuesta del servidor
            {
                while (true) // bucle infinito para leer byte a byte hasta encontrar un salto de línea o fin de stream
                {
                    int leidos = await stream.ReadAsync(buffer, 0, 1);  // Lee un byte del stream de forma asíncrona y almacena el número de bytes leídos en la variable leidos.
                    if (leidos == 0 || (char)buffer[0] == '\n') break;  // Si no se leyeron bytes o se encontró un salto de línea, sale del bucle
                    sb.Append((char)buffer[0]);     // Convierte el byte leído a un carácter y lo agrega al StringBuilder
                }
            }
            catch // Captura cualquier excepción que ocurra durante la lectura de la respuesta
            {
                CerrarConexion();   // Cierra la conexión TCP si ocurre un error al leer la respuesta
                throw;  // Relanza la excepción para que pueda ser manejada por el llamador, throw es una palabra clave que se utiliza para lanzar una excepción en C#.
            }

            return sb.ToString().Trim();    // Convierte el StringBuilder a una cadena y elimina los espacios en blanco al inicio y al final, el método ToString() convierte el contenido del StringBuilder en una cadena de texto, y el método Trim() elimina los espacios en blanco al inicio y al final de la cadena resultante.
        }
        /**
         * Método para cerrar la conexión TCP y liberar los recursos asociados.
         * Cierra el stream y el cliente TCP si están abiertos.
         */

        public void CerrarConexion()
        {
            if (stream != null) stream.Close(); // Cierra el NetworkStream si está abierto
            if (cliente != null) cliente.Close();   // Cierra el TcpClient si está abierto
        }
    
    }
}
