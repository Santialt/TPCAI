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
using Negocio;

namespace TemplateTPCorto
{
    public partial class FormDesbloquearCredenciales : Form
    {
        private Credencial credencial;
        private UsuarioPerfil usuarioPerfil;    
        public FormDesbloquearCredenciales(Credencial credencial, UsuarioPerfil usuarioPerfil)
        {
            InitializeComponent();
            this.credencial = credencial;
            this.usuarioPerfil = usuarioPerfil;


        }
        public FormDesbloquearCredenciales()
        {
            InitializeComponent();
        }

        private void FormDesbloquearCredenciales_Load(object sender, EventArgs e)
        {
            txtLegajo.Enabled = false;
            txtUsuario.Enabled = false;
            txtContraseña.Enabled = false;
            txtFechaAlta.Enabled = false;
            txtUltimoLogin.Enabled = false;
            txtPerfil.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPerfil loginPerfil = new LoginPerfil();
            string legajoabuscar = textBox1.Text;
            string[] datosCredenciales = loginPerfil.BuscarPersonaPorLegajo(legajoabuscar, "credenciales.csv");
            string datosPerfil = loginPerfil.BuscarPerfilPorLegajo(legajoabuscar);
            string nombrePerfil = loginPerfil.ObtenerNombrePerfil(datosPerfil);
            if (datosCredenciales != null)
            {
                txtLegajo.Text = datosCredenciales[0];
                txtUsuario.Text = datosCredenciales[1];
                txtContraseña.Text = datosCredenciales[2];
                txtFechaAlta.Text = datosCredenciales[3];
                txtUltimoLogin.Text = datosCredenciales[4];
                txtPerfil.Text = nombrePerfil;
            }
            else
            {
                MessageBox.Show("No se encontraron credenciales para el legajo ingresado.");
                textBox1.Clear();
            }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpcionSupervisor opcionSupervisor = new OpcionSupervisor(credencial, usuarioPerfil);
            this.Hide();
            opcionSupervisor.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OperacionSupervisor supervisor = new OperacionSupervisor();
            string Legajo = txtLegajo.Text;
            string NombreUsuario = txtUsuario.Text;
            string Contraseña = txtContraseña.Text;
            string FechaAlta = txtFechaAlta.Text;
            string fechaUltimoLogin = "";
            string idcambio = supervisor.ObtenerProximoIdCambio("operacion_cambio_credencial.csv");
            string tipo_operacion = "Desbloquear Credencial";
            string estado = "Pendiente";
            string legajo_solicitante = credencial.Legajo;
            string autorizador = "";
            string fecha_autorizacion = "";

            string solicitud_autorizacion = idcambio + ";" + tipo_operacion + ";" + estado + ";" + legajo_solicitante + ";" + DateTime.Now + ";" + autorizador + ";" + fecha_autorizacion;
            supervisor.AgregarSolicitud(solicitud_autorizacion, "autorizacion.csv");
            string credencialcambio = idcambio + ";" + Legajo + ";" + NombreUsuario + ";" + Contraseña + ";" + FechaAlta + ";" + fechaUltimoLogin;
            supervisor.AgregarSolicitud(credencialcambio, "operacion_cambio_credencial.csv");
            MessageBox.Show("Solicitud de cambio de credencial enviada correctamente.");

            textBox1.Clear();
            txtLegajo.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();
            txtFechaAlta.Clear();
            txtUltimoLogin.Clear();
            txtPerfil.Clear();

        }
    }
}
