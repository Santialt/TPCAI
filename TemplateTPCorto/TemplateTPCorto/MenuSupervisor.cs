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
            label8.Text = credencial.NombreUsuario;
            label10.Text = usuarioPerfil.NombrePerfil1;
            txtLegajo.Enabled = false;  
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            // Busca la persona por legajo y completa los textbox con los datos de la persona 
            LoginPerfil loginPerfil = new LoginPerfil();
            string legajoabuscar = textBox1.Text;
            string[] datos = loginPerfil.BuscarPersonaPorLegajo(legajoabuscar , "persona.csv");
            if (datos != null)
            {
                txtLegajo.Text = datos[0];
                txtNombre.Text = datos[1];
                txtApellido.Text = datos[2];
                txtDni.Text = datos[3];
                txtFechaingreso.Text = datos[4];
            }
            else
            {
                MessageBox.Show("No se encontró ninguna persona con ese legajo.");
            }


        }



        private void button2_Click(object sender, EventArgs e)
        {
            // toma los datos en los textbox y crea una solicitud de cambio de credencial y la guarda en el archivo operacion_cambio_persona.csv
            // tambien crea una solicitud de autorizacion y guarda en el archivo autorizacion.csv
            OperacionSupervisor supervisor = new OperacionSupervisor();
            string Legajo = txtLegajo.Text;
            string Nombre = txtNombre.Text;
            string Apellido = txtApellido.Text;
            string Dni = txtDni.Text;
            string fecha_ingreso = txtFechaingreso.Text;
            string idcambio = supervisor.ObtenerProximoIdCambio("operacion_cambio_persona.csv");
            string tipo_operacion = "Cambio de Persona";
            string estado = "Pendiente";    
            string legajo_solicitante = credencial.Legajo;
            string autorizador = "";
            string fecha_autorizacion = "";

            string solicitud_autorizacion = idcambio + ";" + tipo_operacion + ";" + estado + ";" + legajo_solicitante + ";" + DateTime.Now + ";" + autorizador + ";" + fecha_autorizacion ;
            supervisor.AgregarSolicitud(solicitud_autorizacion, "autorizacion.csv");
            string credencialcambio= idcambio + ";" + Legajo + ";" + Nombre + ";" + Apellido + ";" + Dni + ";" + fecha_ingreso;
            supervisor.AgregarSolicitud(credencialcambio, "operacion_cambio_persona.csv");
            MessageBox.Show("Solicitud de cambio de credencial enviada correctamente.");

            txtApellido.Clear();
            txtDni.Clear();
            txtFechaingreso.Clear();
            txtLegajo.Clear();
            txtNombre.Clear();
            textBox1.Clear();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Cierra el formulario actual y vuelve a la pantalla de login
            OpcionSupervisor opcionSupervisor = new OpcionSupervisor(credencial, usuarioPerfil);
            this.Hide();
            opcionSupervisor.Show();
        }

    }
    
}
