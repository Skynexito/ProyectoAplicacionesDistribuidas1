using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Modelo.Clases
{
    public class Votos
    {
        private int id;
        private int cantidad;
        private Mesa mesa;
        private Opcion opcion;

        public Votos() {
            id = 0;
            cantidad = 0;
            Mesa mesa = new Mesa();
            Opcion opcion = new Opcion();
        }

        public Votos(int id, int cantidad, Mesa mesa, Opcion opcion)
        {
            this.id = id;
            this.cantidad = cantidad;
            this.mesa = mesa;
            this.opcion = opcion;
        }

        public int Id {  get { return id; } set { id = value; } }
        public int Cantidad { get { return cantidad; } set { cantidad = value; } }
        public Mesa Mesa { get { return mesa; } set { mesa = value; } }
        public Opcion Opcion { get { return opcion; } set { opcion = value; } }

        public override string ToString()
        {
            return $"{Opcion.NombreLista}: {Cantidad} votos";
        }
    }
}
