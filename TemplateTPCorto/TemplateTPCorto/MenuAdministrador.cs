using Datos;
using Datos.Login;
using System;
using Negocio;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TemplateTPCorto
{
    public partial class MenuAdministrador : Form
    {
        private Credencial credencial;
        private UsuarioPerfil usuarioPerfil;
        private DataTable tablaoriginal;
        OperacionAdministrador operacionAdministrador = new OperacionAdministrador();
        public MenuAdministrador(Credencial credencial, UsuarioPerfil usuarioPerfil )
        {
            InitializeComponent();
            this.credencial = credencial;
            this.usuarioPerfil = usuarioPerfil; 

        }

        private void MenuAdministrador_Load(object sender, EventArgs e)
        {
          
            comboBox1.SelectedIndex = 0;
            var strings = usuarioPerfil.Roles1;
            string texto = string.Join(Environment.NewLine, strings);
            label2.Text = "Bienvenido/a " + credencial.NombreUsuario;
            label4.Text = usuarioPerfil.NombrePerfil1;
            
            List<string> lineas = operacionAdministrador.Leerarchivo("autorizacion.csv");
            DataTable tablaoriginal = operacionAdministrador.ConvertirCSVaDataTable(lineas);

            dataGridView1.DataSource = tablaoriginal;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                string idCambio = dataGridView1.CurrentRow.Cells["idOperacion"].Value.ToString();

                // Buscar el cambio en el archivo
                OperacionCambioCredencial cambio = operacionAdministrador.BuscarOperacionPorId(idCambio);

                if (cambio != null)
                {
                    // Aplicar cambio en credenciales.csv
                    operacionAdministrador.AplicarCambioEnCredencial(cambio);

                    MessageBox.Show("Cambio aplicado correctamente.");
                    dataGridView1.CurrentRow.Cells["Estado"].Value = "Autorizada";
                    string idCambio2 = dataGridView1.CurrentRow.Cells["idOperacion"].Value.ToString();

                    operacionAdministrador.ActualizarEstadoAutorizacion(idCambio2, "Autorizada");
                }
                else
                {
                    MessageBox.Show("No se encontró la operación.");
                }
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperacionAdministrador operacionAdministrador = new OperacionAdministrador();
            string estadoSeleccionado = comboBox1.SelectedItem.ToString();
            DataView vistaFiltrada = operacionAdministrador.FiltrarPorEstado(tablaoriginal, estadoSeleccionado);

            dataGridView1.DataSource = vistaFiltrada;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
