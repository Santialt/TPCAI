using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Ventas
{
    public class Venta
    {
        Guid _idUsuario;
        Guid _idCliente;
        List<VentaDetalle> _detalle = new List<VentaDetalle>();

        public Guid IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public Guid IdCliente { get => _idCliente; set => _idCliente = value; }
        public List<VentaDetalle> Detalle { get => _detalle; set => _detalle = value; }
    }
}