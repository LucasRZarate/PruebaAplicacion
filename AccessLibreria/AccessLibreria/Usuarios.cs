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
    public partial class Usuarios : Form
    {
        Inicio main;
        OleDbConnection connection;

        public Usuarios()
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

        private void Usuarios_Load(object sender, EventArgs e)
        {
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AutoResizeColumns();
            dgvUsuarios.AutoResizeRows();
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvUsuarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = dgvUsuarios.DefaultCellStyle.BackColor;
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = dgvUsuarios.DefaultCellStyle.ForeColor;
            string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            string projectPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(exePath, @"..\..\.."));
            string dbPath = System.IO.Path.Combine(projectPath, "Access_DbLibreria.accdb");
            connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath);
            RellenarCuadricula();
        }

        bool checkDNI(string cadena)
        {
            txtDni.Focus();
            return cadena.Contains(" ") || cadena == "";
        }

        int isAgeNumber(string age) 
        {
            if (int.TryParse(age, out int ageNum))
            {
                return ageNum;
            }
            else 
            {
                return -1;
            }
        }

        bool isValidAge(int age) 
        {
            if (age < 100 && age > 5)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        void ResetDatos()
        {
            txtDni.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtDni.Focus();
        }

        void RellenarCuadricula()
        {
            string cadenaSQL;
            cadenaSQL = @"
                SELECT DNI AS Dni, Nombre AS Nombre, Apellidos AS Apellidos, Edad AS Edad, EsAdministrador AS [Es administrador]
                FROM Access_TaUsuarios
                ORDER BY Dni";
            OleDbDataAdapter puenteConLaTabla = new OleDbDataAdapter(cadenaSQL, connection);
            DataTable tablaDeLaBD = new DataTable();
            puenteConLaTabla.Fill(tablaDeLaBD);
            dgvUsuarios.DataSource = tablaDeLaBD;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkDNI(txtDni.Text))
            {
                cadenaSQL = $@"SELECT *
                               FROM Access_TaUsuarios
                               WHERE DNI = '{txtDni.Text}'";
                OleDbDataAdapter puenteConLaTabla = new OleDbDataAdapter(cadenaSQL, connection);
                DataTable tablaDeLaBD = new DataTable();
                puenteConLaTabla.Fill(tablaDeLaBD);
                dgvUsuarios.DataSource = tablaDeLaBD;
            }
            else
            {
                if (txtDni.Text == "")
                {
                    RellenarCuadricula();
                }
                else 
                { 
                    MessageBox.Show("El Dni no es valido");
                }
                txtDni.Focus();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkDNI(txtDni.Text))
            {
                int age = isAgeNumber(txtEdad.Text);
                if (age != -1)
                {
                    if (isValidAge(age))
                    {
                        cadenaSQL = $@" UPDATE Access_TaUsuarios
                                            SET DNI = @Dni,
                                                Nombre = @Nombre,
                                                Apellidos = @Apellidos,
                                                Edad = @Edad
                                            WHERE DNI = @Dni;";
                        OleDbCommand instruccionSQL = new OleDbCommand(cadenaSQL, connection);
                        instruccionSQL.Parameters.AddWithValue("@Dni", txtDni.Text);
                        instruccionSQL.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        instruccionSQL.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                        instruccionSQL.Parameters.AddWithValue("@Edad", age);
                        try
                        {
                            connection.Open();
                            int data = instruccionSQL.ExecuteNonQuery();
                            if (data == 0)
                            {
                                MessageBox.Show("Error actualizando al usuario");
                            }
                            else
                            {
                                ResetDatos();
                                RellenarCuadricula();
                                MessageBox.Show("Usuario actuliazado");
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
                        MessageBox.Show("Edad no valida (La edad es valida entre 5 y 100 años)");
                        txtEdad.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("La edad debe ser un numero");
                    txtEdad.Focus();
                }
            }
            else
            {
                MessageBox.Show("El Dni no es valido");
                txtDni.Focus();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string cadenaSQL, cadenaSQL2;
            if (!checkDNI(txtDni.Text))
            {
                cadenaSQL2 = $@"SELECT * 
                                FROM Access_TaPrestamos
                                WHERE DNI = @Dni";                            
                OleDbCommand instruccionSQL2 = new OleDbCommand(cadenaSQL2, connection);   
                instruccionSQL2.Parameters.AddWithValue("@Dni", txtDni.Text);
                try
                {
                    connection.Open();            
                    OleDbDataReader data2 = instruccionSQL2.ExecuteReader();
                    if (data2.Read())
                    {
                        MessageBox.Show("El usuario tiene prestamos, devuelva primero los libros");
                    }
                    else
                    {
                        cadenaSQL = $@"DELETE FROM Access_TaUsuarios
                               WHERE Dni = @Dni";
                        OleDbCommand instruccionSQL = new OleDbCommand(cadenaSQL, connection);
                        instruccionSQL.Parameters.AddWithValue("@Dni", txtDni.Text);
                        int data = instruccionSQL.ExecuteNonQuery();
                        if (data == 0)
                        {
                            MessageBox.Show("Error borrando al usuario");
                        }
                        else
                        {
                            ResetDatos();
                            RellenarCuadricula();
                            MessageBox.Show("Usuario eliminado correctamente");
                        }
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
                MessageBox.Show("El Dni no es valido");
                txtDni.Focus();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ResetDatos();
        }

        private void Usuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.Show();
        }
    }
}
