using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Modelo.ClienteTCP
{
    public class ClienteTCP
    {
        private TcpClient cliente;
        private NetworkStream stream;

        public bool Conectado => cliente != null && cliente.Connected;

        public async Task<bool> ConectarAsync(string ip, int puerto)
        {
            try
            {
                cliente = new TcpClient();
                await cliente.ConnectAsync(ip, puerto);
                stream = cliente.GetStream();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task EnviarComandoAsync(string comando)
        {
            if (!Conectado) throw new InvalidOperationException("No conectado al servidor");

            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(comando.Trim() + "\n");
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }
            catch
            {
                CerrarConexion();
                throw;
            }
        }

        public async Task<string> LeerRespuestaAsync()
        {
            StringBuilder sb = new StringBuilder();
            byte[] buffer = new byte[1];

            try
            {
                while (true)
                {
                    int leidos = await stream.ReadAsync(buffer, 0, 1);
                    if (leidos == 0 || (char)buffer[0] == '\n') break;
                    sb.Append((char)buffer[0]);
                }
            }
            catch
            {
                CerrarConexion();
                throw;
            }

            return sb.ToString().Trim();
        }


        public void CerrarConexion()
        {
            if (stream != null) stream.Close();
            if (cliente != null) cliente.Close();
        }
    
    }
}
