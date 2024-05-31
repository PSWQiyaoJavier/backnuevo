using backend.Services;
using backend.Models;
using System.Collections.Generic;
using backend.MetodoFabrica;
using backend.PatronObservador;
using System.Runtime.InteropServices;
using backend.ModelsSupabase;
using System.Runtime.ConstrainedExecution;

namespace backend.FachadaBD
{
    public class ClaseFachadaBD : InterfazFachadaBD
    {
        private readonly SupabaseService interf;

        public ClaseFachadaBD(IConfiguration configuration)
        {
            interf = SupabaseService.GetInstance(configuration);
        }

        public int PedidoBD(int idComprador)
        {
            int randomnumber = GenerarIdUnico();
            PedidopooBD nuevoElemento = new PedidopooBD
            {
                Id_comprador = idComprador,
                RandomId = randomnumber
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };
            interf.InsertarPedido(nuevoElemento);  
            return randomnumber;
        }

        public int GenerarIdUnico()
        {
            // Aquí se genera un ID único provisional para el pedido (puede ser una implementación simple basada en un contador o un UUID)
            return new Random().Next(MinRandomId, MaxRandomId);
        }

        private const int MinRandomId = 1000;
        private const int MaxRandomId = 9999;

        public void ActualizarUnidadesBD(Pedidopoo ped)
        {
            try{
                foreach (var productoPedido in ped.Productos)
                    {
                        Producto producto = productoPedido.Producto;
                        int cantidadComprada = productoPedido.Cantidad;

                        // Restar la cantidad comprada del inventario del producto
                        interf.UpdateCantidadProducto(producto,cantidadComprada);
                        
                    }
                }catch(Exception ex){
                Console.WriteLine("Error : " + ex.Message);
            throw; // Lanza la excepción para propagarla hacia arriba
            }
            
        }

        public IList<Producto> ObtenerProductos()
        {
            var productosTask = interf.GetAllProducts(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<ProductoBD> productos1 = productosTask.Result;
            List<Producto> productos = new List<Producto>();
            foreach (var producto in productos1)
            {
                Producto nuevo = new Producto{
                    Id= producto.Id,
                    Nombre= producto.Nombre,
                    Categoria= producto.Categoria,
                    Descripcion= producto.Descripcion,
                    Cantidad= producto.Cantidad,
                    Precio= producto.Precio,
                    Foto1= producto.Foto1,
                    Foto2= producto.Foto2,
                    Foto3= producto.Foto3
                };
                productos.Add(nuevo);
            }
            return productos;
        }

        public IList<CarritoCompra> ObtenerChart()
        {
            var productosTask = interf.GetChart(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<CarritoCompra> productos1 = productosTask.Result;
            return productos1;
        }

        public IList<Guardadoparamastarde> ObtenerGuardados()
        {
            var productosTask = interf.GetGuardados(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Guardadoparamastarde> productos1 = productosTask.Result;
            return productos1;
        }

        public IList<Listadeseos> ObtenerDeseos()
        {
            var productosTask = interf.GetDeseos(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Listadeseos> productos1 = productosTask.Result;
            return productos1;
        }


        public IList<UsuarioFabrica> ObtenerUsuarios()
        {
            var productosTask = interf.GetAllUsers(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<UsuarioFabrica> productos1 = productosTask.Result;
            return productos1;
        }

        public IList<UsuarioComprador> ObtenerCompradores()
        {
            var productosTask = interf.GetAllBuyers(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<UsuarioCompradorBD> productos1 = productosTask.Result;
            List<UsuarioComprador> compradors = new List<UsuarioComprador>();
            foreach (var nuevouser in productos1)
            {
                UsuarioComprador nuevo = new UsuarioComprador{
                    Id= nuevouser.Id,
                    Nombre= nuevouser.Nombre,
                    Nick_name= nuevouser.Nick_name,
                    Contraseña= nuevouser.Contraseña,
                    Email= nuevouser.Email,
                    Edad= nuevouser.Edad,
                    Limite_gasto_cents_mes= nuevouser.Limite_gasto_cents_mes
                };
                compradors.Add(nuevo);
            }
            return compradors;
        }

        public async Task AddFactoryMember(UsuarioComprador nuevouser)
        {

            
            IList<UsuarioComprador> allUsers = ObtenerCompradores();


            bool nicknamebool = allUsers.Any(u => u.Nick_name == nuevouser.Nick_name);

            // Verificar si ya existe un miembro con el mismo correo electrónico
            bool emailbool = allUsers.Any(u => u.Email == nuevouser.Email);


            if (!nicknamebool && !emailbool)
            {
                await interf.InsertarBuyerFactory(nuevouser);
            }
            else
            {
                if (nicknamebool)
                    throw new Exception("El member con nick " + nuevouser.Nick_name + " ya existe.");

                if (emailbool)
                    throw new Exception("El member con correo electrónico " + nuevouser.Email + " ya existe.");
            }

        }

        public async void PedidoProductoBD(int idComprador, int random1, List<UsuarioComprador> _compradores)
        {
            PedidopooBD ped = await interf.PedidoByRandom(random1);
            UsuarioComprador comprador = _compradores.FirstOrDefault(p => p.Id == idComprador);
            foreach (var productopedido in comprador.Carritolista)
            {
                PedidoProductoBD nuevoElemento = new PedidoProductoBD
                {
                    Id_pedido = ped.Id,
                    Id_Producto = productopedido.Key.Id,
                    Cantidad = productopedido.Value
                    // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
                };
                interf.InsertarPedidoproducto(nuevoElemento);
            }

            
            comprador.Carritolista.Clear();
            
        }

        public  UsuarioComprador ObtenerUsuarioCompradorPorNick(string nick)
        {

            var productosTask = interf.UserBuyerByNick(nick); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            UsuarioComprador user1 = productosTask.Result;
            return user1;
        }

        public  Producto ObtenerProductoPorPrecio(int nick)
        {

            var productosTask = interf.ProductByPrice(nick); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            Producto user1 = productosTask.Result;
            return user1;
        }

        public  Producto ObtenerProductoPorId(int id)
        {

            var productosTask = interf.ProductById(id); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            Producto user1 = productosTask.Result;
            return user1;
        }

        public void AgregarAlCarrito(int usuarioId, int productoId, int cantidad)
        {
            // Aquí iría la lógica para insertar el nuevo elemento en la tabla CarritoCompra
            // Por ejemplo:
            CarritoCompra nuevoElemento = new CarritoCompra
            {
                Id_comprador = usuarioId,
                Id_producto = productoId,
                Cantidad = cantidad
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };

            interf.InsertarCarrito(nuevoElemento);
            
        }

        public void AgregarADeseos(int usuarioId, int productoId)
        {
            Listadeseos nuevoElemento = new Listadeseos
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
            };

            interf.InsertarDeseo(nuevoElemento); 
        }

        public void AgregarAGuardados(int usuarioId, int productoId)
        {

            Guardadoparamastarde nuevoElemento = new Guardadoparamastarde
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };

            interf.InsertarGuardado(nuevoElemento);
            
        }

        public void EliminarAlCarrito(int usuarioId, int productoId)
        {
            CarritoCompra nuevoElemento = new CarritoCompra
            {
                Id_comprador = usuarioId,
                Id_producto = productoId
            };
            interf.EliminarCarrito(nuevoElemento);
        }

        public void EliminarAlGuardado(int usuarioId, int productoId)
        {
            interf.EliminarGuardado(usuarioId, productoId);
        }

        public void EliminarAlDeseo(int usuarioId, int productoId)
        {
            interf.EliminarDeseo(usuarioId,productoId);
        }
        public void InsertarPedidoproducto(PedidoProductoBD nuevoElemento)
        {
            interf.InsertarPedidoproducto(nuevoElemento);
        }        
    }

}