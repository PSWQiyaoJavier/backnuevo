using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Supabase;
using Supabase.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using backend.Models;
using Postgrest.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using backend.MetodoFabrica;
using Postgrest.Attributes;
using backend.ModelsSupabase;

namespace backend.Services
{
    public class SupabaseService : Interfaz
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseService(IConfiguration configuration)
        {
            var supabaseUrl = configuration["Supabase:Url"];
            var supabaseKey = configuration["Supabase:ApiKey"];

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl, supabaseKey, options);

             // Espera a que la inicialización se complete antes de devolver el control
        }

        public async Task InitializeSupabaseAsync()
        {
            Console.WriteLine("Iniciando la conexión a Supabase...");
            await _supabaseClient.InitializeAsync();
            Console.WriteLine("Conexión a Supabase completada.");
        }
        
        public async Task InsertarProducto(Producto nuevoProducto)
        {
            

            // Inserta el nuevo producto en la tabla correspondiente
            await _supabaseClient
                .From<Producto>()
                .Insert(nuevoProducto);
            Console.WriteLine("Producto insertado correctamente en Supabase.");
        }

        public async Task InsertarPedido(PedidopooBD nuevopedido)
        {
            
            try{
                // Inserta el nuevo producto en la tabla correspondiente
                await _supabaseClient
                    .From<PedidopooBD>()
                    .Insert(nuevopedido);
                Console.WriteLine("Pedido insertado correctamente en Supabase.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar pedido en Supabase: " + ex.Message);
                throw; // Lanza la excepción para propagarla hacia arriba
            }
        }

        public async Task InsertarPedidoproducto(PedidoProductoBD nuevopedido)
        {
            
            try{
                // Inserta el nuevo producto en la tabla correspondiente
                await _supabaseClient
                    .From<PedidoProductoBD>()
                    .Insert(nuevopedido);
                Console.WriteLine("Producto en pedido insertado correctamente en Supabase.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar producto en pedido en Supabase: " + ex.Message);
                throw; // Lanza la excepción para propagarla hacia arriba
            }
        }

        public async Task<List<Producto>> GetProductsById(int y)
        {
            var result = await _supabaseClient
                .From<Producto>()
                .Where(x => x.Id == y)
                .Get();
            List <Producto> productos = result.Models;
            return productos;
        }

        public async Task EliminarProducto(Producto producto)
        {
            await _supabaseClient
                .From<Producto>()
                .Delete(producto);
        }

        public async Task<Producto> ProductByPrice(int filtro)
        {
            var result = await _supabaseClient
                                .From<Producto>()
                                .Where(x => x.Precio == filtro)
                                .Get();
            
                                
            Producto users = result.Model;
            return users;                    
        }

        public async Task<Producto> ProductById(int id)
        {
            var result = await _supabaseClient
                                .From<Producto>()
                                .Where(x => x.Id == id)
                                .Get();
            
                                
            Producto users = result.Model;
            return users;                    
        }

        public async Task<List<ProductoBD>> GetAllProducts()
        {
            var productos = await _supabaseClient
                                .From<ProductoBD>()
                                .Get();

            List <ProductoBD> productos1 = productos.Models;
            return productos1;
        }

        public async Task<List<CarritoCompra>> GetChart()
        {
            var productos = await _supabaseClient
                                .From<CarritoCompra>()
                                .Get();

            List <CarritoCompra> productos1 = productos.Models;
            return productos1;
        }

        public async Task<List<Guardadoparamastarde>> GetGuardados()
        {
            var productos = await _supabaseClient
                                .From<Guardadoparamastarde>()
                                .Get();

            List <Guardadoparamastarde> productos1 = productos.Models;
            return productos1;
        }

        public async Task<List<Listadeseos>> GetDeseos()
        {
            var productos = await _supabaseClient
                                .From<Listadeseos>()
                                .Get();

            List <Listadeseos> productos1 = productos.Models;
            return productos1;
        }


        public async Task<List<UsuarioFabrica>> GetAllUsers()
        {
            var users = await _supabaseClient
                                .From<UsuarioFabrica>()
                                .Get();

            List <UsuarioFabrica> allusers = users.Models;
            return allusers;
        
        }




        public async Task<PedidopooBD> PedidoByRandom(int filtro)
        {
            var result = await _supabaseClient
                                .From<PedidopooBD>()
                                .Where(x => x.RandomId == filtro)
                                .Get();
            
                                
            PedidopooBD users = result.Model;
            return users;                    
        }
        public async Task<UsuarioComprador> UserBuyerByNick(string filtro)
        {
            var result = await _supabaseClient
                                .From<UsuarioComprador>()
                                .Where(x => x.Nick_name == filtro)
                                .Get();
            
                                
            UsuarioComprador users = result.Model;
            return users;                    
        }

        public async Task<List<UsuarioCompradorBD>> GetAllBuyers()
        {
            var users = await _supabaseClient
                                .From<UsuarioCompradorBD>()
                                .Get();

            List <UsuarioCompradorBD> allusers = users.Models;
            return allusers;
        
        }

        
/*
        public async Task<Comprador> BuyerByNick(string filtro)
        {
            var result = await _supabaseClient
                                .From<Comprador>()
                                .Select("*,usuario:nick_name")
                                .Where(x => x.Nick_name == filtro)
                                .Get();
            
                                
            Comprador users = result.Model;
            return users;                    
        }
*/

        public async Task InsertarUserFactory(UsuarioFabrica nuevouser)
        {
            try{

                // Inserta el nuevo producto en la tabla correspondiente
                await _supabaseClient
                        .From<UsuarioFabrica>()
                        .Insert(nuevouser);
                Console.WriteLine("User insertado correctamente en Supabase.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar userfactory en Supabase: " + ex.Message);
                throw; // Lanza la excepción para propagarla hacia arriba
            }
            
        }

        public async Task InsertarBuyerFactory(UsuarioComprador nuevouser)
        {

            
            try{

                UsuarioCompradorBD nuevo = new UsuarioCompradorBD{
                    Id= nuevouser.Id,
                    Nombre= nuevouser.Nombre,
                    Nick_name= nuevouser.Nick_name,
                    Contraseña= nuevouser.Contraseña,
                    Email= nuevouser.Email,
                    Edad= nuevouser.Edad,
                    Limite_gasto_cents_mes= nuevouser.Limite_gasto_cents_mes
                };
                // Inserta el nuevo producto en la tabla correspondiente
                await _supabaseClient
                        .From<UsuarioCompradorBD>()
                        .Insert(nuevo);
                Console.WriteLine("Compradorfactory insertado correctamente en Supabase.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar buyerfactory en Supabase: " + ex.Message);
                throw; // Lanza la excepción para propagarla hacia arriba
            }
        }
        public async Task InsertarSellerFactory(UsuarioVendedor nuevouser)
        {
            

            // Inserta el nuevo producto en la tabla correspondiente
            await _supabaseClient
                    .From<UsuarioVendedor>()
                    .Insert(nuevouser);
            Console.WriteLine("Vendedorfactory insertado correctamente en Supabase.");
        }




        public async Task<bool> UsuarioCompradorExistePorApodo(string apodo)
        {
        // Realizar una consulta a la tabla de usuarios para verificar si existe un usuario con el apodo dado
            var result = await _supabaseClient
                                .From<UsuarioComprador>()
                                .Where(x => x.Nick_name == apodo)
                                .Get();

            // Si la consulta devuelve algún resultado, significa que el usuario existe
            return result.Models.Any();
        }

        public async Task<Producto> UpdateCantidadProducto(Producto prod,int cant)
        {
            try{
                await _supabaseClient
                        .From<Producto>()
                        .Where(x => x.Id == prod.Id)
                        .Set(x => x.Cantidad, prod.Cantidad - cant)
                        .Update();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error actualizar cantidad de producto en Supabase: " + ex.Message);
                throw; // Lanza la excepción para propagarla hacia arriba
            }

            return prod;
        }

        public async Task InsertarCarrito(CarritoCompra nuevocarrito)
        {
            

            // Inserta el nuevo producto en la tabla correspondiente
            await _supabaseClient
                    .From<CarritoCompra>()
                    .Insert(nuevocarrito);
            Console.WriteLine("Carrito insertado correctamente en Supabase.");
        }

        public async Task InsertarDeseo(Listadeseos nuevodeseo)
        {
            await _supabaseClient
                    .From<Listadeseos>()
                    .Insert(nuevodeseo);
            Console.WriteLine("Deseo insertado correctamente en Supabase.");
        }

        public async Task InsertarGuardado(Guardadoparamastarde nuevoguardado)
        {
            

            // Inserta el nuevo producto en la tabla correspondiente
            await _supabaseClient
                    .From<Guardadoparamastarde>()
                    .Insert(nuevoguardado);
            Console.WriteLine("Guardado insertado correctamente en Supabase.");
        }

        public async Task EliminarCarrito(CarritoCompra nuevocarrito)
        {
            await _supabaseClient
                    .From<CarritoCompra>()
                    .Delete(nuevocarrito);
            Console.WriteLine("Carrito eliminado correctamente en Supabase.");
        }

        public async Task EliminarGuardado(int usuarioId, int productoId)
        {
            try{
                await _supabaseClient
                    .From<Guardadoparamastarde>()
                    .Where(x => x.Id_comprador == usuarioId && x.Id_producto == productoId)
                    .Delete();
            Console.WriteLine("Guardado eliminado correctamente en Supabase.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error borrar producto en guardados en Supabase: " + ex.Message);
                throw; // Lanza la excepción para propagarla hacia arriba
            }
        }
            

        public async Task EliminarDeseo(int usuarioId, int productoId)
        {
            try{
                await _supabaseClient
                    .From<Listadeseos>()
                    .Where(x => x.Id_comprador == usuarioId && x.Id_producto == productoId)
                    .Delete();
            Console.WriteLine("Listadeseos eliminado correctamente en Supabase.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error borrar producto en listadeseos en Supabase: " + ex.Message);
                throw; // Lanza la excepción para propagarla hacia arriba
            }
        }

        
    }
}
