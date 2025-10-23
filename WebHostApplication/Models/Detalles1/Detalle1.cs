using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Detalles2;
using WebHostApplication.Models.Vendedores;

namespace WebHostApplication.Models
{
    [Table("detalle1")]
    public class Detalle1
    {
        [Key]
        public int Ind { get; set; }

        // Clave foránea explícita
        [Column("numeroFactura")]
        public int NumeroFactura { get; set; }

        // Relación hacia Detalle2
        [ForeignKey("NumeroFactura")]  // 👈 aquí sí corresponde
        public  Detalle2 Detalle2 { get; set; }

        public string CodigoFijo { get; set; }
        public string Referencia { get; set; }
        public string CodigoBarras { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Can { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Precio2 { get; set; }
        public decimal? Precio3 { get; set; }
        public decimal? Precio4 { get; set; }
        public string U { get; set; }
        public string Grupo { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? TotalDescuento { get; set; }
        public decimal? Subtotal1 { get; set; }
        public decimal? Impuesto { get; set; }
        public decimal? BaseGravable { get; set; }
        public decimal? Ival { get; set; }
        public int? Z { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Utilidad { get; set; }
        public decimal? Exis { get; set; }
        public decimal? Unidx { get; set; }
        public decimal? Unidtotal { get; set; }
        public int? IdImpuesto { get; set; }

        // FK vendedor2 (aún sin navegación)
      
        public int IdVendedor2 { get; set; }

        // Navegación hacia Vendedor2
        [ForeignKey("IdVendedor2")]
        public Vendedor2 Vendedor2 { get; set; }

    }
}
