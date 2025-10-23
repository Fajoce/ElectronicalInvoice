using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models
{
    [Table("productos")]
    public class Productos
    {
        [Key]
        [Column("id")]
        [StringLength(30)]
        public string Id { get; set; } = string.Empty;

        [Column("descripcion")]
        [StringLength(60)]
        public string? Descripcion { get; set; }

        [Column("precio1", TypeName = "decimal(9,2)")]
        public decimal Precio1 { get; set; }

        [Column("precio2", TypeName = "decimal(9,2)")]
        public decimal Precio2 { get; set; }

        [Column("precio3", TypeName = "decimal(9,2)")]
        public decimal? Precio3 { get; set; }

        [Column("nfrac", TypeName = "decimal(9,2)")]
        public decimal? Nfrac { get; set; }

        [Column("existencia", TypeName = "decimal(9,2)")]
        public decimal? Existencia { get; set; }

        [Column("grupo")]
        [StringLength(30)]
        public string? Grupo { get; set; }

        [Column("iva", TypeName = "decimal(9,2)")]
        public decimal? Iva { get; set; }

        [Column("costo", TypeName = "decimal(9,2)")]
        public decimal? Costo { get; set; }

        [Column("max")]
        [StringLength(10)]
        public string? Max { get; set; }

        [Column("min")]
        [StringLength(10)]
        public string? Min { get; set; }

        [Column("Uni")]
        [StringLength(3)]
        public string? Uni { get; set; }

        [Column("codigo2")]
        [StringLength(15)]
        public string? Codigo2 { get; set; }

        [Column("rf", TypeName = "text")]
        public string? Rf { get; set; }
        //public virtual ICollection<Detalle1> Facturas2 { get; set; }
    }
}
