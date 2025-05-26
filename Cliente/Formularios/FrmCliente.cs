using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cliente.Formularios;
using Guna.UI2.WinForms;
using Servidor.Formularios;
using Cliente.Modelo.Clases;
using Cliente.Modelo.ClienteTCP;

namespace Servidor
{
    public partial class FrmCliente : Form
    {
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

        public FrmCliente(FrmClienteInicio formInicio, Localidad localidad)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.formInicio = formInicio;

        }

        public FrmCliente(FrmClienteInicio formInicio, Localidad localidad, ClienteTCP clienteTCP)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.formInicio = formInicio;
            this.clienteTCP = clienteTCP;
            ObtenerParametrosControlAsync(); // ejemplo de uso inmediato
        }

        private async void ObtenerParametrosControlAsync()
        {
            await clienteTCP.EnviarComandoAsync("EnviarParametrosControl");
            string respuesta = await clienteTCP.LeerRespuestaAsync();

            string[] partes = respuesta.Split(',');
            if (partes.Length == 2)
            {
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
            OpenChildFrm(new FrmRegistrarVotos());
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            OpenChildFrm(new FrmCerrarMesa());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            formInicio.Show();
            this.Close();
        }


        //-------------------------------------------------------------------------------------------------------------------------------------------------------------

        private bool _isDragging = false;
        private Point _offset;
        private void pnlForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _offset = e.Location; // Guarda la posición inicial del clic
                pnlForm.Cursor = Cursors.SizeAll; // Cambia el cursor
            }
        }

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

        private void pnlForm_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            pnlForm.Cursor = Cursors.Default; // Restaura el cursor
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }


}
