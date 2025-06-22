using Persistencia.DataBase;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.IO;

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
        public OperacionCambioCredencial BuscarOperacionPorId(string id)
        {
            var ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\", "operacion_cambio_credencial.csv");
            var lineas = File.ReadAllLines(ruta);

            foreach (var linea in lineas)
            {
                var campos = linea.Split(';');
                if (campos.Length >= 5 && campos[0].Trim() == id)
                {
                    return new OperacionCambioCredencial
                    {
                        IdCambio = campos[0],
                        Legajo = campos[1],
                        Usuario = campos[2],
                        NuevaClave = campos[3],
                        FechaSolicitud = campos[4]
                    };
                }
            }

            return null;
        }
        public void AplicarCambioEnCredencial(OperacionCambioCredencial cambio)
        {
            string rutaCredenciales = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\", "credenciales.csv");
            var lineas = File.ReadAllLines(rutaCredenciales).ToList();

            for (int i = 0; i < lineas.Count; i++)
            {
                var campos = lineas[i].Split(';');
                if (campos.Length >= 4 && campos[0] == cambio.Legajo)
                {
                    // Reemplaza la línea con la nueva clave y último login vacío
                    string nuevaLinea = $"{campos[0]};{cambio.Usuario};{cambio.NuevaClave};";
                    lineas[i] = nuevaLinea;
                    break;
                }
            }

            File.WriteAllLines(rutaCredenciales, lineas);
        }
        public void ActualizarEstadoAutorizacion(string idCambio, string nuevoEstado)
        {
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\", "autorizacion.csv");

            if (!File.Exists(ruta)) return;

            var lineas = File.ReadAllLines(ruta).ToList();

            for (int i = 0; i < lineas.Count; i++)
            {
                string[] campos = lineas[i].Split(';');

                if (campos.Length >= 4 && campos[0].Trim() == idCambio)
                {
                    campos[2] = nuevoEstado; 
                    lineas[i] = string.Join(";", campos);
                    break;
                }
            }

            File.WriteAllLines(ruta, lineas);
        }

    }
}
