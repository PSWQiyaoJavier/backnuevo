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

        [Column("foto1")]
        public string Foto1 { get; set; }

        [Column("foto2")]
        public string Foto2 { get; set; }

        [Column("foto3")]
        public string Foto3 { get; set; }

    
        private List<IObservador> observadores = new List<IObservador>();

        public void AgregarObservador(IObservador observador)
        {
            observadores.Add(observador);
        }

        public void EliminarObservador(IObservador observador)
        {
            IObservador observadorAEliminar = observadores.FirstOrDefault(o => o.GetId() == observador.GetId());
            if (observadorAEliminar != null)
            {
                observadores.Remove(observadorAEliminar);
            }
        }

        public void NotificarObservadores()
        {
            foreach (var observador in observadores)
            {
                observador.Actualizar();
            }
        }

        public void CambiarUnidades(int unidadesCompradas)
        {
            // Actualizar el número de unidades
            this.Cantidad = Cantidad - unidadesCompradas;

            // Notificar a los observadores (compradores) sobre el cambio de unidades
            NotificarObservadores();
        }

    }
}
