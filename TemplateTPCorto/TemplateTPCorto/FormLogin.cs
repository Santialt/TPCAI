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

        // 🔓 Obtener perfil y roles del usuario
        UsuarioPerfil usuarioPerfil = new UsuarioPerfil();
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

        // 👥 Redirigir al menú correspondiente según perfil
        if (usuarioPerfil.IdPerfil1 == "1")
        {
            MenuOperador menuOperador = new MenuOperador(credencial, usuarioPerfil);
            this.Hide();
            menuOperador.Show();
        }
        else if (usuarioPerfil.IdPerfil1 == "2")
        {
            MenuSupervisor menuSupervisor = new MenuSupervisor(credencial, usuarioPerfil);
            this.Hide();
            menuSupervisor.Show();
        }
        else if (usuarioPerfil.IdPerfil1 == "3")
        {
            MenuAdministrador menuAdministrador = new MenuAdministrador(credencial, usuarioPerfil);
            this.Hide();
            menuAdministrador.Show();
        }
    }
}


        

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

      
    }
}
