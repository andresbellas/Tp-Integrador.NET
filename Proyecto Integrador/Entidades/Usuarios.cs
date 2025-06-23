using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Usuarios
    {
        public int Id_Usuario {  get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}
