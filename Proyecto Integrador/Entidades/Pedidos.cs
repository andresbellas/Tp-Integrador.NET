using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedidos
    {
        public int Id_pedido { get; set; }
        public int Nro_Pedido { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public Mesa MesaAsignada { get; set; }
        public int Id_Estado { get; set; }

        public List<ItemPedidos> ItemsPedidos { get; set; }



    }
}
