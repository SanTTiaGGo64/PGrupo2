using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryReservas.Models
{
    public class Pago
    {
        public int idPago {  get; set; }
        public int idReserva { get; set; }
        public decimal montoPagado { get; set; }
        public decimal montoRestante { get; set; }
        public DateTime fechaPago { get; set; }
        public string metodoPago { get; set; }

    }
}
