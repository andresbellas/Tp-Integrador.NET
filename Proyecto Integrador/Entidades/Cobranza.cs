using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cobranza
    {
        public int id_cobranza { get; set; }

        public int id_pedido { get; set; }

        public float Total { get; set; }

        public MedioDePago MedioDePago { get; set; }

    }
}
