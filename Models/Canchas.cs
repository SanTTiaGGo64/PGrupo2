using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryReservas.Models
{
    public class Canchas
    {
        public int IdCancha { get; set; }
        public int NumeroCancha { get; set; }
        public string TipoCesped { get; set; }
        public int Capacidad { get; set; }
        public decimal Tarifa { get; set; }
        public int Estado { get; set; }
    }
}
