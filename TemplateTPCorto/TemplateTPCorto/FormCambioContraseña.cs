using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Negocio;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TemplateTPCorto
{
    public partial class FormCambioContraseña : Form
    {
        private Credencial credencial;

        public FormCambioContraseña(Credencial credencial)
        {
            InitializeComponent();
            this.credencial = credencial;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string mensaje;
            String password = txtPassword.Text;
            this.credencial.Contrasena = password;
            this.credencial.FechaUltimoLogin = DateTime.Now;

            PrimerLogin primerLogin = new PrimerLogin();
            primerLogin.actualizarPrimerLogin(credencial, out mensaje);
            MessageBox.Show(mensaje);

            Menu menu = new Menu(credencial);
            menu.Show();
            this.Close();
            return;
        }

        private void FormCambioContraseña_Load(object sender, EventArgs e)
        {

        }
    }
}
