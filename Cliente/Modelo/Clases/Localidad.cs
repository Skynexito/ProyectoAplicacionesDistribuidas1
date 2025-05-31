using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Modelo.Clases
{
    public class Localidad
    {
        // Clase que representa una localidad con un identificador, nombre y cantidad de mesas.
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadMesas { get; set; }
        // Constructor por defecto
        public Localidad() { }
        // Constructor con parámetros para inicializar las propiedades
        public Localidad(int id, string nombre, int numeroMesas)
        {
            Id = id;
            Nombre = nombre;
            CantidadMesas = numeroMesas;
        }
        // Método para obtener una representación en cadena de la localidad

        public override string ToString()
        {
            return $"{Id}: {Nombre}";
        }
        // Método para crear una instancia de Localidad a partir de una cadena
        public static Localidad FromString(string data)
        {
            string[] partes = data.Trim().Split(',');
            return new Localidad(
                int.Parse(partes[0]),
                partes[1],
                int.Parse(partes[2])
            );
        }
    }
}
