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
            List<string> lineas = datos.BuscarRegistro(nombrearchivo);
            return lineas;
        }
        public void EscribirArchivo(string nombreArchivo, List<string> lineas)
        {
            string ruta = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\", nombreArchivo));
            File.WriteAllLines(ruta, lineas);
        }
    }
}
