using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("guardadoparamastarde")]
    public class Guardadoparamastarde : BaseModel
    {

        [Column("id_comprador")]
        public int Id_comprador { get; set; }
        
        [Column("id_producto")]
        public int Id_producto { get; set; }

    }
}