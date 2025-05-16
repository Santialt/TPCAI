using System;
using Datos;
using Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PrimerLogin
    {
        public void actualizarPrimerLogin(Credencial credencial, out string mensaje)
        {
            UsuarioPersistencia usuarioPersistencia = new UsuarioPersistencia();
            usuarioPersistencia.actualizarPrimerLogin(credencial);

            mensaje = "Cambio de contraseña exitoso. Login exitoso.";

            return;
        }
    }
}
