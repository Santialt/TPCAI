using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class SupervisorPersistencia
    {
        public void agregarsolicitud(String credencialacambiar,string nombrearchivo)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            string NombreArchivo = nombrearchivo;
            dataBaseUtils.RegistrarLineaEnArchivo(NombreArchivo, credencialacambiar);  

        }
       
    }
}
