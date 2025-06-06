using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleado
    {
        public int id_empleado { get; set; }
        public int NumeroLegajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Usuario UsuarioEmpleado { get; set; }
        public Rol RolEmpleado { get; set; }
        public bool baja{ get; set; }


    }
}
