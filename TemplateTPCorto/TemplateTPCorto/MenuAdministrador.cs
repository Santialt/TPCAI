using Datos;
using Datos.Login;
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
    public partial class MenuAdministrador : Form
    {
        private Credencial credencial;
        private UsuarioPerfil usuarioPerfil;
        public MenuAdministrador(Credencial credencial, UsuarioPerfil usuarioPerfil )
        {
            InitializeComponent();
            this.credencial = credencial;
            this.usuarioPerfil = usuarioPerfil; 

        }

        private void MenuAdministrador_Load(object sender, EventArgs e)
        {
            var strings = usuarioPerfil.Roles1;
            string texto = string.Join(Environment.NewLine, strings);
            label2.Text = "Bienvenido/a " + credencial.NombreUsuario;
            label4.Text = usuarioPerfil.NombrePerfil1;
        }
    }
}
