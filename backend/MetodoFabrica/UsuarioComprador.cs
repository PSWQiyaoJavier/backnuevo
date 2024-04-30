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



        public List<Producto> Carritolista { get; set; }

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

        public void AgregarProductoCarrito(Producto producto)
        {
            Carritolista.Add(producto);
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
            Carritolista.Remove(producto);
        }

        public Pedidopoo ConvertirCarritoEnPedido(List<int> cantidades)
        {
            Pedidopoo pedido = new Pedidopoo(Carritolista,cantidades, this);
            Carritolista.Clear(); // Limpiar el carrito después de convertirlo en pedido
            Pedidospoo.Add(pedido); // Agregar el pedido a la lista de pedidos del comprador
            return pedido;
        }

        // Métodos para manejar la lista de guardados...

        // Métodos para manejar la lista de pedidos...
        public void AgregarPedido(Pedidopoo pedido)
        {
            Pedidospoo.Add(pedido);
        }

        public List<Pedidopoo> ObtenerPedidos()
        {
            return Pedidospoo;
        }
    }
}