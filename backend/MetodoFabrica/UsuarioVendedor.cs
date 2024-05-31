using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.MetodoFabrica
{
    public class UsuarioVendedor : UsuarioFabrica
    {

        public UsuarioVendedor(string nombre, string nick_name, string contraseña, string email, int edad)
            : base(nombre, nick_name, contraseña, email, edad)
        {}

        public UsuarioVendedor()
        {
            // Puedes inicializar propiedades comunes aquí si es necesario
        }
    }
}