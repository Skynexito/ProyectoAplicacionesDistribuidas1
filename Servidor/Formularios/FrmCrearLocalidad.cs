using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servidor.Modelo.Base_de_datos;
using Servidor.Modelo.Clases;

namespace Servidor.Formularios
{
    public partial class FrmCrearLocalidad : Form
    {
        public FrmCrearLocalidad()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardarNumeroVotantes_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombreLocalidad.Text))
            {
                if(numMesasLocalidad.Value > 0)
                {
                    ServidorDAL db = new ServidorDAL();
                    Localidad localidad = new Localidad(0, txtNombreLocalidad.Text, Convert.ToInt32(numMesasLocalidad.Value));
                    db.registrarLocalidad(localidad);
                    MessageBox.Show("Localidad registrada");
                }else
                {
                    MessageBox.Show("No pueden exisitir 0 mesas en una localidad");
                }
            }
            else
            {
                MessageBox.Show("No puede dejar el nombre de la localidad en blanco");
            }
        }
    }
}
