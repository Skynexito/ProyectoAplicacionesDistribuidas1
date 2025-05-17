using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Modelo.Clases
{
    public class Localidad
    {
        private int id;
        private string nombre;
        private int cantidadMesas;
        private List<Mesa> mesas;

        public Localidad()
        {
            id = 0;
            nombre = "nombre";
            cantidadMesas = 0;
            mesas = new List<Mesa>();
        }

        public Localidad(int id, string nombre, int numeroMesas)
        {
            this.id = id;
            this.nombre = nombre;
            this.cantidadMesas = numeroMesas;
            this.mesas = null;
        }

        public int Id { get { return id; } set { id = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public int CantidadMesas { get { return cantidadMesas; } set { cantidadMesas = value; } }
        public List<Mesa> Mesas { get { return mesas; } set { mesas = value; } }

        public override string ToString() {
            return $"{Id}. {Nombre}";
        }

    }
}
