using Postgrest.Attributes;
using Postgrest.Models;
using backend.PatronObservador;
using backend.Models;

namespace backend.ModelsSupabase
{
    [Table("comprador")]
    public class UsuarioCompradorBD : BaseModel
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

        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }

    }
}