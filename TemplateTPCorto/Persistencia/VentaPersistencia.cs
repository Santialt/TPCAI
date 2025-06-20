using Newtonsoft.Json;
using Persistencia.WebService.Utils;
using Datos.Ventas;
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
        public bool agregarVenta(Venta venta)
        {
            bool ok = true;
            foreach (var detalle in venta.Detalle)
            {
                var requestBody = new
                {
                    idCliente = venta.IdCliente,
                    idUsuario = venta.IdUsuario,
                    idProducto = detalle.IdProducto,
                    cantidad = detalle.Cantidad
                };

                var jsonRequest = JsonConvert.SerializeObject(requestBody);

                HttpResponseMessage response = WebHelper.Post("/api/Venta/AgregarVenta", jsonRequest);

                if (!response.IsSuccessStatusCode)
                {
                    ok = false;
                }
            }
            return ok;
        }

    }
}


