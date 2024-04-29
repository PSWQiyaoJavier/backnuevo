using Postgrest.Attributes;
using Postgrest.Models;
using backend.MetodoFabrica;

namespace backend.Models
{   
   
    public class Pedidopoo : BaseModel
        {
            public List<Producto> Productos { get; set; }
            public DateTime Fecha { get; set; }
            public UsuarioComprador Comprador { get; set; }

            public Pedidopoo(List<Producto> productos, UsuarioComprador comprador)
            {
                Productos = productos;
                Fecha = DateTime.Now;
                Comprador = comprador;
            }
        }
}