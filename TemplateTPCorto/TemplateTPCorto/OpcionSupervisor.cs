using Datos.Login;
using Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Emit;

namespace TemplateTPCorto
{
    public partial class OpcionSupervisor : Form
    {
        private Credencial credencial;
        private UsuarioPerfil usuarioPerfil;

        public OpcionSupervisor(Credencial credencial, UsuarioPerfil usuarioPerfil)
        {
            InitializeComponent();
            this.credencial = credencial;
            this.usuarioPerfil = usuarioPerfil;
        }
        public OpcionSupervisor()
        {
            InitializeComponent();
        }

        private void OpcionSupervisor_Load(object sender, EventArgs e)
        {
            var strings = usuarioPerfil.Roles1;
            string texto = string.Join(Environment.NewLine, strings);
            label1.Text = "Bienvenido/a  " + credencial.NombreUsuario;
            label2.Text = "Perfil:  " + usuarioPerfil.NombrePerfil1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuSupervisor menuSupervisor = new MenuSupervisor(credencial, usuarioPerfil);
            this.Hide();
            menuSupervisor.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Hide();
            formLogin.Show();   
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormDesbloquearCredenciales formDesbloquearCredenciales = new FormDesbloquearCredenciales(credencial, usuarioPerfil);
            this.Hide();
            formDesbloquearCredenciales.Show();
        }
    }
}
