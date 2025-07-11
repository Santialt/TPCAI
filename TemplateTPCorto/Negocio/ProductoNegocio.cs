﻿using Datos.Ventas;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> obtenerProductosPorCategoria(String categoria)
        {
            // Aplico la logica de negocio

            // 1- Mostrar solo productos que tienen stock positivo
            ProductoPersistencia productoPersistencia = new ProductoPersistencia();
            List<Producto> todosProductos = productoPersistencia.obtenerProductosPorCategoria(categoria);
            List<Producto> productosConStock = new List<Producto>();
            foreach (Producto producto in todosProductos)
            {
                if (producto.Stock > 0)
                {
                    productosConStock.Add(producto);
                }
            }

            return productosConStock;
        }
    }
}
