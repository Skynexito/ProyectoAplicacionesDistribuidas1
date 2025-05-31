using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Servidor;
using Cliente.Modelo.Clases;
using System.Net.Sockets;
using Cliente.Modelo.ClienteTCP;

namespace Cliente.Formularios
{
    public partial class FrmClienteInicio : Form
    {
        private List<Localidad> localidades = new List<Localidad>();    // Lista para almacenar las localidades recibidas del servidor
        private ClienteTCP clienteTCP = new ClienteTCP();               // Cliente TCP para manejar la conexión con el servidor

        /** 
         * Constructor del formulario FrmClienteInicio.
         * Inicializa los componentes y llama al método asincrónico para conectar al servidor.
         */

        public FrmClienteInicio()
        {
            InitializeComponent();
            // Iniciar la conexión al servidor al cargar el formulario
            _ = ConectarServidorAsync();    // Llamada asincrónica para conectar al servidor sin bloquear la UI
        }

        /** 
         * Método asincrónico que intenta conectar al servidor TCP.
         * En caso de fallo, muestra un diálogo para reintentar o salir.
         * Si la conexión es exitosa, solicita y carga la lista de localidades en el combobox.
         */

        private async Task ConectarServidorAsync()
        {
            bool conectado = false;

            while (!conectado)
            {
                try
                {
                    conectado = await clienteTCP.ConectarAsync("127.0.0.1", 6000);  // Dirección y puerto del servidor

                    if (!conectado)
                        throw new Exception();

                    await clienteTCP.EnviarComandoAsync("EnviarLocalidades");       // Enviar comando al servidor para obtener localidades, await es un operador que espera la tarea asincrónica

                    string data = await clienteTCP.LeerRespuestaAsync();            // Leer la respuesta del servidor que contiene las localidades
                    localidades = ParsearLocalidades(data);                         // Parsear el string recibido en una lista de objetos Localidad

                    cbbLocalidades.Items.Clear();                                   // Limpiar el combobox de localidades antes de agregar nuevas
                    foreach (var loc in localidades)                                // Iterar sobre la lista de localidades y agregarlas al combobox
                    {
                        cbbLocalidades.Items.Add(loc);
                    }
                    cbbLocalidades.SelectedIndex = localidades.Count > 0 ? 0 : -1;  // Seleccionar la primera localidad si hay alguna disponible, de lo contrario dejarlo sin selección
                }
                catch   // Excepción al intentar conectar o leer del servidor
                {
                    // Mostrar un mensaje de error al usuario y preguntar si desea reintentar la conexión
                    DialogResult retry = MessageBox.Show("No se pudo conectar al servidor. ¿Reintentar?", "Conexión fallida",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    // Si el usuario elige no reintentar, salir de la aplicación
                    if (retry == DialogResult.No)
                    {
                        Application.Exit();
                        break;
                    }
                }
            }
        }

        /** 
         * Método asincrónico para intentar reconectar al servidor.
         * Similar a ConectarServidorAsync, pero devuelve true o false según el resultado.
         * En caso de reconexión exitosa, actualiza el combobox de localidades.
         */
        private async Task<bool> IntentarReconectarAsync()
        {
            try
            {
                bool conectado = await clienteTCP.ConectarAsync("127.0.0.1", 6000); // Dirección y puerto del servidor
                if (conectado)
                {
                    await clienteTCP.EnviarComandoAsync("EnviarLocalidades");   // Enviar comando al servidor para obtener localidades
                    string data = await clienteTCP.LeerRespuestaAsync();        // Leer la respuesta del servidor que contiene las localidades
                    localidades = ParsearLocalidades(data);                     // Parsear el string recibido en una lista de objetos Localidad

                    cbbLocalidades.Items.Clear();   // Limpiar el combobox de localidades antes de agregar nuevas
                    foreach (var loc in localidades)    // Iterar sobre la lista de localidades y agregarlas al combobox
                    {
                        cbbLocalidades.Items.Add(loc);
                    }
                    // Seleccionar la primera localidad si hay alguna disponible, de lo contrario dejarlo sin selección
                    cbbLocalidades.SelectedIndex = localidades.Count > 0 ? 0 : -1;

                    return true;
                }
            }
            catch
            {
                // Ignorar excepción aquí, se maneja en llamada
            }
            return false;
        }

        /** 
         * Método para parsear el string recibido del servidor que contiene las localidades.
         * El formato esperado es: "id,nombre,mesas;id,nombre,mesas;..."
         * Devuelve una lista de objetos Localidad con los datos parseados.
         */
        private List<Localidad> ParsearLocalidades(string data)
        {
            List<Localidad> lista = new List<Localidad>();  // Lista para almacenar las localidades parseadas
            string[] partes = data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries); // Dividir el string en partes usando ';' como separador

            // Iterar sobre cada parte del string y parsear los campos
            foreach (string loc in partes)
            {
                // Dividir cada parte en campos usando ',' como separador
                string[] campos = loc.Split(',');
                // Validar que hay al menos 3 campos y que los primeros y terceros son enteros
                if (campos.Length >= 3 &&
                    int.TryParse(campos[0], out int id) &&
                    int.TryParse(campos[2], out int mesas))
                {
                    // Crear una nueva instancia de Localidad con los datos parseados y agregarla a la lista
                    string nombre = campos[1];
                    lista.Add(new Localidad(id, nombre, mesas));
                }
            }

            return lista;
        }

        /**
         * Evento click del botón "Ingresar".
         * Valida la conexión con el servidor y la selección de localidad.
         * Envía un comando PING para verificar conexión activa.
         * En caso exitoso, abre el formulario principal del cliente (FrmCliente) pasando la localidad seleccionada y la conexión TCP.
         */
        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            // Verificar si el cliente TCP está conectado antes de enviar comandos al servidor
            if (clienteTCP == null || !clienteTCP.Conectado)
            {
                // Si el cliente TCP no está conectado, mostrar un mensaje de error y preguntar si desea reconectar
                var reconectar = MessageBox.Show("El servidor no está disponible. ¿Desea intentar reconectar?", "Error de conexión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Si el usuario elige reconectar, intentar reconectar al servidor
                if (reconectar == DialogResult.Yes)
                {
                    // Intentar reconectar al servidor y esperar el resultado
                    bool exito = await IntentarReconectarAsync();
                    // Si la reconexión no fue exitosa, mostrar un mensaje de error y salir del método
                    if (!exito)
                    {
                        MessageBox.Show("No se pudo reconectar al servidor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }// Si el usuario elige no reconectar, simplemente salir del método
                else
                {
                    return;
                }
            }
            // Verificar si el cliente TCP está conectado antes de enviar comandos al servidor
            try
            {
                await clienteTCP.EnviarComandoAsync("PING");                // Enviar un comando PING para verificar la conexión activa
                string respuesta = await clienteTCP.LeerRespuestaAsync();   // Leer la respuesta del servidor

                // Verificar si la respuesta del servidor es "PONG", lo que indica que la conexión está activa
                if (respuesta != "PONG")
                {
                    MessageBox.Show("Respuesta inesperada del servidor.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // Si ocurre un error al enviar el comando o leer la respuesta, mostrar un mensaje de error y salir del método
            catch
            {
                MessageBox.Show("La conexión con el servidor se perdió.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Verificar si se ha seleccionado una localidad en el combobox
            if (cbbLocalidades.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione una localidad.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Obtener la localidad seleccionada del combobox
            Localidad localidad = (Localidad)cbbLocalidades.SelectedItem;
            this.Hide();
            FrmCliente form = new FrmCliente(this, localidad, clienteTCP);  // Crear una nueva instancia del formulario FrmCliente, pasando la instancia actual, la localidad seleccionada y el cliente TCP
            form.ShowDialog();
            this.Show();
        } 
        private void FrmClienteInicio_Load(object sender, EventArgs e)
        {
            // Puedes agregar lógica adicional al cargar el formulario si es necesario
        }
    }
}
