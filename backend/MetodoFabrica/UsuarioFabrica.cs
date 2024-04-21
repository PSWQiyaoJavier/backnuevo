using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.MetodoFabrica
{
    [Table("usuario")]
    public class UsuarioFabrica : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }


        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("nick_name")]
        public string Nick_name { get; set; }

        [Column("contraseña")]
        public string Contraseña { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("edad")]
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
