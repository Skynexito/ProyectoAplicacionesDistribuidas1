using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Modelo.Clases
{
    public class Mesa
    {
        private int id;
        private int nMesa;
        private bool estado;
        private Localidad localidad;
        
        public Mesa()
        {
            id = 0;
            nMesa = 0;
            estado = true;
            localidad = new Localidad();
        }
        public Mesa(int id, int nMesa, bool estado, Localidad localidad)
        {
            this.id = id;
            this.nMesa = nMesa;
            this.estado = estado;
            this.localidad = localidad;
        }

        public int Id {  get { return id; } set { id = value; } }
        public int NMesa { get { return nMesa; } set { nMesa = value; } }
        public bool IsEstado { get { return estado; } set { estado = value; } }
        public Localidad Localidad { get { return localidad; } set {  localidad = value; } }

        public override string ToString() {
            return $"{NMesa}";
        }
    }
}
