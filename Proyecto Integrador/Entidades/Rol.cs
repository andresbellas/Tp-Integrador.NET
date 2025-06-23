using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Rol
    {
        public int id_rol {get; set;}

        public string Nombre_rol {get; set;}

        public string Descripcion {get; set;}

        public bool Baja { get; set;}
    }
}
