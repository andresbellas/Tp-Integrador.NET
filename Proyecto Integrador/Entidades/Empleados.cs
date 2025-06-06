using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleados
    {
        public int id_empleado { get; set; }
        public int Nro_Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Usuarios UsuarioEmpleado { get; set; }
        public Rol RolEmpleado { get; set; }
        public bool baja{ get; set; }


    }
}
