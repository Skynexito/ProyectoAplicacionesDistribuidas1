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
        
        public Mesa()
        {
            id = 0;
            nMesa = 0;
            estado = true;
        }
        public Mesa(int id, int nMesa, bool estado)
        {
            this.id = id;
            this.nMesa = nMesa;
            this.estado = estado;
        }

        public int Id {  get { return id; } set { id = value; } }
        public int NMesa { get { return nMesa; } set { nMesa = value; } }
        public bool IsEstado { get { return estado; } set { estado = value; } }

        public override string ToString() {
            return $"{NMesa}";
        }
    }
}
