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
            // Iniciar la conexión al servidor al cargar el formulario
            _ = ConectarServidorAsync();
        }

        private async Task ConectarServidorAsync()
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


        private async Task<bool> IntentarReconectarAsync()
        {
            try
            {
                bool conectado = await clienteTCP.ConectarAsync("127.0.0.1", 6000);
                if (conectado)
                {
                    await clienteTCP.EnviarComandoAsync("EnviarLocalidades");
                    string data = await clienteTCP.LeerRespuestaAsync();
                    localidades = ParsearLocalidades(data);

                    cbbLocalidades.Items.Clear();
                    foreach (var loc in localidades)
                    {
                        cbbLocalidades.Items.Add(loc);
                    }
                    cbbLocalidades.SelectedIndex = localidades.Count > 0 ? 0 : -1;

                    return true;
                }
            }
            catch
            {
                // Ignorar excepción aquí, se maneja en llamada
            }
            return false;
        }


        private List<Localidad> ParsearLocalidades(string data)
        {
            List<Localidad> lista = new List<Localidad>();
            string[] partes = data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string loc in partes)
            {
                string[] campos = loc.Split(',');
                if (campos.Length >= 3 &&
                    int.TryParse(campos[0], out int id) &&
                    int.TryParse(campos[2], out int mesas))
                {
                    string nombre = campos[1];
                    lista.Add(new Localidad(id, nombre, mesas));
                }
            }

            return lista;
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            if (clienteTCP == null || !clienteTCP.Conectado)
            {
                var reconectar = MessageBox.Show("El servidor no está disponible. ¿Desea intentar reconectar?", "Error de conexión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (reconectar == DialogResult.Yes)
                {
                    bool exito = await IntentarReconectarAsync();
                    if (!exito)
                    {
                        MessageBox.Show("No se pudo reconectar al servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            try
            {
                await clienteTCP.EnviarComandoAsync("PING");
                string respuesta = await clienteTCP.LeerRespuestaAsync();

                if (respuesta != "PONG")
                {
                    MessageBox.Show("Respuesta inesperada del servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("La conexión con el servidor se perdió.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbbLocalidades.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una localidad.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Localidad localidad = (Localidad)cbbLocalidades.SelectedItem;
            this.Hide();
            FrmCliente form = new FrmCliente(this, localidad, clienteTCP);
            form.ShowDialog();
            this.Show();
        }



        private void FrmClienteInicio_Load(object sender, EventArgs e)
        {
            // Puedes agregar lógica adicional al cargar el formulario si es necesario
        }
    }
}
