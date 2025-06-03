using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Servidor.Formularios; // Importar el espacio de nombres del formulario hijo
using Servidor.Modelo.ServidorTCP;  // Importar la clase ServidorTCP


namespace Servidor
{
    public partial class FrmServidor : Form
    {
        private ServidorTCP servidorTCP;
        
        public FrmServidor()
        {
            
            InitializeComponent();
            servidorTCP = new ServidorTCP();
            servidorTCP.OnMensajeRecibido += AgregarMensaje;

            Thread hiloServidor = new Thread(servidorTCP.IniciarServidor);
            hiloServidor.IsBackground = true;
            hiloServidor.Start();
        }
        /* este metodo se encarga de agregar mensajes al TextBox txtComunicacion de forma segura para el hilo */

        public void AgregarMensaje(string mensaje)
        {
            if (txtComunicacion.InvokeRequired)
            {
                txtComunicacion.Invoke(new Action(() => {
                    txtComunicacion.AppendText(mensaje + Environment.NewLine);
                }));
            }
            else
            {
                txtComunicacion.AppendText(mensaje + Environment.NewLine);
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

        private void btnAjustarVotantes_Click(object sender, EventArgs e)
        {
            OpenChildFrm(new FrmAjustarParametros());
        }

        private void btnCrearLocalidades_Click(object sender, EventArgs e)
        {
            OpenChildFrm(new FrmCrearLocalidad());
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            OpenChildFrm(new FrmEstadisticas());
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
        /* este metodo es para que al cerrar el formulario servidor se detenga el servidor TCP */
        private void FrmServidor_FormClosing(object sender, FormClosingEventArgs e)
        {
            servidorTCP.DetenerServidor();
        }
    }
}
