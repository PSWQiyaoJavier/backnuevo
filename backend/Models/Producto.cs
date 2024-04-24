using Postgrest.Attributes;
using Postgrest.Models;
using backend.PatronObservador;

namespace backend.Models
{
    [Table("producto")]
    public class Producto : BaseModel, ISujetoObservado
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }


        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("categoria")]
        public string Categoria { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("precio")]
        public float Precio { get; set; }


        private List<IObservador> observadores = new List<IObservador>();

        public void AgregarObservador(IObservador observador)
        {
            observadores.Add(observador);
        }

        public void EliminarObservador(IObservador observador)
        {
            observadores.Remove(observador);
        }

        public void NotificarObservadores()
        {
            foreach (var observador in observadores)
            {
                observador.Actualizar();
            }
        }

    }
}
