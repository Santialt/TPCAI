using Datos;
using Datos.Login;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateTPCorto
{
    public partial class MenuSupervisor : Form
    {
        private Credencial credencial;
        private UsuarioPerfil usuarioPerfil;
        public MenuSupervisor(Credencial credencial,UsuarioPerfil usuarioPerfil)
        {
            InitializeComponent();
            this.credencial = credencial;
            this.usuarioPerfil = usuarioPerfil;
        }

        private void MenuSupervisor_Load(object sender, EventArgs e)
        {
            var strings = usuarioPerfil.Roles1;
            string texto = string.Join(Environment.NewLine, strings);
            label8.Text = "Bienvenido/a " + credencial.NombreUsuario;
            label10.Text = usuarioPerfil.NombrePerfil1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPerfil loginPerfil = new LoginPerfil();
            string legajoabuscar = textBox1.Text;
            Credencial cred = loginPerfil.BuscarCredencialPorLegajo("credenciales.csv", legajoabuscar);
            if (cred != null)
            {
                textBox6.ReadOnly = true;
                textBox2.Text = cred.NombreUsuario;
                textBox3.Text = cred.Contrasena;
                textBox4.Text = cred.FechaAlta.ToString("d/M/yyyy");
                textBox5.Text = cred.FechaUltimoLogin.ToString("d/M/yyyy");
                textBox6.Text = cred.Legajo;

            }
            else
            {
                MessageBox.Show("Legajo no encontrado.");
            }
           

        }

        

        private void button2_Click(object sender, EventArgs e)
        { 
            string nombreusuario = textBox2.Text;
            string contrasena = textBox3.Text;
            string fechaalta = textBox4.Text;
            string fechaultimologin = textBox5.Text;
            string legajo = textBox6.Text;
            string idcambio = "1";
            string credencialcambio= idcambio + ";" + legajo + ";" + nombreusuario + ";" + contrasena + ";" + fechaalta + ";" + fechaultimologin;
            Supervisor supervisor = new Supervisor();
            supervisor.AgregarSolicitud(credencialcambio);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
    
}
