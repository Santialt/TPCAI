using System;
using System.Collections.Generic;
using System.IO;

namespace Negocio
{
    public class CambioContrasenaNegocio
    {
        private readonly string _archivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\credenciales.csv");

        public bool DebeCambiarPassword(DateTime ultimoLogin)
        {
            return (DateTime.Now - ultimoLogin).TotalDays > 30 || ultimoLogin == DateTime.MinValue;
        }

        public string CambiarPassword(string nombreUsuario, string nuevaPassword, out bool cambioExitoso)
        {
            cambioExitoso = false;

            if (!File.Exists(_archivo))
                return "El archivo de credenciales no existe.";

            var lineas = File.ReadAllLines(_archivo);
            var nuevasLineas = new List<string>();
            bool encontrado = false;

            foreach (var linea in lineas)
            {
                var campos = linea.Split(';');
                if (campos.Length < 5)
                {
                    nuevasLineas.Add(linea);
                    continue;
                }

                if (campos[1].Trim() == nombreUsuario.Trim())
                {
                    string passActual = campos[2];
                    if (nuevaPassword.Length < 8)
                        return "La contraseña debe tener al menos 8 caracteres.";
                    if (nuevaPassword == passActual)
                        return "La nueva contraseña no puede ser igual a la anterior.";

                    campos[2] = nuevaPassword;
                    campos[4] = DateTime.Now.ToString("dd/MM/yyyy"); // actualiza fecha último login
                    nuevasLineas.Add(string.Join(";", campos));
                    encontrado = true;
                    cambioExitoso = true;
                }
                else
                {
                    nuevasLineas.Add(linea);
                }
            }

            if (!encontrado)
                return "Usuario no encontrado.";

            File.WriteAllLines(_archivo, nuevasLineas);
            return "Contraseña actualizada correctamente.";
        }
    }
}
