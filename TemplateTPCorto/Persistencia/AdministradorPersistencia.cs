using Datos;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class AdministradorPersistencia
    {
       
        public List<string> obtenerdatos(string nombrearchivo)
        {
            DataBaseUtils datos = new DataBaseUtils();
            string nombreArchivo = "autorizacion.csv";
            List<string> lineas = datos.BuscarRegistro(nombreArchivo);
            return lineas;
        }

    }
}
