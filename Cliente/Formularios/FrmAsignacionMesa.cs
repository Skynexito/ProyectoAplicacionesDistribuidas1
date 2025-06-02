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
        // Variables para almacenar la localidad, la fecha de votación y el cliente TCP
        private Localidad localidad;
        private DateTime fechaVotacion;
        private ClienteTCP clienteTCP;
        private FrmCliente frmPadre;

        public FrmAsignacionMesa()
        {
            InitializeComponent();
        }

        public FrmAsignacionMesa(Localidad localidad)
        {
            InitializeComponent();
            this.localidad = localidad; // Asignar la localidad al campo correspondiente

        }
        // Constructor que recibe una localidad y una fecha de votación, además del cliente TCP para la comunicación con el servidor
        public FrmAsignacionMesa(Localidad localidad, DateTime fecha, ClienteTCP client, FrmCliente padre)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.fechaVotacion = fecha;
            this.clienteTCP = client;
            this.frmPadre = padre;
        }

        /** 
         * Método que se ejecuta al hacer clic en el botón de salida, 
         * permitiendo cerrar el formulario actual.
         */
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /** 
         * Método asíncrono que se ejecuta al hacer clic en el botón para asignar una mesa. 
         * Primero verifica que la fecha seleccionada en el calendario coincida con la fecha de votación. 
         * Si es así, envía un comando al servidor para solicitar la asignación de una mesa para la localidad especificada. 
         * Luego lee la respuesta con el número de mesa asignado. 
         * Si el número es 0, indica que no quedan mesas disponibles y muestra un mensaje; 
         * de lo contrario, muestra el número de mesa asignado en el formulario. 
         * Si la fecha no coincide, muestra un mensaje de error.
         */
        private async void btnAsignarMesa_Click(object sender, EventArgs e)
        {
            if (fechaVotacion == mcVotacion.SelectionStart)
            {
                string mensaje = $"AsignarMesa|{localidad.Id}";

                // Usar la referencia pasada al formulario para llamar al método centralizado
                string respuesta = await frmPadre.EnviarComandoConManejoErrores(mensaje);

                if (respuesta == null)
                {
                    // Error manejado, solo salir
                    return;
                }

                if (int.TryParse(respuesta, out int numeroMesa))
                {
                    if (numeroMesa == 0)
                    {
                        MessageBox.Show("Ya se han asignado todas las mesas para esta localidad.");
                    }
                    else
                    {
                        numMesa.Text = numeroMesa.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Respuesta inválida del servidor.");
                }
            }
            else
            {
                MessageBox.Show("La fecha no corresponde con el día de las votaciones.");
            }
        }


    }
}
