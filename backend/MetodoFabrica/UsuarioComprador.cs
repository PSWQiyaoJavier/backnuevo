using Postgrest.Attributes;
using Postgrest.Models;
using backend.PatronObservador;
using backend.Models;

namespace backend.MetodoFabrica
{
    [Table("comprador")]
    public class UsuarioComprador : UsuarioFabrica, IObservador
    {


        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }



        public Dictionary<Producto, int> Carritolista { get; set; }

        public List<Producto> Guardadoslista { get; set;}

        public List<Pedidopoo> Pedidospoo {get; set;}

        public List<Producto> Deseoslista {get; set;}

        public UsuarioComprador(string nombre, string nick_name, string contraseña, string email, int edad, int limite)
            : base(nombre, nick_name, contraseña, email, edad)
        {
            Limite_gasto_cents_mes = limite;
        }

        public UsuarioComprador()
        {
            // Puedes inicializar propiedades comunes aquí si es necesario
        }

        public void AgregarProductoGuardado(Producto producto)
        {
            Guardadoslista.Add(producto);
            producto.AgregarObservador(this);
        }

        public void Actualizar()
        {
            // Lógica para manejar la actualización del Producto
            Console.WriteLine("Un producto de la lista guardados para más tarde ha sido actualizado. Deberías tomar alguna acción " + Nombre);
        }

        public void AgregarProductoCarrito(Producto producto, int cantidad)
        {
            if (Carritolista.ContainsKey(producto))
            {
                Carritolista[producto] += cantidad; // Si el producto ya está en el carrito, se incrementa la cantidad
            }
            else
            {
                Carritolista.Add(producto, cantidad); // Si es un producto nuevo, se agrega al carrito con la cantidad
            }
        }

        public void AgregarProductoDeseos(Producto producto)
        {
            Deseoslista.Add(producto);
        }

        public void EliminarProductoDeseos(Producto producto)
        {
            Deseoslista.Remove(producto);
        }

        public void EliminarProductoGuardados(Producto producto)
        {
            Guardadoslista.Remove(producto);
            producto.EliminarObservador(this);
        }

        public void EliminarProductoCarrito(Producto producto)
        {
            if(Carritolista.ContainsKey(producto))
            {
                Carritolista.Remove(producto);
            }
            
        }

        public void ModificarCantidadEnCarrito(Producto producto, int nuevaCantidad)
        {
            if (Carritolista.ContainsKey(producto))
            {
                Carritolista[producto] = nuevaCantidad;
            }
        }

        public Dictionary<Producto, int> ObtenerCarrito()
        {
            return Carritolista;
        }

        public Pedidopoo ConvertirCarritoEnPedido()
        {
            Pedidopoo pedido = new Pedidopoo();
            pedido.Id_comprador = this.Id;
            foreach (var item in Carritolista)
            {
                pedido.Productos.Add(new PedidoProducto
                {
                    Producto = item.Key,
                    Cantidad = item.Value
                });
            }
        return pedido;
        }
        public Pedidopoo RealizarPedido()
        {
            if (Carritolista.Count > 0)
            {
                // Convertir el carrito en un pedido
                Pedidopoo pedido = ConvertirCarritoEnPedido();
                if(Pedidospoo == null)
                {
                    Pedidospoo = new List<Pedidopoo>();
                }
                Pedidospoo.Add(pedido);
                pedido.RestarCantidadProductosDeInventario();
                return pedido;
                //Carritolista.Clear();
            }
            return null;
        }

        public List<Pedidopoo> ObtenerPedidos()
        {
            return Pedidospoo;
        }
    }
}