using Datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class PerfilPersistencia
    {
        
        public List<string> LeerArchivo(string nombreArchivo)
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\", nombreArchivo);
            List<string> lineas = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        lineas.Add(linea);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error leyendo archivo: " + e.Message);
            }

            return lineas;
        }

    }
}

