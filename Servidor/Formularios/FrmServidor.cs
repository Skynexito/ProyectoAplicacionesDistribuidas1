using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servidor.Formularios;

namespace Servidor
{
    public partial class FrmServidor : Form
    {
        public FrmServidor()
        {
            InitializeComponent();
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
            OpenChildFrm(new FrmAjustarNVotantes());
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
    }


}
