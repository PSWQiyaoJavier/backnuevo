using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("listadeseos")]
    public class Listadeseos : BaseModel
    {

        [PrimaryKey]
        [Column("id_comprador")]
        public int Id_comprador { get; set; }
        
        [PrimaryKey]
        [Column("id_producto")]
        public int Id_producto { get; set; }

    }
}