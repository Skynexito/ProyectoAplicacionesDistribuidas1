using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Modelo.Clases
{

    public class Opcion
    {
        // Clase que representa una opción de voto en una elección, con un identificador, el nombre de la lista y el nombre del candidato.
        private int id;
        private string nombreLista;
        private string nombreCandidato;

        // Constructores para inicializar la clase Opcion
        public Opcion()
        {
            id = 0;
            nombreLista = "lista";
            nombreCandidato = "candidato";
        }

        // Constructor con parámetros para inicializar las propiedades de la clase Opcion
        public Opcion(int id, string nombreLista, string nombreCandidato)
        {
            this.id = id;
            this.nombreLista = nombreLista;
            this.nombreCandidato = nombreCandidato;
        }

        // Propiedades para acceder a los campos privados de la clase Opcion
        public int Id { get { return id; } set { id = value; } }
        public string NombreLista { get { return nombreLista; } set { nombreLista = value; } }
        public string Candidato { get { return nombreCandidato; } set { nombreCandidato = value; } }

        // Método para crear una instancia de Opcion a partir de una cadena
        public override string ToString()
        {
            return $"Lista: {NombreLista}, Candidato: {Candidato}";
        }
    }
}
