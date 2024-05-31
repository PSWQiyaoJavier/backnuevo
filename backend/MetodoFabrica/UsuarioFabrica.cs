using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.MetodoFabrica
{

    public class UsuarioFabrica : BaseModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Nick_name { get; set; }

        public string Contraseña { get; set; }

        public string Email { get; set; }

        public int Edad { get; set; }

        public UsuarioFabrica(string nombre, string nick_name, string contraseña, string email, int edad)
        {
            Nombre = nombre;
            Nick_name = nick_name;
            Contraseña = contraseña;
            Email = email;
            Edad = edad;
        }
        public UsuarioFabrica()
        {
            // Puedes inicializar propiedades comunes aquí si es necesario
        }

    }
}
