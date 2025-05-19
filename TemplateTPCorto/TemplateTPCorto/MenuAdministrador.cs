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
            OperacionAdministrador operacionAdministrador = new OperacionAdministrador();
            List<string> lineas = operacionAdministrador.Leerarchivo("autorizacion.csv");
            DataTable tablaoriginal = operacionAdministrador.ConvertirCSVaDataTable(lineas);

            dataGridView1.DataSource = tablaoriginal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperacionAdministrador operacionAdministrador = new OperacionAdministrador();
            string estadoSeleccionado = comboBox1.SelectedItem.ToString();
            DataView vistaFiltrada = operacionAdministrador.FiltrarPorEstado(tablaoriginal, estadoSeleccionado);

            dataGridView1.DataSource = vistaFiltrada;
        }
    }
}
