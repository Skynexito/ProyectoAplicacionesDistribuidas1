using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servidor;
using Cliente.Modelo.Clases;
using System.Net.Sockets;

namespace Cliente.Formularios
{
    public partial class FrmClienteInicio : Form
    {
        private List<Localidad> localidades = new List<Localidad>();
      
        public FrmClienteInicio()
        {
            InitializeComponent();
            ConectarServidor();
        }

        private void ConectarServidor()
        {
            bool conectado = false;

            while (!conectado)
            {
                try
                {
                    TcpClient cliente = new TcpClient();
                    cliente.Connect("127.0.0.1", 6000);
                    NetworkStream stream = cliente.GetStream();

                    byte[] mensaje = Encoding.UTF8.GetBytes("1\n");
                    stream.Write(mensaje, 0, mensaje.Length);

                    StringBuilder sb = new StringBuilder();
                    byte[] buffer = new byte[1];
                    while (true)
                    {
                        int leido = stream.Read(buffer, 0, 1);
                        if (leido == 0 || (char)buffer[0] == '\n') break;
                        sb.Append((char)buffer[0]);
                    }

                    string data = sb.ToString();
                    localidades = ParsearLocalidades(data);
                    cbbLocalidades.Items.Clear();
                    foreach (var loc in localidades)
                    {
                        cbbLocalidades.Items.Add(loc);
                    }
                    cbbLocalidades.SelectedIndex = localidades.Count > 0 ? 0 : -1;

                    conectado = true;
                }
                catch
                {
                    DialogResult retry = MessageBox.Show("No se pudo conectar al servidor. ¿Reintentar?", "Conexión fallida",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (retry == DialogResult.No)
                    {
                        Application.Exit();
                        break;
                    }
                }
            }
        }

        private List<Localidad> ParsearLocalidades(string data)
        {
            List<Localidad> lista = new List<Localidad>();
            string[] partes = data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string loc in partes)
            {
                string[] campos = loc.Split(',');
                int id = int.Parse(campos[0]);
                string nombre = campos[1];
                int mesas = int.Parse(campos[2]);
                lista.Add(new Localidad(id, nombre, mesas));
            }

            return lista;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            Localidad localidad = (Localidad) cbbLocalidades.SelectedItem;
            this.Hide();
            FrmCliente form = new FrmCliente(this, localidad );
            form.ShowDialog();
            
        }
    }
}
