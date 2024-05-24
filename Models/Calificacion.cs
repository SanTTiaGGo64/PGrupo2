using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryReservas.Models
{
    public class Calificacion
    {
        public int idCalificacion {  get; set; }
        public int idReserva { get; set; }
        public int calificacion { get; set; }
        public string comentario { get; set; }
        public DateTime fechaCalificacion { get; set; }
    }
}
