using Persistencia;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class OperacionSupervisor
    {
        SupervisorPersistencia supervisorPersistencia = new SupervisorPersistencia();   
        DataBaseUtils dataBaseUtils = new DataBaseUtils();  
        public void AgregarSolicitud(string credencialacambiar,string nombrearchivo)
        {
            supervisorPersistencia.agregarsolicitud(credencialacambiar ,nombrearchivo); // agrega la solicitud al archivo
        }
        public string ObtenerProximoIdCambio(string nombreArchivo)
        {
            List<string> lineas = dataBaseUtils.BuscarRegistro(nombreArchivo);
            int ultimoId = 0;
            if (lineas.Count > 1)
            {
                for (int i = 1; i < lineas.Count; i++)
                {
                    string[] columnas = lineas[i].Split(';');
                    if (columnas.Length > 0 && int.TryParse(columnas[0], out int id)) 
                    {
                        if (id > ultimoId)
                            ultimoId = id;
                    }
                }
            }

          
            return (ultimoId + 1).ToString();  // devuelvo el próximo ID como string
        }

        



    }
}
