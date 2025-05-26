using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cliente.Modelo.Clases;
using Cliente.Modelo.ClienteTCP;

namespace Servidor
{
    public partial class FrmAsignacionMesa : Form
    {
        private Localidad localidad;
        private DateTime fechaVotacion;
        private ClienteTCP clienteTCP;
        public FrmAsignacionMesa()
        {
            InitializeComponent();
        }

        public FrmAsignacionMesa(Localidad localidad)
        {
            InitializeComponent();
            this.localidad = localidad;

        }

        public FrmAsignacionMesa(Localidad localidad, DateTime fecha, ClienteTCP client)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.fechaVotacion = fecha;
            this.clienteTCP = client;

        }

        //Metodo que permite cerrar el formulario mediante un boton
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnAsignarMesa_Click(object sender, EventArgs e)
        {
            if (fechaVotacion == mcVotacion.SelectionStart)
            {
                string mensaje = $"AsignarMesa|{localidad.Id}\n";
                await clienteTCP.EnviarComandoAsync(mensaje);

                string respuesta = await clienteTCP.LeerRespuestaAsync();
                //MessageBox.Show("Respuesta recibida: '" + respuesta + "'");

                int numeroMesa = int.Parse(respuesta);

                if (numeroMesa == 0)
                {
                    MessageBox.Show("Ya se han asignado todas las mesas para esta localidad.");
                }
                else
                {
                    //MessageBox.Show($"Mesa asignada con número: {numeroMesa}");
                    numMesa.Text = Convert.ToString(numeroMesa);
                    
                }
            }
            else
            {
                MessageBox.Show("La fecha no corresponde con el día de las votaciones.");
            }
        }

    }
}
