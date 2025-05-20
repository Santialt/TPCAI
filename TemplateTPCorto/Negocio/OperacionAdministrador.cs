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
        public DataTable ConvertirCSVaDataTable(List<string> lineas)
        {
            DataTable tabla = new DataTable();

            if (lineas == null || lineas.Count == 0)
                return tabla;

            // Crear columnas
            string[] columnas = lineas[0].Split(';');

            foreach (string columna in columnas)
            {
                tabla.Columns.Add(columna.Trim());
            }

            // Agregar filas
            for (int i = 1; i < lineas.Count; i++)
            {
                string[] valores = lineas[i].Split(';');
                tabla.Rows.Add(valores);
            }

            return tabla;
        }
        public DataView FiltrarPorEstado(DataTable tabla, string estado)
        {
            DataView vista = new DataView(tabla);

            if (!string.IsNullOrEmpty(estado) && estado.ToLower() != "todos")
            {
                vista.RowFilter = $"estado = '{estado}'";
            }

            return vista;
        }
        public void ProcesarCambio(string idAutorizacion)
        {
            var operaciones = AdministradorPersistencia.obtenerdatos("operacion_cambio_credencial.csv"); 
            var credenciales = AdministradorPersistencia.obtenerdatos("credenciales.csv");

            string operacion = operaciones.FirstOrDefault(largo => largo.StartsWith(idAutorizacion + ";")); // Busca la operación por ID
            if (operacion == null)
                throw new Exception("No se encontró operación para la autorización seleccionada.");

            string[] datosOperacion = operacion.Split(';');
            string legajo = datosOperacion[1].Trim();
            string nuevoPerfil = datosOperacion[2].Trim();

            bool modificado = false;
            for (int i = 0; i < credenciales.Count; i++)
            {
                string[] partes = credenciales[i].Split(';');
                if (partes[0] == legajo)
                {
                    partes[1] = nuevoPerfil; // Se asume que el perfil es el campo 3
                    credenciales[i] = string.Join(";", partes);
                    modificado = true;
                    break;
                }
            }

            if (!modificado)
                throw new Exception("No se encontró legajo en credenciales.");

            AdministradorPersistencia.EscribirArchivo("credenciales.csv", credenciales);
        }
    }
}
