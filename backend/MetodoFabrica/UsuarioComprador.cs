using Postgrest.Attributes;
using Postgrest.Models;
using backend.PatronObservador;
using backend.Models;

namespace backend.MetodoFabrica
{
    [Table("comprador")]
    public class UsuarioComprador : UsuarioFabrica, IObservador
    {


        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }

        public List<Producto> Carritolista { get; set; }

        public List<Producto> Guardadoslista { get; set;}

        public UsuarioComprador(string nombre, string nick_name, string contraseña, string email, int edad, int limite)
            : base(nombre, nick_name, contraseña, email, edad)
        {
            Limite_gasto_cents_mes = limite;
        }

        public UsuarioComprador()
        {
            // Puedes inicializar propiedades comunes aquí si es necesario
        }

        public void AgregarProductoGuardado(Producto producto)
        {
            Guardadoslista.Add(producto);
        }

        public void Actualizar()
        {
            // Lógica para manejar la actualización del Producto
            Console.WriteLine("El producto ha sido actualizado. Deberías tomar alguna acción.");
        }
    }
}