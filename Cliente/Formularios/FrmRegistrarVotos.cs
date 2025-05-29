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
using Cliente.Modelo.ClienteTCP;

namespace Servidor.Formularios
{
    public partial class FrmRegistrarVotos : Form
    {
        private List<Opcion> listaOpciones = new List<Opcion>();
        private Localidad localidad;
        private int maxVotantes;
        private ClienteTCP clienteTCP;
        
        public FrmRegistrarVotos()
        {
            InitializeComponent();
        }

        public FrmRegistrarVotos(Localidad localidad, int maxVotantes, ClienteTCP client)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.maxVotantes = maxVotantes;
            this.clienteTCP = client;
            numMesa.Maximum = localidad.CantidadMesas;
        }

        private async void FrmRegistrarVotos_Load(object sender, EventArgs e)
        {
            await clienteTCP.EnviarComandoAsync("EnviarOpciones"); // comando al servidor
            string data = await clienteTCP.LeerRespuestaAsync();

            listaOpciones = ParsearOpciones(data);
            ConfigurarTabla();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnGuardarNumeroVotantes_Click(object sender, EventArgs e)
        {
            List<Votos> votosList = new List<Votos>();
            Mesa mesaActual = new Mesa();
            mesaActual.NMesa = Convert.ToInt32(numMesa.Value);
            mesaActual.Localidad = localidad;
            int sumaVotos = 0;

            foreach (DataGridViewRow row in dgvVotos.Rows)
            {
                if (row.IsNewRow) continue;

                int idOpcion = Convert.ToInt32(row.Cells["IdOpcion"].Value);
                string nombreCandidato = row.Cells["Candidato"].Value?.ToString();
                string cantidadStr = row.Cells["Cantidad"].Value?.ToString();

                if (int.TryParse(cantidadStr, out int cantidad) && cantidad >= 0)
                {
                    sumaVotos += cantidad;
                    Opcion opcion = listaOpciones.Find(o => o.Id == idOpcion);
                    Votos voto = new Votos(0, cantidad, mesaActual, opcion); // asegúrate de tener mesaActual
                    votosList.Add(voto);
                }
                else
                {
                    MessageBox.Show($"Cantidad inválida en candidato: {nombreCandidato}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // Validación de cantidad total de votos
            if (sumaVotos != maxVotantes)
            {
                MessageBox.Show($"El número de votos contados ({sumaVotos}) no concuerda con el número esperado ({maxVotantes}) para esta mesa.",
                                "Votos inconsistentes",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (votosList.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un voto válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Construir mensaje para el servidor
            string comando = $"RegistrarVotos|{mesaActual.NMesa},{localidad.Id}|"; // ← asegúrate de tener esos objetos
            foreach (var v in votosList)
            {
                comando += $"{v.Opcion.Id},{v.Cantidad};";
            }

            await clienteTCP.EnviarComandoAsync(comando);
            string respuesta = await clienteTCP.LeerRespuestaAsync();

            if (respuesta == "OK")
                MessageBox.Show("Votos registrados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error al registrar votos: " + respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void ConfigurarTabla()
        {
            dgvVotos.Columns.Clear();
            dgvVotos.Rows.Clear();

            // ID oculto
            var colId = new DataGridViewTextBoxColumn();
            colId.Name = "IdOpcion";
            colId.HeaderText = "ID";
            colId.Visible = false;
            dgvVotos.Columns.Add(colId);

            // Candidato (solo lectura)
            var colCandidato = new DataGridViewTextBoxColumn();
            colCandidato.Name = "Candidato";
            colCandidato.HeaderText = "Candidato";
            colCandidato.ReadOnly = true;
            dgvVotos.Columns.Add(colCandidato);

            // Votos (editable)
            var colCantidad = new DataGridViewTextBoxColumn();
            colCantidad.Name = "Cantidad";
            colCantidad.HeaderText = "Votos";
            dgvVotos.Columns.Add(colCantidad);

            // Agregar filas
            foreach (var opcion in listaOpciones)
            {
                dgvVotos.Rows.Add(opcion.Id, opcion.Candidato, ""); // votos vacíos inicialmente
            }
            colCandidato.SortMode = DataGridViewColumnSortMode.NotSortable;
            colCantidad.SortMode = DataGridViewColumnSortMode.NotSortable;

            // Desactiva los estilos visuales automáticos del tema
            dgvVotos.EnableHeadersVisualStyles = false;

            // Estilo para el encabezado
            dgvVotos.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvVotos.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvVotos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvVotos.ColumnHeadersHeight = 30;

            // Opcional: mejora estética general si estás usando temas
            dgvVotos.ThemeStyle.HeaderStyle.BackColor = Color.White;
            dgvVotos.ThemeStyle.HeaderStyle.ForeColor = Color.Black;
            dgvVotos.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);



        }


        private List<Opcion> ParsearOpciones(string data)
        {
            List<Opcion> lista = new List<Opcion>();
            string[] partes = data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //MessageBox.Show("Datos recibidos:\n" + data);

            foreach (string parte in partes)
            {
                string[] campos = parte.Split(',');

                int id = int.Parse(campos[0]);
                string nombreCandidato = campos[1];
                lista.Add(new Opcion(id, "Lista", nombreCandidato)); // Lista puede venir luego del servidor
            }

            return lista;
        }

        private void numMesa_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
