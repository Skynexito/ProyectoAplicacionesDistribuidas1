using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servidor;

namespace Cliente.Formularios
{
    public partial class FrmClienteInicio : Form
    {
        public FrmClienteInicio()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCliente form = new FrmCliente(this);
            form.ShowDialog();
            
        }
    }
}
