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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuSupervisor menuSupervisor = new MenuSupervisor(credencial, usuarioPerfil);
            this.Hide();
            menuSupervisor.Show();
        }
    }
}
