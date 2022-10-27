using System.Data;

namespace CoderHouse_SistemaGestion.Models
{
    public class Usuario
    {
        private int _id;
        private string _nombre;
        private string _apellido;
        private string _nombreUsuario;
        private string _contresenna;
        private string _mail;

        public int Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Apellido { get { return _apellido; } set { _apellido = value; } }
        public string NombreUsuario { get { return _nombreUsuario; } set { _nombreUsuario = value; } }
        public string Contrasenna { get { return _contresenna; } set { _contresenna = value; } }
        public string Mail { get { return _mail; } set { _mail = value; } }

        public Usuario()
        {
            Id = 0;
            Nombre = string.Empty;
            Apellido = string.Empty;
            NombreUsuario = string.Empty;
            Contrasenna = string.Empty;
            Mail = string.Empty;
        }

        public Usuario(int id, string nombre,string apellido, string nombreUsuario, string contrsenna, string mail)
        {
            Id=id;
            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreUsuario;
            Contrasenna = contrsenna;
            Mail = mail;
        }



    }
}
