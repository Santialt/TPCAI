using Datos;
using Persistencia;
using System;
using System.Collections.Generic;
using System.IO;

namespace Negocio
{
    public class LoginNegocio
    {
        private string rutaIntentos = @"./login_intentos.csv";
        private string rutaBloqueados = @"./usuario_bloqueado.csv";

        public Credencial login(string usuario, string password, out string mensaje)
        {
            mensaje = "";

            // Verificar si está bloqueado
            if (EstaBloqueado(usuario))
            {
                mensaje = "El usuario está bloqueado.";
                return null;
            }

            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            Credencial credencial = usuarioPersistencia.login(usuario);

            if (credencial == null)
            {
                mensaje = "Usuario no encontrado.";
                return null;
            }

            if (credencial.Contrasena.Equals(password))
            {
                ResetearIntentos(usuario);
                mensaje = "Login exitoso.";
                return credencial;
            }
            else
            {
                RegistrarIntentoFallido(usuario, out bool bloqueado);
                mensaje = bloqueado ? "Usuario bloqueado por 3 intentos fallidos." : "Contraseña incorrecta.";
                return null;
            }
        }

        private bool EstaBloqueado(string usuario)
        {
            if (!File.Exists(rutaBloqueados)) return false;
            var lineas = File.ReadAllLines(rutaBloqueados);
            foreach (var linea in lineas)
            {
                if (linea.Trim() == usuario) return true;
            }
            return false;
        }

        private void RegistrarIntentoFallido(string usuario, out bool seBloqueo)
        {
            seBloqueo = false;
            Dictionary<string, int> intentos = new Dictionary<string, int>();

            if (File.Exists(rutaIntentos))
            {
                var lineas = File.ReadAllLines(rutaIntentos);
                foreach (var linea in lineas)
                {
                    var partes = linea.Split(';');
                    if (partes.Length >= 2)
                    {
                        string user = partes[0];
                        int cant = int.Parse(partes[1]);
                        intentos[user] = cant;
                    }
                }
            }

            if (intentos.ContainsKey(usuario))
                intentos[usuario]++;
            else
                intentos[usuario] = 1;

            if (intentos[usuario] >= 3)
            {
                File.AppendAllText(rutaBloqueados, usuario + Environment.NewLine);
                intentos.Remove(usuario);
                seBloqueo = true;
            }

            using (StreamWriter sw = new StreamWriter(rutaIntentos, false))
            {
                foreach (var item in intentos)
                    sw.WriteLine(item.Key + ";" + item.Value);
            }
        }

        private void ResetearIntentos(string usuario)
        {
            if (!File.Exists(rutaIntentos)) return;

            Dictionary<string, int> intentos = new Dictionary<string, int>();
            var lineas = File.ReadAllLines(rutaIntentos);

            foreach (var linea in lineas)
            {
                var partes = linea.Split(';');
                if (partes[0] != usuario)
                    intentos[partes[0]] = int.Parse(partes[1]);
            }

            using (StreamWriter sw = new StreamWriter(rutaIntentos, false))
            {
                foreach (var item in intentos)
                    sw.WriteLine(item.Key + ";" + item.Value);
            }
        }
    }
}
