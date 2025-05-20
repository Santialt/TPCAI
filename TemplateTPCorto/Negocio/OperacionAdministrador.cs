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
    }
}
