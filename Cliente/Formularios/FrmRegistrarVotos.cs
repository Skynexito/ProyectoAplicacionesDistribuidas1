using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cliente.Modelo.Clases;    // Clases que representan las entidades de la aplicación
using Cliente.Modelo.ClienteTCP;    // Cliente TCP para la comunicación con el servidor

namespace Servidor.Formularios
{
    public partial class FrmRegistrarVotos : Form
    {
        // Variables para almacenar las opciones de votación, la localidad, el número máximo de votantes y el cliente TCP
        private List<Opcion> listaOpciones = new List<Opcion>();
        private Localidad localidad;
        private int maxVotantes;
        private ClienteTCP clienteTCP;
        
        public FrmRegistrarVotos()
        {
            InitializeComponent();
        }
        // Constructor que recibe una localidad y un número máximo de votantes, además del cliente TCP para la comunicación con el servidor
        public FrmRegistrarVotos(Localidad localidad, int maxVotantes, ClienteTCP client)
        {
            InitializeComponent();
            this.localidad = localidad;
            this.maxVotantes = maxVotantes;
            this.clienteTCP = client;
            numMesa.Maximum = localidad.CantidadMesas;
        }
        /** 
         * Evento que se ejecuta al cargar el formulario. 
         * Envía un comando al servidor para solicitar las opciones disponibles, 
         * recibe la respuesta, la parsea y configura la tabla de votos para que el usuario pueda ingresar datos.
         */
        private async void FrmRegistrarVotos_Load(object sender, EventArgs e)
        {
            await clienteTCP.EnviarComandoAsync("EnviarOpciones"); // Enviar comando al servidor para obtener las opciones de votación
            string data = await clienteTCP.LeerRespuestaAsync(); // Leer la respuesta del servidor con las opciones
            // Parsear las opciones recibidas y almacenarlas en la lista
            listaOpciones = ParsearOpciones(data);
            // Configurar la tabla de votos con las opciones obtenidas
            ConfigurarTabla();
        }
        /** 
         * Método que se ejecuta al hacer clic en el botón de salida, 
         * cerrando el formulario actual.
         */
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /** 
         * Evento que se ejecuta al hacer clic en el botón para guardar el número de votantes. 
         * Recorre las filas de la tabla de votos, valida que las cantidades sean números válidos y no negativos, 
         * acumula el total de votos y verifica que coincida con el número máximo esperado. 
         * Si todo es correcto, construye un comando con los datos y lo envía al servidor para registrar los votos. 
         * Muestra mensajes de éxito o error según la respuesta del servidor.
         */
        private async void btnGuardarNumeroVotantes_Click(object sender, EventArgs e)
        {
            // se crea una lista para almacenar los votos
            List<Votos> votosList = new List<Votos>();
            // se crea una instancia de Mesa con el número de mesa y la localidad
            Mesa mesaActual = new Mesa();
            mesaActual.NMesa = Convert.ToInt32(numMesa.Value);
            mesaActual.Localidad = localidad;
            int sumaVotos = 0;

            foreach (DataGridViewRow row in dgvVotos.Rows)  // Recorre cada fila de la tabla de votos
            {
                if (row.IsNewRow) continue; // Ignora la fila nueva al final de la tabla

                int idOpcion = Convert.ToInt32(row.Cells["IdOpcion"].Value);    // Obtiene el ID de la opción de la fila actual
                string nombreCandidato = row.Cells["Candidato"].Value?.ToString(); // Obtiene el nombre del candidato de la fila actual
                string cantidadStr = row.Cells["Cantidad"].Value?.ToString();   // Obtiene la cantidad de votos de la fila actual

                if (int.TryParse(cantidadStr, out int cantidad) && cantidad >= 0)   // Verifica si la cantidad es un numero valido y no negativo
                {
                    sumaVotos += cantidad;  // Acumula la cantidad de votos
                    Opcion opcion = listaOpciones.Find(o => o.Id == idOpcion);      // Busca la opción correspondiente en la lista de opciones
                    Votos voto = new Votos(0, cantidad, mesaActual, opcion);        // Crea un nuevo objeto Votos con los datos de la fila actual
                    votosList.Add(voto);    // Agrega el voto a la lista de votos
                }
                else // Si la cantidad no es válida, muestra un mensaje de error
                {
                    MessageBox.Show($"Cantidad inválida en candidato: {nombreCandidato}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // Validación de cantidad total de votos, asegura que la suma de votos coincida con el número máximo de votantes esperado
            if (sumaVotos != maxVotantes)
            {
                MessageBox.Show($"El número de votos contados ({sumaVotos}) no concuerda con el número esperado ({maxVotantes}) para esta mesa.",
                                "Votos inconsistentes",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            // Validación de votos ingresados, asegura que al menos un voto válido haya sido ingresado
            if (votosList.Count == 0)
            {
                MessageBox.Show("Debe ingresar al menos un voto válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Construcción del comando para enviar al servidor
            string comando = $"RegistrarVotos|{mesaActual.NMesa},{localidad.Id}|"; // asegúrate de tener esos objetos
            foreach (var v in votosList)    // Recorre la lista de votos y construye el comando
            {
                comando += $"{v.Opcion.Id},{v.Cantidad};";  // Formato: "IdOpcion,Cantidad;"
            }

            await clienteTCP.EnviarComandoAsync(comando);   // Enviar el comando al servidor para registrar los votos, await aqui es importante para esperar la respuesta del servidor
            string respuesta = await clienteTCP.LeerRespuestaAsync();   // Leer la respuesta del servidor

            if (respuesta == "OK")      // Si la respuesta del servidor es "OK", significa que los votos se registraron correctamente
                MessageBox.Show("Votos registrados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else // Si la respuesta no es "OK", muestra un mensaje de error con la respuesta del servidor
                MessageBox.Show("Error al registrar votos: " + respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /** 
         * Configura la tabla de votos (DataGridView) para mostrar las opciones de votación. 
         * Define las columnas, oculta el ID, establece los encabezados, y agrega las filas correspondientes a cada opción. 
         * Además, aplica estilos visuales para mejorar la apariencia.
         */
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

        /** 
         * Método que recibe una cadena con los datos de opciones separados por punto y coma, 
         * la parsea y devuelve una lista de objetos Opcion con los datos correspondientes.
         */
        private List<Opcion> ParsearOpciones(string data)
        {
            List<Opcion> lista = new List<Opcion>();    // Lista para almacenar las opciones parseadas
            string[] partes = data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);     // Divide la cadena en partes usando el punto y coma como separador
            
            foreach (string parte in partes)    // Recorre cada parte de la cadena
            {
                string[] campos = parte.Split(','); // Divide cada parte en campos usando la coma como separador
                int id = int.Parse(campos[0]);  // Obtiene el ID de la opción
                string nombreCandidato = campos[1]; // Obtiene el nombre del candidato
                lista.Add(new Opcion(id, "Lista", nombreCandidato)); // Crea un nuevo objeto Opcion y lo agrega a la lista
            }

            return lista; // Devuelve la lista de opciones parseadas
        }

        private void numMesa_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
