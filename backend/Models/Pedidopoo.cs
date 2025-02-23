using Postgrest.Attributes;
using Postgrest.Models;
using backend.MetodoFabrica;
using backend.Logica;
using backend.PatronEstrategia;

namespace backend.Models
{    
    public class Pedidopoo : BaseModel
        {

            public int Id { get; set; }

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
            
            public void pagar(EstrategiaPago formapago, int total)
            {
                formapago.ProcesarPago(total);
            }


        }
}