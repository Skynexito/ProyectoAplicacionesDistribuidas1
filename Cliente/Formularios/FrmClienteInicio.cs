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
using Cliente.Modelo.ClienteTCP;

namespace Cliente.Formularios
{
    public partial class FrmClienteInicio : Form
    {
        private List<Localidad> localidades = new List<Localidad>();
        private ClienteTCP clienteTCP = new ClienteTCP();


        public FrmClienteInicio()
        {
            InitializeComponent();
            ConectarServidor();
        }

        private async void ConectarServidor()
        {
            bool conectado = false;

            while (!conectado)
            {
                try
                {
                    conectado = await clienteTCP.ConectarAsync("127.0.0.1", 6000);

                    if (!conectado)
                        throw new Exception();

                    await clienteTCP.EnviarComandoAsync("EnviarLocalidades");

                    string data = await clienteTCP.LeerRespuestaAsync();
                    localidades = ParsearLocalidades(data);

                    cbbLocalidades.Items.Clear();
                    foreach (var loc in localidades)
                    {
                        cbbLocalidades.Items.Add(loc);
                    }
                    cbbLocalidades.SelectedIndex = localidades.Count > 0 ? 0 : -1;
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
            Localidad localidad = (Localidad)cbbLocalidades.SelectedItem;
            this.Hide();
            FrmCliente form = new FrmCliente(this, localidad, clienteTCP);
            form.ShowDialog();
        }

    }
}
