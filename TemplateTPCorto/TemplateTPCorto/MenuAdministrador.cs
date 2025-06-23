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
            //Preparación de la tabla

            var strings = usuarioPerfil.Roles1;
            string texto = string.Join(Environment.NewLine, strings);
            label2.Text = "Bienvenido/a " + credencial.NombreUsuario;
            label4.Text = usuarioPerfil.NombrePerfil1;
            dataGridView1.DataSource = operacionAdministrador.actualizarTabla();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            int anchoTotal = 0;

            foreach (DataGridViewColumn col in dataGridView1.Columns )   
            {
                anchoTotal += col.Width;
            }

            dataGridView1.Width = anchoTotal + dataGridView1.RowHeadersWidth + 2;
            lblDetalles.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //De acuerdo a la fila seleccionada, se aplica el cambio correspondiente segun el estado y tipo de operacion (cambio de Persona o Desbloquear Credencial
            //si el estado es Autorizada, no se puede volver a procesar
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

        private void button3_Click(object sender, EventArgs e)
        {
            //se muestra el detalle de la operación seleccionada por el usuario en caso de que quiera ver la fila seleccionada
            lblDetalles.Show();
            string estado = dataGridView1.CurrentRow.Cells["Estado"].Value.ToString();
            string tipoOperacion = dataGridView1.CurrentRow.Cells["tipoOperacion"].Value.ToString();
            if (tipoOperacion == "Cambio de Persona")
            {
                string idCambio = dataGridView1.CurrentRow.Cells["idOperacion"].Value.ToString();
                OperacionCambioPersona cambio = operacionAdministrador.BuscarOperacionPorId(idCambio);

                lblDetalles.Text = "Nombre: " + cambio.Nombre + Environment.NewLine +
                                   "Apellido: " + cambio.Apellido + Environment.NewLine +
                                   "DNI: " + cambio.Dni + Environment.NewLine +
                                   "Legajo: " + cambio.Legajo + Environment.NewLine +
                                   "Fecha de ingreso: " + cambio.FechaIngreso;
            }
            else if (tipoOperacion == "Desbloquear Credencial")
            {
                string idCambio = dataGridView1.CurrentRow.Cells["idOperacion"].Value.ToString();
                OperacionCambioCredencial cambio = operacionAdministrador.BuscarOperacionCredencialPorId(idCambio);
                lblDetalles.Text = "Legajo: " + cambio.Legajo + Environment.NewLine +
                                   "Nombre de usuario: " + cambio.NombreUsuario + Environment.NewLine +
                                   "Contraseña: " + cambio.Contraseña + Environment.NewLine +
                                   "Fecha de alta: " + cambio.FechaAlta + Environment.NewLine +
                                   "Fecha del último login: " + cambio.FechaUltimoLogin;
               
            }
        }
    }
}
