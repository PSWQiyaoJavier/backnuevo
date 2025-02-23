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
using backend.ModelsSupabase;

namespace backend.FachadaBD
{
    public interface InterfazFachadaBD
    {
        int PedidoBD(int idComprador);
        int GenerarIdUnico();
        void ActualizarUnidadesBD(Pedidopoo ped);
        IList<Producto> ObtenerProductos();
        IList<CarritoCompra> ObtenerChart();
        IList<Guardadoparamastarde> ObtenerGuardados();
        IList<Listadeseos> ObtenerDeseos();
        IList<UsuarioFabrica> ObtenerUsuarios();
        IList<UsuarioComprador> ObtenerCompradores();
        Task AddFactoryMember(UsuarioComprador nuevouser);
        void PedidoProductoBD(int idComprador, int random1, List<UsuarioComprador> _compradores);
        UsuarioComprador ObtenerUsuarioCompradorPorNick(string nick);
        Producto ObtenerProductoPorPrecio(int nick);
        Producto ObtenerProductoPorId(int id);
        void AgregarAlCarrito(int usuarioId, int productoId, int cantidad);
        void AgregarADeseos(int usuarioId, int productoId);
        void AgregarAGuardados(int usuarioId, int productoId);
        void EliminarAlCarrito(int usuarioId, int productoId);
        void EliminarAlGuardado(int usuarioId, int productoId);
        void EliminarAlDeseo(int usuarioId, int productoId);
        void InsertarPedidoproducto(PedidoProductoBD nuevoElemento);

    }

}