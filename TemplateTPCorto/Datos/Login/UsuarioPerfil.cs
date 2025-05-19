using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Login
{
    public class UsuarioPerfil
    {
        private string Legajo;
        private string NombreUsuario;
        private string Contrasenia;
        private string IdPerfil;
        private string NombrePerfil;
        private List<string> Roles = new List<string>();

        public string Legajo1 { get => Legajo; set => Legajo = value; }
        public string NombreUsuario1 { get => NombreUsuario; set => NombreUsuario = value; }
        public string Contrasenia1 { get => Contrasenia; set => Contrasenia = value; }
        public string IdPerfil1 { get => IdPerfil; set => IdPerfil = value; }
        public string NombrePerfil1 { get => NombrePerfil; set => NombrePerfil = value; }
        public List<string> Roles1 { get => Roles; set => Roles = value; }
       
  
    }
}
