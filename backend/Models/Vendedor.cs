using Postgrest.Attributes;
using Postgrest.Models;


namespace backend.Models
{
    [Table("vendedorfinal")]
    public class Vendedor : Usuario
    {
        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }
    }
}