using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MedioDePago
    {

        public int Id_Pago { get; set; }        

        public string Nombre_Pago { get; set; }

        public string Descripcion { get; set; }

        public bool habilitado { get; set; }
    }
}
