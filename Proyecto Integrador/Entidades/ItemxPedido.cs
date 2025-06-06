using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Entidades
{
    public class ItemxPedido
    {

        public int Id_item {  get; set; }

        public int Id_Insumo { get; set; }

        public int Cantidad { get; set; }

        public float Total { get; set; }


    }
}
