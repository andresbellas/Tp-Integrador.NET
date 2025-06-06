using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedido
    {
        public int Id_pedido { get; set; }
        public Mesa MesaAsignada { get; set; }
        public int id_estado { get; set; }

        public List<ItemxPedido> ItemsPedidos { get; set; }

        public DateTime fechaPedido { get; set; }


    }
}
