using Datos;
using Datos.Login;
using Persistencia;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class LoginPerfil
    {
        DataBaseUtils dataBaseUtils = new DataBaseUtils();
        PerfilPersistencia perfilPersistencia = new PerfilPersistencia();
        string archivoCsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\");



        public string BuscarLegajo(Credencial credencial)
            {
                List<string> registros = dataBaseUtils.BuscarRegistro("usuario_perfil.csv");
                

                foreach (string linea in registros)
                {
                    string[] columnas = linea.Split(';');
                    if (columnas.Length > 0 && columnas[0].Trim() == credencial.Legajo)
                    {
                        return columnas[1].Trim(); // Se encontró el legajo
                    }
                }

                return null; // No se encontró el legajo
            }
         
        public string ObtenerNombrePerfil(string idPerfil)
        {
            List<string> lineas = perfilPersistencia.LeerArchivo("perfil.csv");

            foreach (string linea in lineas)
            {
                string[] campos = linea.Split(';');
                if (campos.Length >= 2 && campos[0] == idPerfil)
                    return campos[1];
            }

            return null;
        }
      
            public List<string> ObtenerIdRoles(string idPerfil)
            {
                List<string> roles = new List<string>();
                List<string> lineas = perfilPersistencia.LeerArchivo("perfil_rol.csv");

                foreach (string linea in lineas)
                {
                    string[] campos = linea.Split(';');
                    if (campos.Length >= 2 && campos[0] == idPerfil)
                        roles.Add(campos[1]);
                }

                return roles;
            }
            public string ObtenerNombreRol(string idRol)
             {
                  List<string> lineas = perfilPersistencia.LeerArchivo("rol.csv");

                    foreach (string linea in lineas)
                      {
                        string[] campos = linea.Split(';');
                         if (campos.Length >= 2 && campos[0] == idRol)
                         return campos[1];
                      }

            return null;
             }

        public string[] BuscarPersonaPorLegajo(string legajoBuscado)
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\", "persona.csv");

            if (!File.Exists(rutaArchivo))
                return null;

            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                sr.ReadLine(); // salteo cabecera

                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] campos = linea.Split(';');

                    if (campos.Length >= 5 && campos[0].Trim() == legajoBuscado.Trim())
                    {
                        return campos;
                    }
                }
            }

            return null;
        }
    }
    

}
