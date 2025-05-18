using Datos;
using Negocio;
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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            String usuario = txtUsuario.Text.Trim();
            String password = txtPassword.Text;
            
            LoginNegocio loginNegocio = new LoginNegocio();
            string mensaje;

            Credencial credencial = loginNegocio.login(usuario, password, out mensaje);

            MessageBox.Show(mensaje);

            if (credencial != null)
            {
                // login exitoso sin fecha ultimo ingreso >> form cambiocontraseña
                if (credencial.FechaUltimoLogin == DateTime.MinValue)
                {
                    this.Hide();
                    FormCambioContraseña cambiocontraseña = new FormCambioContraseña(credencial);
                    cambiocontraseña.ShowDialog(); // Mostrás como form modal
                    this.Show(); 
                    return;
                }

                // Login exitoso: redirigir o cargar siguiente pantalla
                Menu menu = new Menu(credencial);
                 menu.Show();
                 this.Hide();
            }

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
