using Postgrest.Attributes;
using Postgrest.Models;
using backend.MetodoFabrica;

namespace backend.Models
{   
   
    public class Pedidopoo : BaseModel
        {
            public List<Producto> Productos { get; set; }
            public List<int> Cantidades { get; set; }
            public DateTime Fecha { get; set; }
            public UsuarioComprador Comprador { get; set; }

            public Pedidopoo(List<Producto> productos, List<int> cantidades, UsuarioComprador comprador)
            {
                Productos = productos;
                Cantidades = cantidades;
                Fecha = DateTime.Now;
                Comprador = comprador;
            }
        }
}