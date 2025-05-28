using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Servidor.Modelo.Base_de_datos;
using Servidor.Modelo.Clases;

namespace Servidor.Formularios
{
    public partial class FrmEstadisticas : Form
    {
        public FrmEstadisticas()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEstadisticas_Load(object sender, EventArgs e)
        {
            ConfigurarChart();
            CargarGrafico();
            CargarLocalidades();
            cbbMesa.Visible = false;
        }

        private void CargarLocalidades()
        {
            List<Localidad> localidades = new List<Localidad>();
            ClienteDAL clienteDAL = new ClienteDAL();

            localidades = clienteDAL.ObtenerLocalidades();
            localidades.Insert(0, new Localidad(0, "Mostrar todos", 0));
            cbbLocalidades.DataSource = null;
            cbbLocalidades.DataSource = localidades;
        }

        private void CargarGrafico(int? idLocalidad = null, int? numeroMesa = null)
        {
            ServidorDAL dal = new ServidorDAL();
            var resumen = dal.ObtenerResumenVotos(idLocalidad, numeroMesa);

            chartVotos.Series.Clear();
            chartVotos.Titles.Clear();

            var serie = new Series("Votos")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            int total = resumen.Sum(r => r.Cantidad);

            foreach (var item in resumen)
            {
                double porcentaje = total > 0 ? (item.Cantidad * 100.0 / total) : 0;
                serie.Points.AddXY($"{item.Nombre} ({porcentaje:0.##}%)", item.Cantidad);
            }

            chartVotos.Series.Add(serie);
            chartVotos.Titles.Add("Distribución de votos");
        }

        private void ConfigurarChart()
        {
            chartVotos.Series.Clear();
            chartVotos.Titles.Clear();
            chartVotos.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            area.BackColor = Color.Transparent;
            chartVotos.ChartAreas.Add(area);

            Series serie = new Series("Votos");
            serie.ChartType = SeriesChartType.Pie;
            serie.IsValueShownAsLabel = true;
            serie.LabelForeColor = Color.Black;
            serie.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            chartVotos.Series.Add(serie);
        }

        private void cbbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((Localidad)cbbLocalidades.SelectedItem).Id == 0)
            {
                CargarGrafico();
                cbbMesa.Visible = false;
                cbbMesa.SelectedIndex = -1;
            }else if (cbbLocalidades.SelectedItem is Localidad loc && loc.Id > 0)
            {
                CargarGrafico(((Localidad)cbbLocalidades.SelectedItem).Id);
                CargarMesasParaLocalidad(loc.Id);
                cbbMesa.Visible=true;
                cbbMesa.SelectedIndex = 0;
            }
        }

        private void cbbMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMesa.SelectedItem == null)
            {
                CargarGrafico();
                cbbMesa.Visible = false;
            }
            else if (cbbMesa.SelectedItem is int mesa && mesa > 0)
            {
                CargarGrafico(((Localidad)cbbLocalidades.SelectedItem).Id, mesa);
            }
        }

        private void CargarMesasParaLocalidad(int idLocalidad)
        {
            ClienteDAL clienteDAL = new ClienteDAL();
            int cantidadMesas = clienteDAL.ContarMesasPorLocalidad(idLocalidad);

            List<string> listaMesas = new List<string>();

            // Opción vacía al inicio
            listaMesas.Add(string.Empty);

            // Agregar números de mesa como strings
            for (int i = 1; i <= cantidadMesas; i++)
            {
                listaMesas.Add(i.ToString());
            }

            cbbMesa.DataSource = null;
            cbbMesa.DataSource = listaMesas;
        }

        private void cbbMesa_DropDown(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            int itemsToShow = Math.Min(cb.Items.Count, 5);  // Mostrar máximo 5 ítems
            int itemHeight = cb.ItemHeight;
            int border = SystemInformation.BorderSize.Height;

            cb.DropDownHeight = (itemHeight * itemsToShow) + border * 2;
        }

        private void cbbLocalidades_DropDown(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            int itemsToShow = Math.Min(cb.Items.Count, 5);  // Mostrar máximo 5 ítems
            int itemHeight = cb.ItemHeight;
            int border = SystemInformation.BorderSize.Height;

            cb.DropDownHeight = (itemHeight * itemsToShow) + border * 2;
        }
    }
}
