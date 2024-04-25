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
        Task GuardarProductoParaMasTarde(string nickcomprador, int idProducto);
        void AgregarProductoAGuardados(int idComprador, int idProducto);


    }
}