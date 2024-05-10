using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("pedido_producto")]
    public class PedidoProducto : BaseModel
    {

        [Column("id_pedido")]
        public int Id_pedido { get; set; }
        
        [Column("id_producto")]
        public int Id_Producto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        public Producto Producto {get; set;}

    }
}