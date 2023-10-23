using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessLibreria
{
    public partial class Logging : Form
    {
        OleDbConnection connection;

        public Logging()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string cadenaSQL = $@"SELECT * FROM Access_TaUsuarios WHERE DNI = '{txtInicioDNI.Text}' AND Contraseña = '{txtInicioPassword.Text}'";
            OleDbCommand instruccionSQL = new OleDbCommand(cadenaSQL, connection);
            try
            {
                connection.Open();
                OleDbDataReader data = instruccionSQL.ExecuteReader();
                
                if (data.Read())
                {
                    bool admin;
                    if (data["EsAdministrador"].ToString() == "True")
                    {
                        admin = true;
                    }
                    else 
                    {
                        admin = false;
                    }
                    Usuario user = new Usuario(data["DNI"].ToString(), data["Nombre"].ToString(), data["Apellidos"].ToString(), int.Parse(data["Edad"].ToString()), admin);
                    Inicio inicio = new Inicio();
                    inicio.User = user;
                    inicio.Main = this;
                    inicio.Show();
                    Hide();
                    connection.Close();
                    LimpairDatos();
                }
                else 
                {
                    MessageBox.Show("Usuario no encontrado");
                    connection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Usuario no encontrado");
                connection.Close();
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            if (!checkDNI(txtRegDNI.Text))
            {
                int age = isAgeNumber(txtRegEdad.Text);
                if (age != -1)
                {
                    if (isValidAge(age))
                    {
                        cadenaSQL = $@" INSERT INTO Access_TaUsuarios
                                            (DNI, Contraseña, Nombre, Apellidos, Edad, EsAdministrador)
                                        VALUES
                                            (@Dni, @Contraseña, @Nombre, @Apellidos, @Edad, @EsAdmin)";
                        OleDbCommand instruccionSQL = new OleDbCommand(cadenaSQL, connection);
                        instruccionSQL.Parameters.AddWithValue("@Dni", txtRegDNI.Text);
                        instruccionSQL.Parameters.AddWithValue("@Contraseña", txtRegPassword.Text);
                        instruccionSQL.Parameters.AddWithValue("@Nombre", txtRegNombre.Text);
                        instruccionSQL.Parameters.AddWithValue("@Apellidos", txtRegApellidos.Text);
                        instruccionSQL.Parameters.AddWithValue("@Edad", age);
                        instruccionSQL.Parameters.AddWithValue("@EsAdmin", cbRegAdmin.Checked);
                        try
                        {
                            connection.Open();
                            int data = instruccionSQL.ExecuteNonQuery();
                            if (data == 0)
                            {
                                MessageBox.Show("Error insertando el usuario");
                            }
                            else
                            {
                                Usuario user = new Usuario(txtRegDNI.Text, txtRegNombre.Text, txtRegApellidos.Text, age, cbRegAdmin.Checked);
                                Inicio inicio = new Inicio();
                                inicio.User = user;
                                inicio.Main = this;
                                inicio.Show();
                                Hide();
                                LimpairDatos();
                            }
                            connection.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Usuario con Dni ya existente");
                            connection.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Edad no valida (La edad es valida entre 5 y 100 años)");
                        txtRegEdad.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("La edad debe ser un numero");
                    txtRegEdad.Focus();
                }
            }
            else
            {
                MessageBox.Show("El Dni no es valido");
                txtRegDNI.Focus();
            }
        }

        private void Logging_Load(object sender, EventArgs e)
        {
            try 
            {
                string exePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                string projectPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(exePath, @"..\..\.."));
                string dbPath = System.IO.Path.Combine(projectPath, "Access_DbLibreria.accdb");
                connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath);
            }
            catch 
            {
                MessageBox.Show("Error conectando con la base de datos");
            }
        }

        void LimpairDatos() 
        {
            txtInicioDNI.Text = string.Empty;
            txtInicioPassword.Text = string.Empty;
            txtRegApellidos.Text = string.Empty;
            txtRegDNI.Text = string.Empty;
            txtRegEdad.Text = string.Empty;
            txtRegNombre.Text = string.Empty;
            txtRegPassword.Text = string.Empty;
            cbRegAdmin.Checked = false;
        }

        bool checkDNI(string cadena)
        {
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
    }
}
