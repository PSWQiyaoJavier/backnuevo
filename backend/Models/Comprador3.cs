using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("comprador2")]
    public class Comprador3 : Usuario
    {

        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }

        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }
    }
}