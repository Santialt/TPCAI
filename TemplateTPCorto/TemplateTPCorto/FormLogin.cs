using Datos;
using Datos.Login;
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
                // Login exitoso: redirigir o cargar siguiente pantalla
                // Por ejemplo:
                UsuarioPerfil usuarioPerfil = new UsuarioPerfil();
                List<string> strings = new List<string>();


                LoginPerfil loginPerfil = new LoginPerfil();
                usuarioPerfil.IdPerfil1 = loginPerfil.BuscarLegajo(credencial);
                usuarioPerfil.NombrePerfil1 = loginPerfil.ObtenerNombrePerfil(usuarioPerfil.IdPerfil1);
                List<string> idRoles = loginPerfil.ObtenerIdRoles(usuarioPerfil.IdPerfil1);
                foreach (string idRol in idRoles)
                {
                    string nombreRol = loginPerfil.ObtenerNombreRol(idRol);
                    if (nombreRol != null)
                        usuarioPerfil.Roles1.Add(nombreRol);
                }
                if (usuarioPerfil.IdPerfil1 == "1")
                {

                    MenuOperador menuoperador = new MenuOperador(credencial, usuarioPerfil);
                    this.Hide();
                    menuoperador.Show();



                }
                else if (usuarioPerfil.IdPerfil1 == "2")
                {
                    MenuSupervisor menuSupervisor = new MenuSupervisor(credencial,usuarioPerfil);   
                    this.Hide();
                    menuSupervisor.Show();
                  
                   
                }
                else if (usuarioPerfil.IdPerfil1 == "3")
                {
                   
                    MenuAdministrador menuadministrador = new MenuAdministrador(credencial,usuarioPerfil);
                    this.Hide();
                    menuadministrador.Show();
                    


                }
        
            }

        }

      
    }
}
