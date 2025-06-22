using Datos;
using Datos.Login;
using System;
using Negocio;
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
    public partial class MenuAdministrador : Form
    {
        private Credencial credencial;
        private UsuarioPerfil usuarioPerfil;
        private DataTable tablaoriginal;
        OperacionAdministrador operacionAdministrador = new OperacionAdministrador();
        public MenuAdministrador(Credencial credencial, UsuarioPerfil usuarioPerfil  )
        {
            InitializeComponent();
            this.credencial = credencial;
            this.usuarioPerfil = usuarioPerfil; 
         

        }

        private void MenuAdministrador_Load(object sender, EventArgs e)
        {
           
           
            var strings = usuarioPerfil.Roles1;
            string texto = string.Join(Environment.NewLine, strings);
            label2.Text = "Bienvenido/a " + credencial.NombreUsuario;
            label4.Text = usuarioPerfil.NombrePerfil1;
            dataGridView1.DataSource = operacionAdministrador.actualizarTabla();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                string estado = dataGridView1.CurrentRow.Cells["Estado"].Value.ToString();
                string tipoOperacion = dataGridView1.CurrentRow.Cells["tipoOperacion"].Value.ToString();

                if (estado == "Autorizada")
                {
                    MessageBox.Show("Esta operación ya fue autorizada y no puede volver a procesarse.");
                    return;
                }
                if (estado == "Pendiente" && tipoOperacion == "Cambio de Persona" )
                {
                    string idCambio = dataGridView1.CurrentRow.Cells["idOperacion"].Value.ToString();
                    OperacionCambioPersona cambio = operacionAdministrador.BuscarOperacionPorId(idCambio);

                    if (cambio != null)
                    {
                        operacionAdministrador.AplicarCambioEnPersona(cambio);
                        MessageBox.Show("Cambio aplicado correctamente.");
                        dataGridView1.CurrentRow.Cells["Estado"].Value = "Autorizada";
                        operacionAdministrador.ActualizarEstadoAutorizacion(idCambio, "Autorizada", credencial.Legajo,tipoOperacion);
                        dataGridView1.DataSource = operacionAdministrador.actualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la operación.");
                    }
                }
                if (estado == "Pendiente" && tipoOperacion == "Desbloquear Credencial")
                {
                    string idCambio = dataGridView1.CurrentRow.Cells["idOperacion"].Value.ToString();
                    OperacionCambioCredencial cambio = operacionAdministrador.BuscarOperacionCredencialPorId(idCambio);
                    if (cambio != null)
                    {
                        operacionAdministrador.AplicarCambioenCrendencial(cambio);
                        MessageBox.Show("Cambio aplicado correctamente.");
                        dataGridView1.CurrentRow.Cells["Estado"].Value = "Autorizada";
                        operacionAdministrador.ActualizarEstadoAutorizacion(idCambio, "Autorizada", credencial.Legajo,tipoOperacion);
                        dataGridView1.DataSource = operacionAdministrador.actualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la operación.");
                    }
                }

            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            this.Hide();
            formLogin.Show();
        }
    }
}
