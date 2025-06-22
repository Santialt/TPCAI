using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
   public class OperacionCambioCredencial
    {
        public string IdCambio { get; set; }
        public string Legajo { get; set; }
        public string Usuario { get; set; }
        public string NuevaClave { get; set; }
        public string FechaSolicitud { get; set; }
    }
}
