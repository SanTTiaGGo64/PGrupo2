using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryReservas.Models
{
    public class Reservas
    {
        public int IdReserva { get; set; }
        public int IdUsuario { get; set; }
        public int IdCancha { get; set; }
        public DateTime FechaReserva { get; set; }
        public int HoraEntrada { get; set; }
        public int HoraSalida { get; set; }
        public int Estado { get; set; }
    }
}
