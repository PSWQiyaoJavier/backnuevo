using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.MetodoFabrica
{
    [Table("vendedorfinal")]
    public class UsuarioVendedor : UsuarioFabrica
    {
        
        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }
        public UsuarioVendedor(string nombre, string nick_name, string contraseña, string email, int edad)
            : base(nombre, nick_name, contraseña, email, edad)
        {}

        public UsuarioVendedor()
        {
            // Puedes inicializar propiedades comunes aquí si es necesario
        }
    }
}