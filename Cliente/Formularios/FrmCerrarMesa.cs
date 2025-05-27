using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cliente.Modelo.Clases;
using Cliente.Modelo.ClienteTCP;

namespace Servidor.Formularios
{
    public partial class FrmCerrarMesa : Form
    {
        private Localidad localidad;
        private ClienteTCP clienteTCP;
        public FrmCerrarMesa()
        {
            InitializeComponent();
        }

        public FrmCerrarMesa(Localidad localidad, ClienteTCP cliente)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.clienteTCP = cliente;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            int numeroMesa = Convert.ToInt32(numMesa.Value);

            await clienteTCP.EnviarComandoAsync($"CerrarMesa|{numeroMesa},{localidad.Id}");
            string respuesta = await clienteTCP.LeerRespuestaAsync();

            switch (respuesta)
            {
                case "1":
                    MessageBox.Show("La mesa fue cerrada correctamente.");
                    break;
                case "0":
                    MessageBox.Show("La mesa ya estaba cerrada.");
                    break;
                case "2":
                    MessageBox.Show("La mesa no existe.");
                    break;
                default:
                    MessageBox.Show("Error inesperado del servidor: " + respuesta);
                    break;
            }

        }
    }
}
