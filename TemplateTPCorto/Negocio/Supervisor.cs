using Persistencia;
using Persistencia.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Supervisor
    {
        SupervisorPersistencia supervisorPersistencia = new SupervisorPersistencia();   
        public void AgregarSolicitud(string credencialacambiar)
        {
            supervisorPersistencia.agregarsolicitud(credencialacambiar);
        }
        


    }
}
