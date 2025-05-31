using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cliente.Formularios;  // Importar el espacio de nombres del formulario hijo
using Guna.UI2.WinForms;
using Servidor.Formularios; // Importar el espacio de nombres del formulario principal
using Cliente.Modelo.Clases;    // Importar las clases de modelo necesarias
using Cliente.Modelo.ClienteTCP; // Importar la clase ClienteTCP para la comunicación con el servidor

namespace Servidor
{
    public partial class FrmCliente : Form
    {
        // Variables para almacenar los parámetros de control obtenidos del servidor
        private int maxVotantes;
        private Localidad localidad;
        private DateTime fechaEleccion;
        private FrmClienteInicio formInicio;
        private ClienteTCP clienteTCP;

        public FrmCliente()
        {
            InitializeComponent();
        }
        public FrmCliente(FrmClienteInicio formInicio)
        {
            InitializeComponent();
            this.formInicio = formInicio;
        }
        // Constructor que recibe un objeto FrmClienteInicio y una localidad específica
        public FrmCliente(FrmClienteInicio formInicio, Localidad localidad)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.formInicio = formInicio;

        }
        // Constructor que recibe un objeto FrmClienteInicio, una localidad y un cliente TCP para la comunicación con el servidor
        public FrmCliente(FrmClienteInicio formInicio, Localidad localidad, ClienteTCP clienteTCP)
        {
            InitializeComponent();
            this.localidad = localidad;         // Localidad seleccionada para el cliente
            this.formInicio = formInicio;       // Inicio del formulario principal que contiene el panel de formularios hijos
            this.clienteTCP = clienteTCP;       // Cliente TCP para la comunicación con el servidor
            ObtenerParametrosControlAsync(); // Obtener los parámetros de control desde el servidor
            lblNombre.Text = localidad.Nombre;  // Mostrar el nombre de la localidad en el label
        }

        private async void ObtenerParametrosControlAsync()
        {
            await clienteTCP.EnviarComandoAsync("EnviarParametrosControl"); // Enviar comando al servidor para obtener los parámetros de control
            string respuesta = await clienteTCP.LeerRespuestaAsync();       // Leer la respuesta del servidor

            string[] partes = respuesta.Split(',');     // Dividir la respuesta en partes usando la coma como separador
            // Asignar los valores a las variables correspondientes
            if (partes.Length == 2) // Asegurarse de que la respuesta tenga el formato correcto
            {
                // Convertir los valores de la respuesta a los tipos adecuados
                maxVotantes = int.Parse(partes[0]);
                fechaEleccion = DateTime.Parse(partes[1]);

            }
        }




        //----------------- Metodo para cambiar entre formularios a traves de un panel dentro del formulario Servidor -----------------------------------------------
        private Form frmActivo = null;
        private void OpenChildFrm(Form frmChild)
        {
            if (frmActivo != null) frmActivo.Close();
            frmActivo = frmChild;
            frmChild.TopLevel = false;
            frmChild.FormBorderStyle = FormBorderStyle.None;
            frmChild.Dock = DockStyle.Fill;
            panelChildFrm.Controls.Add(frmChild);
            panelChildFrm.Tag = frmChild;
            frmChild.BringToFront();
            frmChild.Show();

            
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------------

        //------------------- Metodos que permitiran ingresar a los diferentes opciones de formulario -----------------------------------------------------------------

        private void btnAsignarMesa_Click(object sender, EventArgs e)
        {
            OpenChildFrm(new FrmAsignacionMesa(localidad, fechaEleccion, clienteTCP));
        }

        private void btnCrearLocalidades_Click(object sender, EventArgs e)
        {
            OpenChildFrm(new FrmRegistrarVotos(localidad, maxVotantes, clienteTCP));
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            OpenChildFrm(new FrmCerrarMesa(localidad, clienteTCP));
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            formInicio.Show();
            this.Close();
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------

        private bool _isDragging = false;       // Variable para controlar el arrastre del formulario
        private Point _offset;                  // Variable para almacenar la posición del clic inicial

        // Evento para iniciar el arrastre del formulario al hacer clic en el panel
        private void pnlForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _offset = e.Location; // Guarda la posición inicial del clic
                pnlForm.Cursor = Cursors.SizeAll; // Cambia el cursor
            }
        }

        // Evento para mover el formulario mientras se arrastra

        private void pnlForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calcula la nueva posición del formulario
                Point newLocation = this.PointToScreen(e.Location);
                newLocation.Offset(-_offset.X, -_offset.Y);
                this.Location = newLocation;
            }
        }
        // Evento para finalizar el arrastre del formulario al soltar el botón del ratón
        private void pnlForm_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            pnlForm.Cursor = Cursors.Default; // Restaura el cursor
        }
        // metodo que permite cerrar el formulario mediante un boton
        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }


}
