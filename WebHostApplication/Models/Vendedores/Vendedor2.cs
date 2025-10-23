using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models.Vendedores
{
    [Table("vendedor2")]
    public class Vendedor2
    {
        [Key]
        [Column("idvendedor")]
        public int IdVendedor { get; set; }

        [Column("id")]
        [StringLength(30)]
        public string? Id { get; set; }

        [Column("nombre")]
        [StringLength(30)]
        public string? Nombre { get; set; }

        [Column("apellido1")]
        [StringLength(30)]
        public string? Apellido1 { get; set; }

        [Column("apellido2")]
        [StringLength(30)]
        public string? Apellido2 { get; set; }

        [Column("dir")]
        [StringLength(30)]
        public string? Dir { get; set; }

        [Column("tel")]
        [StringLength(30)]
        public string? Tel { get; set; }

        [Column("creditot", TypeName = "decimal(9,2)")]
        public decimal? CreditoT { get; set; }

        [Column("abonot", TypeName = "decimal(9,2)")]
        public decimal? AbonoT { get; set; }

        [Column("condicion")]
        public int? Condicion { get; set; }

        [Column("comision", TypeName = "decimal(9,2)")]
        public decimal? Comision { get; set; }

        [Column("porcentaje", TypeName = "decimal(9,2)")]
        public decimal? Porcentaje { get; set; }

        // 🔹 Relación con Detalle1 (uno a muchos)
        public virtual ICollection<Detalle1> Ventas { get; set; } = new List<Detalle1>();
    }
}
