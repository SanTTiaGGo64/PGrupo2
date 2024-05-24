using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryReservas.Models
{
    public class usuarioModel
    {
        public int IdUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public string telefono { get; set; }
        public int estado { get; set; }

    }
}
