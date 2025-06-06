using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MedioDePago
    {

        public int id_pago { get; set; }        

        public string NombreCliente { get; set; }

        public bool habilitado { get; set; }
    }
}
