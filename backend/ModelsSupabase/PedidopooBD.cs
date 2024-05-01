using Postgrest.Attributes;
using Postgrest.Models;
using backend.MetodoFabrica;

namespace backend.ModelsSupabase
{   
    [Table("pedido")]    
    public class PedidopooBD : BaseModel
        {
            [PrimaryKey("id", false)]
            public int Id { get; set; }

            [Column("id_comprador")]
            public int Id_comprador { get; set; }

            [Column("randomid")]
            public int RandomId { get; set; }
        }
}