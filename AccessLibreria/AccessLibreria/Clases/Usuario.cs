using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLibreria
{
    public class Usuario
    {
        string dni, nombre, apellidos;
        int edad;
        bool esAdministrador;

        public Usuario(string dni, string nombre, string apellidos, int edad, bool esAdministrador)
        {
            Dni = dni;
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
            EsAdministrador = esAdministrador;
        }

        public string Dni { get => dni; set => dni = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public int Edad { get => edad; set => edad = value; }
        public bool EsAdministrador { get => esAdministrador; set => esAdministrador = value; }
    }
}
