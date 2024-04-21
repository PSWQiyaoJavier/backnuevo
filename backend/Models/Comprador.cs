using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("compradorfinal")]
    public class Comprador : Usuario
    {
        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }

        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }

    }
}