using Datos;
using Datos.Ventas;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class FormVentas : Form
    {
        List<Producto> productos = new List<Producto>();
        List<Producto> productosEnCarrito = new List<Producto>();
        double subTotal = 0.0;
        double total = 0.0;

        public FormVentas()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.FormVentas_Load);
        }

        private void FormVentas_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarCategoriasProductos();
            IniciarTotales();
        }

        private void IniciarTotales()
        {
            lablSubTotal.Text = "0.00";
            lblTotal.Text = "0.00";
        }

        private void CargarCategoriasProductos()
        {

            VentasNegocio ventasNegocio = new VentasNegocio();

            List<CategoriaProductos> categoriaProductos = ventasNegocio.obtenerCategoriaProductos();

            foreach (CategoriaProductos categoriaProducto in categoriaProductos)
            {
                cboCategoriaProductos.Items.Add(categoriaProducto);
            }
        }

        private void CargarClientes()
        {
            VentasNegocio ventasNegocio = new VentasNegocio();

            List<Cliente> listadoClientes = ventasNegocio.obtenerClientes();

            foreach (Cliente cliente in listadoClientes)
            {
                cmbClientes.Items.Add(cliente);
            }
        }

        private void btnListarProductos_Click(object sender, EventArgs e)
        {
            VentasNegocio ventasNegocio = new VentasNegocio();


        }

        private void cboCategoriaProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // borrar esto
        }

        private void btnListarProductos_Click_1(object sender, EventArgs e)
        {
            if (cboCategoriaProductos.Text.Equals(""))
            {
                MessageBox.Show("Debes seleccionar una categoria antes de listar los productos.");
                return;
            }
            ProductoNegocio productoNegocio = new ProductoNegocio();
            productos = productoNegocio.obtenerProductosPorCategoria(cboCategoriaProductos.Text);
            lstProducto.Items.Clear();
            foreach (Producto producto in productos)
            {
                lstProducto.Items.Add(producto);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (lstProducto.Items.Count <= 0)
            {
                MessageBox.Show("Debes seleccionar una categoria y listar los productos.");
                return;
            }
            int.TryParse(txtCantidad.Text, out int cantidadAComprar);
            if (cantidadAComprar <= 0)
            {
                MessageBox.Show("La cantidad seleccionada debe ser mayor a 0.");
                return;
            }
            String productoSeleccionadoString = lstProducto.GetItemText(lstProducto.SelectedItem);
            if (productoSeleccionadoString.Equals(""))
            {
                MessageBox.Show("Debes seleccionar un producto para agregarlo al carrito.");
                return;
            }
            Producto productoSeleccionado = getProductoPorNombre(productoSeleccionadoString);
            if (productosEnCarrito.Exists(p => p.Nombre.Equals(productoSeleccionado.Nombre)))
            {
                MessageBox.Show("Ya has agregado este producto al carrito, si quieres cambiar la cantidad primero quitalo del carrito.");
                return;
            }
            if (cantidadAComprar > productoSeleccionado.Stock)
            {
                MessageBox.Show("Cantidad seleccionada excede el stock disponible, por favor seleccione una cantidad valida.");
                return;
            }
            productoSeleccionado.Stock = cantidadAComprar;
            productosEnCarrito.Add(productoSeleccionado);
            listBox1.Items.Add(productoSeleccionado);
            subTotal += productoSeleccionado.Precio * cantidadAComprar;
            total += productoSeleccionado.Precio * cantidadAComprar;
            actualizarTotales();
        }

        private Producto getProductoPorNombre(String productoString)
        {
            String[] splitProducto = productoString.Split('|');
            String nombre = splitProducto[0].Trim();
            foreach (Producto producto in productos)
            {
                if (producto.Nombre.Equals(nombre))
                {
                    return producto;
                }
            }
            return null;
        }

        private void actualizarTotales()
        {
            lablSubTotal.Text = subTotal.ToString();
            lblTotal.Text = total.ToString();
        }

        private void FormVentas_Load_1(object sender, EventArgs e)
        {

        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto del carrito para quitar.");
                return;
            }

            Producto productoSeleccionado = (Producto)listBox1.SelectedItem;
            listBox1.Items.Remove(productoSeleccionado);
            productosEnCarrito.RemoveAll(p => p.Id.Equals(productoSeleccionado.Id));
            subTotal -= productoSeleccionado.Precio * productoSeleccionado.Stock;
            total -= productoSeleccionado.Precio * productoSeleccionado.Stock;
            actualizarTotales();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un cliente para cargar la venta.");
                return;
            }

            if (productosEnCarrito.Count == 0)
            {
                MessageBox.Show("Debe agregar productos al carrito.");
                return;
            }

            Cliente cliente = (Cliente)cmbClientes.SelectedItem;

            Venta venta = new Venta();
            venta.IdCliente = cliente.Id;
            venta.IdUsuario = new Guid("784c07f2-2b26-4973-9235-4064e94832b5");

            foreach (Producto p in productosEnCarrito)
            {
                VentaDetalle detalle = new VentaDetalle();
                detalle.IdProducto = p.Id;
                detalle.Cantidad = p.Stock;
                venta.Detalle.Add(detalle);
            }

            VentasNegocio ventasNegocio = new VentasNegocio();
            bool ok = ventasNegocio.agregarVenta(venta);

            if (ok)
            {
                MessageBox.Show("Venta cargada con éxito");
                productosEnCarrito.Clear();
                listBox1.Items.Clear();
                subTotal = 0.0;
                total = 0.0;
                actualizarTotales();
            }
            else
            {
                MessageBox.Show("No se pudo registrar la venta");
            }
        }
    }
}
