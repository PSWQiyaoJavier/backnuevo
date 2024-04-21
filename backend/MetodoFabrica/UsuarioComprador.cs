using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.MetodoFabrica
{
    [Table("compradorfinal")]
    public class UsuarioComprador : UsuarioFabrica
    {
        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }

        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }

        public UsuarioComprador(string nombre, string nick_name, string contraseña, string email, int edad, int limite)
            : base(nombre, nick_name, contraseña, email, edad)
        {
            Limite_gasto_cents_mes = limite;
        }

        public UsuarioComprador()
        {
            // Puedes inicializar propiedades comunes aquí si es necesario
        }
    }
}