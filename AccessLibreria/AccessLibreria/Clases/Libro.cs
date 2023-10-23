using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessLibreria
{
    public class Libro
    {
        string isbn, titulo, editorial;

        public Libro(string isbn, string titulo, string editorial)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.editorial = editorial;
        }

        public string Isbn { get => isbn; set => isbn = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Editorial { get => editorial; set => editorial = value; }
    }
}
