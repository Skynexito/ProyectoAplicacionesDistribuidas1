using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Modelo.Clases
{
    public class Localidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadMesas { get; set; }

        public Localidad() { }

        public Localidad(int id, string nombre, int numeroMesas)
        {
            Id = id;
            Nombre = nombre;
            CantidadMesas = numeroMesas;
        }

        public override string ToString()
        {
            return $"{Id}: {Nombre}";
        }

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


