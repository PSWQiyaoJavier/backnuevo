using backend.Services;
using backend.Models;
using System.Collections.Generic;
using backend.MetodoFabrica;
using backend.Logica;

namespace backend.DatosEnMemoria
{
    public class DatosEnMemoria : IDatosEnMemoria
    {

        private readonly Interfaz interf;
        public List<Producto> _productos { get; set; }
        public List<UsuarioComprador> _compradores { get; set; }
        public DatosEnMemoria(Interfaz interf)
        {
            this.interf = interf;
        }


        public IList<Producto> ObtenerProductos()
        {
            var productosTask = interf.GetAllProducts(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Producto> productos1 = productosTask.Result;
            return productos1;
        }

        public IList<UsuarioComprador> ObtenerCompradores()
        {
            var productosTask = interf.GetAllBuyers(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<UsuarioComprador> productos1 = productosTask.Result;
            return productos1;
        }

        public DatosEnMemoria()
        {
            // Obtener los datos de productos y compradores desde la base de datos
            var datosProductos = ObtenerProductos();
            var datosCompradores = ObtenerCompradores();

            // Convertir los datos en objetos de tu dominio
            foreach (var datosProducto in datosProductos)
            {
                Producto producto = new Producto
                {
                    Id = datosProducto.Id,
                    Nombre = datosProducto.Nombre,
                    Categoria = datosProducto.Categoria,
                    Descripcion = datosProducto.Descripcion,
                    Cantidad = datosProducto.Cantidad,
                    Precio = datosProducto.Precio
                    // Puedes agregar más propiedades si es necesario
                };

                _productos.Add(producto);
            }

            foreach (var datosComprador in datosCompradores)
            {
                UsuarioComprador comprador = new UsuarioComprador
                {
                    Id = datosComprador.Id,
                    Nombre = datosComprador.Nombre
                    // Puedes agregar más propiedades si es necesario
                };

                _compradores.Add(comprador);
            }
        }
    }
}