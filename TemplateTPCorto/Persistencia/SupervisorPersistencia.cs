using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class SupervisorPersistencia
    {
        public void agregarsolicitud(String credencialacambiar)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            string rutaarchivo = "operacion_cambio_credencial.csv";
            dataBaseUtils.AgregarRegistro(rutaarchivo, credencialacambiar);

        }
    }
}
