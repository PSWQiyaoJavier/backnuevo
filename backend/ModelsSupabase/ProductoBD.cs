using Postgrest.Attributes;
using Postgrest.Models;
using backend.PatronObservador;

namespace backend.ModelsSupabase
{
    [Table("producto")]
    public class ProductoBD : BaseModel
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


    }
}
