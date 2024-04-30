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
using backend.Services;
using backend.Models;
using backend.MetodoFabrica;

namespace backend.Logica
{
    public interface InterfazLogica
    {
        IList<Producto> ObtenerProductos();
        IList<UsuarioFabrica> ObtenerUsuarios();
        Boolean Bool1(string nick);
        IList<Producto> ObtenerProductosPorNombre(string keyWords);

        Task Login(String nick, String password);
        Usuario UserLogged();
        Usuario ObtenerUsuarioPorNick(string nick);
        Producto ObtenerProductoPorPrecio(int nick);

        Usuario ObtenerUsuarioPorEdad(int edad);
        Usuario UpdateEdadUsuario(Usuario usuario,int edad);
        void AgregarAlCarrito(int usuarioId, int productoId);

        IList<CarritoCompra> GetChartByUser(Usuario user);
        IList<CarritoCompra> ObtenerChart();

        IList<Producto> GetProductByChart(CarritoCompra carr);
        //IList<Producto> ObtenerProductosPorNombre(string nombre);
        void Logout();
        Task AddFabrica(string nombre, string nick_name, string contraseña, string email, int edad, int limite);
        Task LoginComprador(String nick, String password);
        IList<CarritoCompra> GetChartByUserBuyer(UsuarioComprador user);
        UsuarioComprador ObtenerUsuarioCompradorPorNick(string nick);
        IList<UsuarioComprador> ObtenerCompradores();
        Task InicializarDatosDesdeBD();
        void AgregarProductoAGuardados(int idComprador, int idProducto);
        void ActualizarUnidades(int idProducto, int uni);
        void AgregarProductoACarrito(int idComprador, int idProducto);
        void AgregarProductoADeseos(int idComprador, int idProducto);
        void RealizarPedido(int idComprador);
        IList<Guardadoparamastarde> GetGuardadosByUserBuyer(UsuarioComprador user);
        List<Producto> PooGuardados(int userid);
        List<Producto> PooCarrito(int userid);
        List<Producto> PooDeseos(int userid);
        void EliminarAlCarrito(int usuarioId, int productoId);
        void EliminarAlGuardado(int usuarioId, int productoId);
        void EliminarAlDeseo(int usuarioId, int productoId);
        void EliminarProductoCarrito(int idComprador, int idProducto);
        void EliminarProductoGuardado(int idComprador, int idProducto);
        void EliminarProductoDeseo(int idComprador, int idProducto);
        UsuarioComprador LoginComprador2(String nick, String password);

    }
}