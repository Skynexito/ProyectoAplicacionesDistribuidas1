using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Modelo.Clases
{
    public enum OpcionesDefault {
    Blanco,
    Nulo
    }

     public class Opcion
    {
        private int id;
        private string nombreCandidato;

        public Opcion()
        {
            id = 0;
            nombreCandidato = "candidato";
        }
        public Opcion(int id, string nombreCandidato)
        {
            this.id = id;
            this.nombreCandidato = nombreCandidato;
        }

        public int Id { get { return id; } set { id = value; } }
        public string Candidato { get { return nombreCandidato; } set { nombreCandidato= value; } }

        public override string ToString()
        {
            return $" Candidato: {Candidato}";
        }
    }
}
