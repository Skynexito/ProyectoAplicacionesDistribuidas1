using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Modelo.Clases
{
    public enum OpcionesDefault {
    Blanco,
    Nulo
    }

     public class Opcion
    {
        private int id;
        private string nombreLista;
        private string nombreCandidato;

        public Opcion()
        {
            id = 0;
            nombreLista = "lista";
            nombreCandidato = "candidato";
        }
        public Opcion(int id, string nombreLista, string nombreCandidato)
        {
            this.id = id;
            this.nombreLista = nombreLista;
            this.nombreCandidato = nombreCandidato;
        }

        public int Id { get { return id; } set { id = value; } }
        public string NombreLista {  get { return nombreLista; } set { nombreLista = value; } }
        public string Candidato { get { return nombreCandidato; } set { nombreCandidato= value; } }

        public override string ToString()
        {
            return $"Lista: {NombreLista}, Candidato: {Candidato}";
        }
    }
}
