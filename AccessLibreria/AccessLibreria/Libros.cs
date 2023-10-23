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
    public partial class Libros : Form
    {
        Inicio main;
        Usuario usuario;
        OleDbConnection connection;

        public Libros()
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
            set { usuario = value; }
        }

        private void Libros_Load(object sender, EventArgs e)
        {
            if (usuario.EsAdministrador)
            {
                txtCantidad.ReadOnly = false;
                btnAgregar.Visible = true;
                btnEliminar.Visible = true;
                btnModificar.Visible = true;
                btnAlquilar.Visible = false;
            }
            else 
            {
                txtCantidad.ReadOnly = true;
                btnAgregar.Visible = false;
                btnEliminar.Visible = false;
                btnModificar.Visible = false;
                btnAlquilar.Visible = true;
            }
            dgvLibros.AllowUserToAddRows = false;
            dgvLibros.AllowUserToDeleteRows = false;
            dgvLibros.ReadOnly = true;
            dgvLibros.AutoResizeColumns();
            dgvLibros.AutoResizeRows();
            dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLibros.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvLibros.DefaultCellStyle.SelectionBackColor = dgvLibros.DefaultCellStyle.BackColor;
            dgvLibros.DefaultCellStyle.SelectionForeColor = dgvLibros.DefaultCellStyle.ForeColor;
            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string projectPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(exePath, @"..\..\.."));
            string dbPath = System.IO.Path.Combine(projectPath, "Access_DbLibreria.accdb");
            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath);
            RellenarCuadricula();
        }

        void ResetDatos()
        {
            txtISBN.Text = string.Empty;
            txtTitulo.Text = string.Empty;
            txtEditorial.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtISBN.Focus();
        }

        void RellenarCuadricula()
        {
            string cadenaSQL;
            cadenaSQL = @"
                SELECT libro.ISBN, libro.Titulo, libro.Editorial, libro.Cantidad
                FROM Access_TaLibros libro
                ORDER BY ISBN";
            OleDbDataAdapter puenteConLaTabla = new OleDbDataAdapter(cadenaSQL, connection);
            DataTable tablaDeLaBD = new DataTable();
            puenteConLaTabla.Fill(tablaDeLaBD);
            dgvLibros.DataSource = tablaDeLaBD;
        }

        private void Libros_FormClosing(object sender, FormClosingEventArgs e)
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
       
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkISBN(txtISBN.Text))
            {
                if (int.TryParse(txtCantidad.Text, out int quantity) && quantity > 0)
                {
                    cadenaSQL = $@" INSERT INTO Access_TaLibros
                                    (ISBN, Titulo, Editorial, Cantidad)
                                VALUES
                                    (@ISBN, @Titulo, @Editorial, @Cantidad);";
                    OleDbCommand instruccionSQL = new OleDbCommand(cadenaSQL, connection);
                    instruccionSQL.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                    instruccionSQL.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                    instruccionSQL.Parameters.AddWithValue("@Editorial", txtEditorial.Text);
                    instruccionSQL.Parameters.AddWithValue("@Cantidad", quantity);
                    try
                    {
                        connection.Open();
                        int data = instruccionSQL.ExecuteNonQuery();
                        connection.Close();
                        if (data == 0)
                        {
                            MessageBox.Show("Error insertando el libro");
                        }
                        else
                        {
                            MessageBox.Show("Libro insertado correctamente");
                            RellenarCuadricula();
                            ResetDatos();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        connection.Close();
                    }
                }
                else 
                {
                    MessageBox.Show("Cantidad debe ser un numero positivo");
                    txtCantidad.Focus();
                }
            }
            else
            {
                MessageBox.Show("El ISBN no es valido");
                txtISBN.Focus();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkISBN(txtISBN.Text))
            {
                cadenaSQL = $@"SELECT libro.ISBN, libro.Titulo, libro.Editorial, libro.Cantidad
                                FROM Access_TaLibros libro
                                WHERE libro.ISBN = '{txtISBN.Text}'";
                OleDbDataAdapter puenteConLaTabla = new OleDbDataAdapter(cadenaSQL, connection);
                DataTable tablaDeLaBD = new DataTable();
                puenteConLaTabla.Fill(tablaDeLaBD);
                dgvLibros.DataSource = tablaDeLaBD;        
            }
            else
            {
                MessageBox.Show("EL ISBN no es valido");
                txtISBN.Focus();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkISBN(txtISBN.Text))
            {
                if (int.TryParse(txtCantidad.Text, out int quantity) && quantity > 0)
                {
                    cadenaSQL = $@"UPDATE Access_TaLibros
                                SET ISBN = @ISBN,
                                    Titulo = @Titulo,
                                    Editorial = @Editorial,
                                    Cantidad = @Cantidad
                                WHERE ISBN = @ISBN;";
                    OleDbCommand instruccionSQL = new OleDbCommand(cadenaSQL, connection);
                    instruccionSQL.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                    instruccionSQL.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                    instruccionSQL.Parameters.AddWithValue("@Editorial", txtEditorial.Text);
                    instruccionSQL.Parameters.AddWithValue("@Cantidad", quantity);
                    try
                    {
                        connection.Open();
                        int data = instruccionSQL.ExecuteNonQuery();
                        if (data == 0)
                        {
                            MessageBox.Show("Error actualizando el libro");
                        }
                        else
                        {
                            ResetDatos();
                            RellenarCuadricula();
                            MessageBox.Show("Libro actuliazado");
                        }
                        connection.Close();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        connection.Close();
                    }
                }
                else 
                {
                    MessageBox.Show("Cantidad debe ser un numero positivo");
                    txtCantidad.Focus();
                }
            }
            else
            {
                MessageBox.Show("El ISBN no es valido");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkISBN(txtISBN.Text))
            {
                cadenaSQL = $@"DELETE FROM Access_TaLibros
                                WHERE ISBN = @ISBN;";
                OleDbCommand instruccionSQL2 = new OleDbCommand(cadenaSQL, connection);
                instruccionSQL2.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                try
                {
                    connection.Open();
                    int data = instruccionSQL2.ExecuteNonQuery();
                    connection.Close();
                    if (data == 0)
                    {
                        MessageBox.Show("Error borrando el libro");
                    }
                    else
                    {
                        ResetDatos();
                        RellenarCuadricula();
                        MessageBox.Show("Libro eliminado correctamente");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("El ISBN no es valido");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ResetDatos();
        }

        private void btnAlquilar_Click(object sender, EventArgs e)
        {
            string cadenaSQLSelect;
            if (!checkISBN(txtISBN.Text))
            {
                cadenaSQLSelect = $@"SELECT libro.Cantidad
                                FROM Access_TaLibros libro
                                WHERE libro.ISBN = '{txtISBN.Text}'";
                OleDbCommand instruccionSQL = new OleDbCommand(cadenaSQLSelect, connection);
                try
                {
                    connection.Open();
                    OleDbDataReader data = instruccionSQL.ExecuteReader();
                    if (data.Read())
                    {
                        if (int.Parse(data["Cantidad"].ToString()) > 0)
                        {
                            string cadenaSQLUpdate = $@"UPDATE Access_TaLibros
                                                            SET ISBN = '{txtISBN.Text}',
                                                                Cantidad = Cantidad - 1
                                                            WHERE ISBN = '{txtISBN.Text}';";
                            OleDbCommand instruccionSQLUpdate = new OleDbCommand(cadenaSQLUpdate, connection);
                            instruccionSQLUpdate.ExecuteNonQuery();
                            string cadenaSQLInsert = $@"INSERT INTO Access_TaPrestamos
                                                            (ISBN,  DNI)
                                                        VALUES
                                                            (@ISBN, @DNI)";
                            OleDbCommand instruccionSQLInsert = new OleDbCommand(cadenaSQLInsert, connection);
                            instruccionSQLInsert.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                            instruccionSQLInsert.Parameters.AddWithValue("@DNI", usuario.Dni);
                            int confirmation = instruccionSQLInsert.ExecuteNonQuery();
                            if (confirmation == 0)
                            {
                                MessageBox.Show("Error insertando alquilando el libro");
                            }
                            else
                            {
                                MessageBox.Show($"Libro alquilado correctamente");
                                RellenarCuadricula();
                            }
                        }
                        else 
                        {
                            MessageBox.Show("No hay unidades para alquilar");
                        }
                    }
                    else 
                    {
                        MessageBox.Show("No existe ese libro en la libreria");
                    }
                    connection.Close();
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("EL ISBN no es valido");
                txtISBN.Focus();
            }
        }
    }
}
