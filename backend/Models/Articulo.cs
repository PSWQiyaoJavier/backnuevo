using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("articulo")]
    public class Articulo : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("categoria")]
        public string Categoria { get; set; }

        [Column("edad_min")]
        public int Edad_min { get; set; }

        [Column("consejos_utilizacion")]
        public string Consejos_utilizacion { get; set; }

        [Column("consejos_retirada")]
        public string Consejos_retirada { get; set; }

        [Column("origen")]
        public string Origen { get; set; }

        [Column("proceso_produccion")]
        public string Proceso_produccion { get; set; }

        [Column("impacto_ambiental_social")]
        public string Impacto_ambiental_social { get; set; }

        [Column("contribucion_ods")]
        public string Contribucion_ods { get; set; }

        [Column("id_usuario")]
        public int Id_usuario { get; set; }

    }
}