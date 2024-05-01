using Postgrest.Attributes;
using Postgrest.Models;
using backend.MetodoFabrica;
using backend.Logica;

namespace backend.Models
{   
    [Table("pedido")]    
    public class Pedidopoo : BaseModel
        {
            [PrimaryKey]
            [Column("id")]
            public int Id { get; set; }

            [Column("id_comprador")]
            public int Id_comprador { get; set; }

            public List<PedidoProducto> Productos { get; set; }

            public Pedidopoo()
            {
                Productos = new List<PedidoProducto>();
            }

            public void RestarCantidadProductosDeInventario()
            {
                foreach (var productoPedido in Productos)
                {
                    Producto producto = productoPedido.Producto;
                    int cantidadComprada = productoPedido.Cantidad;

                    // Restar la cantidad comprada del inventario del producto
                    producto.CambiarUnidades(cantidadComprada);
                    
                }
            }
    



        }
}