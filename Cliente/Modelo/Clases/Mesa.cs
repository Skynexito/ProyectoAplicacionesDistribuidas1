using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Modelo.Clases
{
    public class Mesa
    {
        // Clase que representa una mesa de votación, asociada a una localidad y con un estado (activa/inactiva).
        private int id;
        private int nMesa;
        private bool estado;
        private Localidad localidad;

        // Constructor por defecto
        public Mesa()
        {
            id = 0;
            nMesa = 0;
            estado = true;
            localidad = new Localidad();
        }
        // Constructor con parámetros para inicializar las propiedades de la clase Mesa
        public Mesa(int id, int nMesa, bool estado, Localidad localidad)
        {
            this.id = id;
            this.nMesa = nMesa;
            this.estado = estado;
            this.localidad = localidad;
        }
        // Propiedades para acceder a los campos privados de la clase Mesa
        public int Id {  get { return id; } set { id = value; } }
        public int NMesa { get { return nMesa; } set { nMesa = value; } }
        public bool IsEstado { get { return estado; } set { estado = value; } }
        public Localidad Localidad { get { return localidad; } set {  localidad = value; } }
        // Método para crear una instancia de Mesa a partir de una cadena
        public override string ToString() {
            return $"{NMesa}";
        }
    }
}
