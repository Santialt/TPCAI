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

namespace TemplateTPCorto
{
    public partial class Menu : Form
    {
        private Credencial credencial;

        public Menu(Credencial credencial)
        {
            InitializeComponent();
            this.credencial = credencial;
        }

        
        private void Menu_Load(object sender, EventArgs e)
        {
            // Por ejemplo, si tenés un label:
            // lblUsuario.Text = "Bienvenido, " + credencial.NombreUsuario;
        }
    }
}
