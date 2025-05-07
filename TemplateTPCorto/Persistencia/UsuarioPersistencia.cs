using Datos;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class UsuarioPersistencia
    {
        public Credencial login(String username)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            string linea = dataBaseUtils.BuscarUsuario("credenciales.csv", username);

            if (string.IsNullOrEmpty(linea))
                return null;

            return new Credencial(linea);
        }
    }
}
