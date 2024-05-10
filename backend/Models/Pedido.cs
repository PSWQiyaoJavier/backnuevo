using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("pedido")]
    public class Pedido : BaseModel
    {

        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("id_comprador")]
        public int Id_comprador { get; set; }

        [Column("precio_total")]
        public float Precio_total { get; set; }

    }
}