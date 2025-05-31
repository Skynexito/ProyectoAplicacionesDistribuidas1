using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Modelo.Clases
{
    public class Votos
    {
        // Clase que representa un voto en una elección, asociando una mesa y una opción con la cantidad de votos.
        private int id;
        private int cantidad;
        private Mesa mesa;
        private Opcion opcion;
        // Constructores para inicializar la clase Votos
        public Votos() {
            id = 0;
            cantidad = 0;
            Mesa mesa = new Mesa();
            Opcion opcion = new Opcion();
        }
        // Constructor con parámetros para inicializar las propiedades de la clase Votos
        public Votos(int id, int cantidad, Mesa mesa, Opcion opcion)
        {
            this.id = id;
            this.cantidad = cantidad;
            this.mesa = mesa;
            this.opcion = opcion;
        }
        // Propiedades para acceder a los campos privados de la clase Votos
        public int Id {  get { return id; } set { id = value; } }
        public int Cantidad { get { return cantidad; } set { cantidad = value; } }
        public Mesa Mesa { get { return mesa; } set { mesa = value; } }
        public Opcion Opcion { get { return opcion; } set { opcion = value; } }
        // Método para crear una instancia de Votos a partir de una cadena
        public override string ToString()
        {
            return $"{Opcion.NombreLista}: {Cantidad} votos";
        }
    }
}
