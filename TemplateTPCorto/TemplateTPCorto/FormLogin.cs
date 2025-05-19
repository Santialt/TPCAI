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
                // 👇 Validar si es primer login o pasaron más de 30 días
                bool esPrimerLogin = credencial.FechaUltimoLogin == DateTime.MinValue;
                bool pasaron30Dias = (DateTime.Now - credencial.FechaUltimoLogin).TotalDays > 30;

                if (esPrimerLogin || pasaron30Dias)
                {
                    this.Hide();
                    FormCambioContraseña cambiocontraseña = new FormCambioContraseña(credencial);
                    cambiocontraseña.ShowDialog(); // modal
                    this.Show(); // volver si se cierra
                    return;
                }

                // ✅ Login exitoso normal
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
