using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Credencial
    {
        private string _legajo;
        private string _nombreUsuario;
        private string _contrasena;
        private DateTime _fechaAlta;
        private DateTime _fechaUltimoLogin;

        public string Legajo
        {
            get => _legajo;
            set => _legajo = value;
        }

        public string NombreUsuario
        {
            get => _nombreUsuario;
            set => _nombreUsuario = value;
        }

        public string Contrasena
        {
            get => _contrasena;
            set => _contrasena = value;
        }

        public DateTime FechaAlta
        {
            get => _fechaAlta;
            set => _fechaAlta = value;
        }

        public DateTime FechaUltimoLogin
        {
            get => _fechaUltimoLogin;
            set => _fechaUltimoLogin = value;
        }

        public Credencial(string registro)
        {
            string[] datos = registro.Split(';');

            _legajo = datos[0];
            _nombreUsuario = datos[1];
            _contrasena = datos[2];
            _fechaAlta = DateTime.ParseExact(datos[3], "d/M/yyyy", CultureInfo.InvariantCulture);

            if (string.IsNullOrWhiteSpace(datos[4]))
            {
                _fechaUltimoLogin = DateTime.MinValue;
            }
            else
            {
                _fechaUltimoLogin = DateTime.ParseExact(datos[4], "d/M/yyyy", CultureInfo.InvariantCulture);
            }
        }

        public override string ToString()
        {
            return $"{Legajo};{NombreUsuario};{Contrasena};{FechaAlta:dd/MM/yyyy};{FechaUltimoLogin:dd/MM/yyyy}";
        }
    }
}

