using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Supabase;
using Supabase.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using backend.Models;
using Postgrest.Models;
using backend.MetodoFabrica;
using backend.ModelsSupabase;

namespace backend.Services
{
    public interface Interfaz
    {
        Task InsertarProducto(Producto nuevoProducto);
        Task<List<Producto>> GetProductsById(int y);
        Task EliminarProducto(Producto producto);
        Task<List<Producto>> GetAllProducts();
        Task<List<UsuarioFabrica>> GetAllUsers();
        Task<Usuario> UserByNick(string filtro);
        Task InsertarUser(Usuario nuevouser);
        Task<bool> UsuarioExistePorApodo(string apodo);
        Task<Usuario> UpdateAgeUser(Usuario usuario,int edad1 ,int edad);
        Task<Usuario> UserByAge(int filtro);
        Task InsertarCarrito(CarritoCompra nuevocarrito);
        //Task<Comprador> BuyerByNick(string filtro);
        Task<Producto> ProductByPrice(int filtro);
        Task InsertarBuyer(Comprador nuevobuyer);
        Task<List<CarritoCompra>> GetChart();
        Task Insert1<Comprador>(Comprador item) where Comprador : Usuario,new();
        Task InsertarUserFactory(UsuarioFabrica nuevouser);
        Task InsertarBuyerFactory(UsuarioComprador nuevouser);
        Task InsertarSellerFactory(UsuarioVendedor nuevouser);
        Task<bool> UsuarioCompradorExistePorApodo(string apodo);
        Task<UsuarioComprador> UserBuyerByNick(string filtro);
        Task<List<UsuarioComprador>> GetAllBuyers();
        Task<Producto> ProductById(int id);
        Task<List<Guardadoparamastarde>> GetGuardados();
        Task<List<Listadeseos>> GetDeseos();
        Task InsertarDeseo(Listadeseos nuevodeseo);
        Task InsertarGuardado(Guardadoparamastarde nuevoguardado);
        Task EliminarCarrito(CarritoCompra nuevocarrito);
        Task EliminarGuardado(int usuarioId, int productoId);
        Task EliminarDeseo(int usuarioId, int productoId);
        Task InsertarPedido(PedidopooBD nuevopedido);
        Task<PedidopooBD> PedidoByRandom(int filtro);
        Task InsertarPedidoproducto(PedidoProductoBD nuevopedido);
        Task<Producto> UpdateCantidadProducto(Producto prod,int cant);
    }
}