﻿using System;
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
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string FechaAlta{ get; set; }
        public string FechaUltimoLogin { get; set; }
    }
}
