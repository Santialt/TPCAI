﻿using Datos;
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
        public Credencial login(string username)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();
            string linea = dataBaseUtils.BuscarUsuario("credenciales.csv", username);

            if (string.IsNullOrEmpty(linea))
                return null;

            return new Credencial(linea);
        }

        public void actualizarPrimerLogin(Credencial credencial)
        {
            DataBaseUtils dataBaseUtils = new DataBaseUtils();

            // Borra la línea vieja del usuario
            dataBaseUtils.BorrarRegistro(credencial.Legajo, "credenciales.csv");

            // Agrega la línea nueva con datos actualizados
            dataBaseUtils.AgregarRegistro("credenciales.csv", credencial.ToString());
        }
    }
}
