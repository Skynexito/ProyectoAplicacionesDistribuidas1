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

namespace Servidor
{
    public partial class FrmAjustarNVotantes : Form
    {
        public FrmAjustarNVotantes()
        {
            InitializeComponent();
        }

        //Metodo que permite cerrar el formulario mediante un boton
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardarNumeroVotantes_Click(object sender, EventArgs e)
        {
            ServidorDAL servicio = new ServidorDAL();
            servicio.registrarCantidadMesasPorLocalidad(Convert.ToInt32(numVotantes.Value));
            MessageBox.Show("Numero guardado con exito");
        }
    }
}
