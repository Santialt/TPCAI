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
            this.Load += new System.EventHandler(this.FormCambioContraseña_Load);
            this.credencial = credencial;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nuevaPassword = txtPassword.Text;

         
            if (nuevaPassword.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres.");
                return;
            }

           
            if (nuevaPassword == credencial.Contrasena)
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a la anterior.");
                return;
            }

          
            CambioContrasena cambioNegocio = new CambioContrasena();
            bool fueExitosa;
            string mensaje = cambioNegocio.CambiarPassword(credencial.NombreUsuario, nuevaPassword, out fueExitosa);

            MessageBox.Show(mensaje);

            if (fueExitosa)
            {
                Menu menu = new Menu(credencial);
                menu.Show();
                this.Close();
            }
        }

        private void FormCambioContraseña_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Debe cambiar su contraseña por ser el primer ingreso o por haber pasado más de 30 días desde el último acceso.");
        }

    }
}
