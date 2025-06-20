using Newtonsoft.Json;
using Persistencia.WebService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class VentaPersistencia
    {
        public bool agregarVenta(Datos.Ventas.Venta venta)
        {
            var jsonRequest = JsonConvert.SerializeObject(venta);
            HttpResponseMessage response = WebHelper.Post("/Api/Venta/AgregarVenta", jsonRequest);
            foreach (var item in venta.Detalle)
            {
                System.Diagnostics.Debug.WriteLine($"Producto en venta - ID: {item.IdProducto}, Cantidad: {item.Cantidad}");
            }

            string responseText = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = response.Content.ReadAsStringAsync().Result;
                System.Windows.Forms.MessageBox.Show("Error al registrar venta:\n" + errorContent, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            System.Diagnostics.Debug.WriteLine("Respuesta API: " + responseText);
            return response.IsSuccessStatusCode;
        }

    }
}


