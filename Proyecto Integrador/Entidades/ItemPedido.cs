using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Entidades
{
    public class ItemPedidos
    {

        public int Id_item {  get; set; }
        public int Id_Pedido { get; set; }

        public string Sku { get; set; }

        public int Cantidad { get; set; }

        public float Total { get; set; }


    }
}
