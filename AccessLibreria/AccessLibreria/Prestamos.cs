using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace AccessLibreria
{
    public partial class Prestamos : Form
    {
        Inicio main;
        Usuario usuario;
        OleDbConnection connection;

        public Prestamos()
        {
            InitializeComponent();
        }

        public Inicio FormParent
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

        private void Prestamos_Load(object sender, EventArgs e)
        {
            if (!usuario.EsAdministrador)
            {
                lblDni.Visible = false;
                txtDni.Visible = false;
                btnDevolver.Visible = true;
            }
            else 
            {
                lblDni.Visible = true;
                txtDni.Visible = true;
                btnDevolver.Visible = false;
            }
            dgvPrestamos.AllowUserToAddRows = false;
            dgvPrestamos.AllowUserToDeleteRows = false;
            dgvPrestamos.ReadOnly = true;
            dgvPrestamos.AutoResizeColumns();
            dgvPrestamos.AutoResizeRows();
            dgvPrestamos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvPrestamos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvPrestamos.DefaultCellStyle.SelectionBackColor = dgvPrestamos.DefaultCellStyle.BackColor;
            dgvPrestamos.DefaultCellStyle.SelectionForeColor = dgvPrestamos.DefaultCellStyle.ForeColor;
            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string projectPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(exePath, @"..\..\.."));
            string dbPath = System.IO.Path.Combine(projectPath, "Access_DbLibreria.accdb");
            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath);
            RellenarCuadricula();
        }

        void ResetDatos()
        {
            txtISBN.Text = string.Empty;
            txtDni.Text = string.Empty;
            txtISBN.Focus();
        }

        void RellenarCuadricula()
        {
            string cadenaSQL = "";
            if (usuario.EsAdministrador)
            {
                cadenaSQL = $@"SELECT prestamos.ISBN AS ISBN, libros.Titulo AS Titulo, libros.Editorial AS Editorial, prestamos.DNI AS [DNI Usuario]
                                FROM Access_TaPrestamos prestamos, Access_TaLibros libros
                                WHERE prestamos.ISBN = libros.ISBN";
            }
            else 
            {
                cadenaSQL = $@"SELECT prestamos.ISBN AS ISBN, libros.Titulo AS Titulo, libros.Editorial AS Editorial
                                FROM Access_TaPrestamos prestamos, Access_TaLibros libros
                                WHERE prestamos.ISBN = libros.ISBN AND prestamos.DNI = '{usuario.Dni}'";
            }
            OleDbDataAdapter puenteConLaTabla = new OleDbDataAdapter(cadenaSQL, connection);
            DataTable tablaDeLaBD = new DataTable();
            puenteConLaTabla.Fill(tablaDeLaBD);
            dgvPrestamos.DataSource = tablaDeLaBD;
        }

        private void Prestamos_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.Show();
        }

        public bool checkISBN(string cadena)
        {
            txtISBN.Focus();
            return cadena.Contains(" ") || cadena == "";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkISBN(txtISBN.Text))
            {
                cadenaSQL = $@"SELECT prestamos.ISBN AS ISBN, libros.Titulo AS Titulo, libros.Editorial AS Editorial
                                FROM Access_TaPrestamos prestamos, Access_TaLibros libros
                                WHERE prestamos.ISBN = libros.ISBN
                                AND prestamos.ISBN = '{txtISBN.Text}'";
                OleDbDataAdapter puenteConLaTabla = new OleDbDataAdapter(cadenaSQL, connection);
                DataTable tablaDeLaBD = new DataTable();
                puenteConLaTabla.Fill(tablaDeLaBD);
                dgvPrestamos.DataSource = tablaDeLaBD;
            }
            else
            {
                if (txtISBN.Text == "") 
                {
                    RellenarCuadricula();
                }
                MessageBox.Show("El ISBN no es valido");
                txtISBN.Focus();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ResetDatos();
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            try
            {
                string cadenaSQLUpdate = $@"UPDATE Access_TaLibros
                                            SET ISBN = '{txtISBN.Text}',
                                                Cantidad = Cantidad + 1
                                            WHERE ISBN = '{txtISBN.Text}';";
                OleDbCommand instruccionSQLUpdate = new OleDbCommand(cadenaSQLUpdate, connection);
                connection.Open();
                instruccionSQLUpdate.ExecuteNonQuery();
                string cadenaSQLDelete = $@"DELETE FROM Access_TaPrestamos
                                                WHERE ISBN = '{txtISBN.Text}' AND DNI = '{usuario.Dni}'";
                OleDbCommand instruccionSQLDelete = new OleDbCommand(cadenaSQLDelete, connection);
                instruccionSQLDelete.ExecuteNonQuery();
                MessageBox.Show("Libro devuelto");
                connection.Close();
                RellenarCuadricula();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error devolviendo el libro. Error: " + ex.Message);
                connection.Close();
            }
        }
    }
}
