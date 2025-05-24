using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Servidor.Formularios;
using System.Net;
using System.Net.Sockets;
using Servidor.Modelo.Clases;
using Servidor.Modelo.Base_de_datos;


namespace Servidor
{
    public partial class FrmServidor : Form
    {
        private TcpListener servidor;
        private bool activo = true;
        public FrmServidor()
        {
            InitializeComponent();
            Thread hiloServidor = new Thread(IniciarServidor);
            hiloServidor.IsBackground = true;
            hiloServidor.Start();
        }

        private void IniciarServidor()
        {
            try
            {
                servidor = new TcpListener(IPAddress.Any, 6000);
                servidor.Start();
                while (activo)
                {
                    TcpClient cliente = servidor.AcceptTcpClient();
                    Task.Run(() => AtenderCliente(cliente));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar el servidor: " + ex.Message);
            }
        }

        private async Task AtenderCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            try
            {
                byte[] buffer = new byte[1024];
                int leidos = await stream.ReadAsync(buffer, 0, buffer.Length);
                string comando = Encoding.UTF8.GetString(buffer, 0, leidos).Trim();

                if (comando == "1")
                {
                    ClienteDAL clienteDAL = new ClienteDAL();
                    List<Localidad> localidades = clienteDAL.ObtenerLocalidades();
                    StringBuilder sb = new StringBuilder();
                    foreach (var loc in localidades)
                        sb.Append($"{loc.Id},{loc.Nombre},{loc.CantidadMesas};"); // Cada objeto termina en ;

                    string mensaje = sb.ToString();
                    byte[] datos = Encoding.UTF8.GetBytes(mensaje + "\n");
                    await stream.WriteAsync(datos, 0, datos.Length);
                }else if (comando == "2")
                {
                    ServidorDAL servidor = new ServidorDAL();
                    (int numeroVotantes, DateTime fecha) = servidor.ObtenerDatosControl();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al atender cliente: " + ex.Message);
            }
            finally
            {
                stream.Close();
                cliente.Close();
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
    }


}
