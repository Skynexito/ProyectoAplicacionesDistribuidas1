using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;  // Gráficos
using Servidor.Modelo.Base_de_datos;    // Acceso a la base de datos
using Servidor.Modelo.Clases;   // Clases que representan las entidades de la aplicación

namespace Servidor.Formularios
{
    public partial class FrmEstadisticas : Form
    {
        // Timer para actualizar el gráfico periódicamente
        private System.Windows.Forms.Timer timerActualizacion;

        public FrmEstadisticas()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /* Método que se ejecuta al cargar el formulario.
         * Configura el gráfico y carga los datos iniciales.
         */
        private void FrmEstadisticas_Load(object sender, EventArgs e)
        {
            ConfigurarChart();
            CargarGrafico();
            CargarLocalidades();
            cbbMesa.Visible = false;
            cbbLocalidades.DropDownStyle = ComboBoxStyle.DropDownList;

            // Inicializar y arrancar el timer para actualizar el gráfico periódicamente
            timerActualizacion = new System.Windows.Forms.Timer();  // timer es un objeto Timer que se usará para actualizar el gráfico
            timerActualizacion.Interval = 30000; // 30 segundos     // este intervalo es el que se usará para actualizar el gráfico
            timerActualizacion.Tick += TimerActualizacion_Tick;
            timerActualizacion.Start();
        }

        /* Evento que se ejecuta cada vez que el timer dispara su tick.
         * Actualiza el gráfico con los datos de la localidad y mesa seleccionadas.
         */
        private void TimerActualizacion_Tick(object sender, EventArgs e)
        {
            // Si el formulario está cerrado o en proceso de cerrar, no hacer nada
            if (this.IsDisposed || !this.IsHandleCreated)
            {
                timerActualizacion.Stop();
                timerActualizacion.Dispose();
                return;
            }

            try
            {
                int? idLocalidad = null;
                int? numeroMesa = null;

                if (cbbLocalidades.SelectedItem is Localidad loc && loc.Id > 0) // Verifica si hay una localidad seleccionada
                    idLocalidad = loc.Id;

                if (cbbMesa.Visible && cbbMesa.SelectedItem != null && int.TryParse(cbbMesa.SelectedItem.ToString(), out int mesa)) // Verifica si hay una mesa seleccionada
                    numeroMesa = mesa;

                CargarGrafico(idLocalidad, numeroMesa);
            }
            catch (Exception ex)
            {
                // Opcional: loguear o ignorar errores si el control ya no está accesible
            }
        }

        /* este metodo se ejecuta cuando el formulario se está cerrando.
         * Detiene el timer y remueve el evento para evitar que siga disparando.
         */
        private void FrmEstadisticas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timerActualizacion != null) // Verifica si el timer está inicializado
            {
                timerActualizacion.Stop();  // Detiene el timer para evitar que siga disparando eventos
                timerActualizacion.Tick -= TimerActualizacion_Tick; // Remueve el evento para evitar que siga disparando
                timerActualizacion.Dispose();   // Libera los recursos del timer
                timerActualizacion = null;  // Asigna null para evitar referencias posteriores
            }
        }
        /* metodo que carga las localidades en el combo box.
         * Obtiene la lista de localidades desde la base de datos y las asigna al combo box.
         * Agrega una opción "Mostrar todos" al inicio de la lista.
         */
        private void CargarLocalidades()
        {
            List<Localidad> localidades = new List<Localidad>();
            ClienteDAL clienteDAL = new ClienteDAL();

            localidades = clienteDAL.ObtenerLocalidades();
            localidades.Insert(0, new Localidad(0, "Mostrar todos", 0));
            cbbLocalidades.DataSource = null;
            cbbLocalidades.DataSource = localidades;
        }
        /* metodo que carga el gráfico de votos.
         * Si se recibe un id de localidad y/o número de mesa, los usa para filtrar los datos.
         * Si no, carga todos los votos.
         * Configura el gráfico como un gráfico de tipo "Pie" y muestra los porcentajes de cada candidato.
         */

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
        /* metodo que configura el gráfico.
         * Limpia las series, títulos y áreas del gráfico.
         * Crea un área de gráfico transparente y una serie de tipo "Pie" para mostrar los votos.
         * Configura la serie para mostrar los valores como etiquetas y establece el estilo de fuente.
         */
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
            if (cbbLocalidades.SelectedIndex == 0)
            {
                cbbMesa.Visible = false;
                cbbMesa.SelectedIndex = -1;
                cbbMesa.DataSource = null;
                CargarGrafico();
            }
            else if (cbbLocalidades.SelectedItem is Localidad loc && loc.Id > 0)
            {
                CargarGrafico(loc.Id);
                CargarMesasParaLocalidad(loc.Id);
                cbbMesa.Visible = true;
                cbbMesa.SelectedIndex = 0;
            }
        }

        private void cbbMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMesa.SelectedItem == null)
            {
                CargarGrafico(((Localidad)cbbLocalidades.SelectedItem).Id);
                cbbMesa.Visible = false;
            }
            else if (!string.IsNullOrEmpty((string)cbbMesa.SelectedItem))
            {
                int mesa = Convert.ToInt32(cbbMesa.SelectedItem);
                CargarGrafico(((Localidad)cbbLocalidades.SelectedItem).Id, mesa);
            }
        }

        private void CargarMesasParaLocalidad(int idLocalidad)
        {
            ClienteDAL clienteDAL = new ClienteDAL();
            int cantidadMesas = clienteDAL.ContarMesasPorLocalidad(idLocalidad);

            List<string> listaMesas = new List<string>();

            listaMesas.Add(string.Empty); // Opción vacía al inicio

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

            int itemsToShow = Math.Min(cb.Items.Count, 5);
            int itemHeight = cb.ItemHeight;
            int border = SystemInformation.BorderSize.Height;

            cb.DropDownHeight = (itemHeight * itemsToShow) + border * 2;
        }

        private void cbbLocalidades_DropDown(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            int itemsToShow = Math.Min(cb.Items.Count, 5);
            int itemHeight = cb.ItemHeight;
            int border = SystemInformation.BorderSize.Height;

            cb.DropDownHeight = (itemHeight * itemsToShow) + border * 2;
        }

        // No olvides enlazar FormClosing para limpiar timer:
        private void FrmEstadisticas_Shown(object sender, EventArgs e)
        {
            this.FormClosing += FrmEstadisticas_FormClosing;    // Esto asegura que el timer se detenga y limpie al cerrar el formulario.
        }
    }
}
