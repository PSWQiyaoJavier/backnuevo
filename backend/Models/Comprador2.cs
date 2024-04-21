using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("usuario")]
    public class Comprador2 : BaseModel
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
        // Otros campos que puedas necesitar


        // Relación con Cliente si es necesario

    }
}
