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

        IList<Producto> ObtenerProductosPorNombre(string keyWords);

        IList<Producto> GetProductByChart(CarritoCompra carr);
        //IList<Producto> ObtenerProductosPorNombre(string nombre);
        Task AddFabrica(string nombre, string nick_name, string contraseña, string email, int edad, int limite);

        IList<CarritoCompra> GetChartByUserBuyer(UsuarioComprador user);

        Task InicializarDatosDesdeBD();
        void AgregarProductoAGuardados(int idComprador, int idProducto);

        void AgregarProductoACarrito(int idComprador, int idProducto, int cantidades);        
        void AgregarProductoADeseos(int idComprador, int idProducto);
        void RealizarPedido(int idComprador,int numero, string uno, string dos, string metodopago, int total);
        IList<Guardadoparamastarde> GetGuardadosByUserBuyer(UsuarioComprador user);
        List<Producto> PooGuardados(int userid);
        List<(Producto,int)> PooCarrito(int userid);
        List<Producto> PooDeseos(int userid);
        void EliminarProductoCarrito(int idComprador, int idProducto);
        void EliminarProductoGuardado(int idComprador, int idProducto);
        void EliminarProductoListaDeseos(int idComprador, int idProducto);
        UsuarioComprador LoginComprador2(String nick, String password);
        IList<Producto> ObtenerProductos();

    }
}