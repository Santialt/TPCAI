using Persistencia.DataBase;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class OperacionAdministrador
    {
       AdministradorPersistencia AdministradorPersistencia = new AdministradorPersistencia();   
     
        public List<string> Leerarchivo(string nombrearchivo)
        {
            List<string> lineas = AdministradorPersistencia.obtenerdatos(nombrearchivo);
            return lineas;

        }
        public DataTable ConvertirCSVaDataTable(List<string> lineas) // convierte el archivo csv a un DataTable para visualizarlo
        {
            DataTable tabla = new DataTable();

            if (lineas == null || lineas.Count == 0)
                return tabla;
            string[] columnas = lineas[0].Split(';');

            foreach (string columna in columnas)
            {
                tabla.Columns.Add(columna.Trim());
            }
            for (int i = 1; i < lineas.Count; i++)
            {
                string[] valores = lineas[i].Split(';');
                tabla.Rows.Add(valores);
            }

            return tabla;
        }
        public DataView FiltrarPorEstado(DataTable tabla, string estado) // metodo no funciona
        {
            DataView vista = new DataView(tabla);

            if (!string.IsNullOrEmpty(estado) && estado.ToLower() != "todos") // verifica si el estado no es nulo o vacio y si no es "todos"
            {
                vista.RowFilter = $"estado = '{estado}'"; // filtra por estado
            }

            return vista;
        }
        public void ProcesarCambio(string idAutorizacion, string legajoAdministrador)
        {
            var operaciones = AdministradorPersistencia.obtenerdatos("operacion_cambio_credencial.csv");
            var credenciales = AdministradorPersistencia.obtenerdatos("credenciales.csv");
            var autorizaciones = AdministradorPersistencia.obtenerdatos("autorizacion.csv");

            string operacion = operaciones.FirstOrDefault(lee => lee.StartsWith(idAutorizacion + ";")); // busca la operacion en el archivo operacion_cambio_credencial.csv
            if (operacion == null)
                throw new Exception("No se encontró operación para la autorización seleccionada.");

            string[] datosOperacion = operacion.Split(';');
            string legajo = datosOperacion[1].Trim(); //se agregan las variables para el cambio 
            string nuevoPerfil = datosOperacion[2].Trim();
            string contraseña = datosOperacion[3].Trim();
            string fechaalta = datosOperacion[4].Trim();



            bool modificado = false;
            for (int i = 0; i < credenciales.Count; i++)
            {
                string[] partes = credenciales[i].Split(';');
                if (partes[0] == legajo)
                {
                    partes[1] = nuevoPerfil; // cambio de perfil
                    partes[2] = contraseña; // cambio de contraseña
                    partes[3] = fechaalta; // cambio de fecha alta




                    credenciales[i] = string.Join(";", partes);
                    modificado = true;
                    break;
                }
            }

            if (!modificado)
                throw new Exception("No se encontró legajo en credenciales."); // si no se encuentra el legajo en credenciales

            AdministradorPersistencia.EscribirArchivo("credenciales.csv", credenciales); // se escribe el archivo
            for (int i = 1; i < autorizaciones.Count; i++)
            {
                string[] partes = autorizaciones[i].Split(';');
                if (partes[0] == idAutorizacion)
                {
                    partes[2] = "Autorizado"; // estado
                    partes[5] = legajoAdministrador; // legajo del administrador
                    partes[6] = DateTime.Now.ToString(); // fecha de autorizacion
                    autorizaciones[i] = string.Join(";", partes);
                    break;
                }
            }

            AdministradorPersistencia.EscribirArchivo("autorizacion.csv", autorizaciones); // actualiza autorizaciones
        }
    }
}
