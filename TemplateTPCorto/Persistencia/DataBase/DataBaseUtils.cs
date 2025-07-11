﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Persistencia.DataBase
{
    public class DataBaseUtils
    {
        string archivoCsv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas\");


        public List<String> BuscarRegistro(String nombreArchivo)
        {
            String rutaArchivo = Path.GetFullPath(archivoCsv + nombreArchivo); // Normaliza la ruta

            List<String> listado = new List<String>();

            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        listado.Add(linea);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo leer el archivo:");
                Console.WriteLine(e.Message);
            }
            return listado;
        }

        // Método para borrar un registro
        public void BorrarRegistro(string id, String nombreArchivo)
        {
            String archivoParaBorrar = archivoCsv + nombreArchivo; // Cambia esta ruta al archivo CSV que deseas leer

            String rutaArchivo = Path.GetFullPath(archivoParaBorrar); // Normaliza la ruta

            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine("El archivo no existe: " + archivoParaBorrar);
                    return;
                }

                // Leer el archivo y obtener las líneas
                List<string> listado = BuscarRegistro(nombreArchivo);

                // Filtrar las líneas que no coinciden con el ID a borrar (comparar solo la primera columna)
                var registrosRestantes = listado.Where(linea =>
                {
                    var campos = linea.Split(';');
                    return campos[0] != id; // Verifica solo el ID (primera columna)
                }).ToList();

                // Sobrescribir el archivo con las líneas restantes
                File.WriteAllLines(archivoParaBorrar, registrosRestantes);

                Console.WriteLine($"Registro con ID {id} borrado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al intentar borrar el registro:");
                Console.WriteLine($"Mensaje: {e.Message}");
                Console.WriteLine($"Pila de errores: {e.StackTrace}");
            }
        }

        // Método para agregar un registro
        public void AgregarRegistro(string nombreArchivo, string nuevoRegistro)
        {
            String rutaArchivo = Path.GetFullPath(archivoCsv + nombreArchivo);
            //string archivoCsv = Path.Combine(Directory.GetCurrentDirectory(), "Persistencia", "Datos", nombreArchivo);

            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine("El archivo no existe: " + rutaArchivo);
                    return;
                }

                // Abrir el archivo y agregar el nuevo registro
                using (StreamWriter sw = new StreamWriter(rutaArchivo, append: true))
                {
                    sw.WriteLine(nuevoRegistro); // Agregar la nueva línea
                }

                Console.WriteLine("Registro agregado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al intentar agregar el registro:");
                Console.WriteLine($"Mensaje: {e.Message}");
                Console.WriteLine($"Pila de errores: {e.StackTrace}");
            }
        }
        public string BuscarUsuario(string nombreArchivo, string nombreUsuario)
        {
            string rutaArchivo = Path.Combine(archivoCsv, nombreArchivo);

            if (!File.Exists(rutaArchivo))
                return null;

            try
            {
                using (var fs = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] campos = linea.Split(';');

                        if (campos.Length >= 2)
                        {
                            string usuarioLeido = campos[1].Trim();
                            string usuarioIngresado = nombreUsuario.Trim();

                            if (string.Equals(usuarioLeido, usuarioIngresado, StringComparison.OrdinalIgnoreCase))
                            {
                                return linea;
                            }
                        }
                    }
                }
            }
            catch
            {
                // Log opcional o manejo de error
            }

            return null;
        }

        public void RegistrarLineaEnArchivo(string nombreArchivo, string linea) // se creo un nuevo método para registrar una línea en el archivo 
        {
            string rutaRelativa = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Persistencia\DataBase\Tablas", nombreArchivo);
            string rutaCompleta = Path.GetFullPath(rutaRelativa);

            try
            {
                if (string.IsNullOrWhiteSpace(linea))
                {
                    Console.WriteLine("La línea está vacía. No se escribe.");
                    return;
                }

                
                linea = linea.Trim();

               
                if (File.Exists(rutaCompleta))
                {
                    var lineas = File.ReadAllLines(rutaCompleta).ToList();

                    // Eliminar líneas vacías 
                    lineas = lineas.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

                    
                    lineas.Add(linea);
                    File.WriteAllLines(rutaCompleta, lineas);
                }
                else
                {
                    // Si no existe, lo crea con la línea limpia
                    using (StreamWriter sw = new StreamWriter(rutaCompleta, append: false))
                    {
                        sw.WriteLine(linea);
                    }
                }

                Console.WriteLine("Línea registrada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar la línea:");
                Console.WriteLine($"Mensaje: {ex.Message}");
            }
        }

    }
}
