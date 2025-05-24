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

namespace Servidor
{
    public partial class FrmAsignacionMesa : Form
    {
        private Localidad localidad;
        private DateTime fechaVotacion;
        public FrmAsignacionMesa()
        {
            InitializeComponent();
        }

        public FrmAsignacionMesa(Localidad localidad)
        {
            InitializeComponent();
            this.localidad = localidad;

        }

        public FrmAsignacionMesa(Localidad localidad, DateTime fecha)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.fechaVotacion = fecha;

        }

        //Metodo que permite cerrar el formulario mediante un boton
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAsignarMesa_Click(object sender, EventArgs e)
        {


        }
    }
}
