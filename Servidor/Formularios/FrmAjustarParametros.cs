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
    public partial class FrmAjustarParametros : Form
    {
        public FrmAjustarParametros()
        {
            InitializeComponent();
            ServidorDAL servidor = new ServidorDAL();
            (int numeroVotantes, DateTime fecha) = servidor.ObtenerDatosControl();
            numVotantes.Value = Convert.ToDecimal(numeroVotantes);
            mcFecha.SelectionStart = fecha;

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            ServidorDAL servicio = new ServidorDAL();
            servicio.ActualizarFechaEleccion(mcFecha.SelectionStart);
            MessageBox.Show("Fecha guardada con exito");
        }
    }
}
