using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Modelo.Clases
{
    public class Mesa
    {
        private int id;
        private int nMesa;
        private bool estado;


        // Constructor por defecto
        public Mesa()
        {
            id = 0;
            nMesa = 0;
            estado = true;
        }

        // Constructor con parámetros para inicializar las propiedades
        public Mesa(int id, int nMesa, bool estado)
        {
            this.id = id;
            this.nMesa = nMesa;
            this.estado = estado;
        }

        // Propiedades para acceder a los campos privados
        public int Id {  get { return id; } set { id = value; } }
        public int NMesa { get { return nMesa; } set { nMesa = value; } }
        public bool IsEstado { get { return estado; } set { estado = value; } }

        // Método para crear una instancia de Mesa a partir de una cadena
        public override string ToString() {
            return $"{NMesa}";
        }
    }
}
