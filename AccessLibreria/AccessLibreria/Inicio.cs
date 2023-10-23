using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessLibreria
{
    public partial class Inicio : Form
    {
        Usuario usuario;

        Logging main;

        public Logging Main 
        { 
            set 
            { 
                main = value; 
            } 
        }

        public Usuario User 
        {
            set 
            { 
                usuario = value; 
            }
        }

        public Inicio()
        {
            InitializeComponent();
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            User = null;
            Close();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.FormParent = this;
            usuarios.Show();
            Hide();
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            Libros libros = new Libros();
            libros.FormParent = this;
            libros.User = usuario;
            libros.Show();
            Hide();
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            Prestamos prestamo = new Prestamos();
            prestamo.FormParent = this;
            prestamo.User = usuario;
            prestamo.Show();
            Hide();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = usuario.Dni;
            if (usuario.EsAdministrador)
            {
                btnUsuarios.Visible = true;
                btnPrestamos.Text = "Prestamos";
            }
            else 
            {
                btnUsuarios.Visible = false;
                btnPrestamos.Text = "Mis prestamos";
            }
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.Show();
        }
    }
}
