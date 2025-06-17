using Datos;
using Datos.Login;
using Microsoft.Win32;
using Negocio;
using System;
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
    public partial class MenuOperador : Form
    {
        private Credencial credencial;
        private UsuarioPerfil usuarioPerfil;

        public MenuOperador(Credencial credencial , UsuarioPerfil usuarioPerfil)
        {
            InitializeComponent();
            this.credencial = credencial;
            this.usuarioPerfil = usuarioPerfil;
        }

        
    

        private void Menu_Load(object sender, EventArgs e)
        {
            var strings = usuarioPerfil.Roles1;
            string texto = string.Join(Environment.NewLine, strings);
            label3.Text = "Bienvenido/a " + credencial.NombreUsuario;
            label4.Text = usuarioPerfil.NombrePerfil1;


            // Por ejemplo, si tenés un label:
            // lblUsuario.Text = "Bienvenido, " + credencial.NombreUsuario;
        }

        private void cargarVenta_Click(object sender, EventArgs e)
        {
            FormVentas formVentas = new FormVentas();
            this.Hide();
            formVentas.Show();
        }
    }
}
