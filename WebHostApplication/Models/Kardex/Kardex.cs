using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.Models
{

        [Table("kardex")]
        public class Kardex
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Display(Name = "Fecha")]
            public DateTime Fecha { get; set; }

            [Display(Name = "Tipo de Movimiento")]
            [StringLength(20)]
            public string TipoMovimiento { get; set; } = string.Empty;

            [Display(Name = "Producto ID")]
            [StringLength(50)]
            public string ProductoId { get; set; } = string.Empty;

            [StringLength(255)]
            public string Descripcion { get; set; } = string.Empty;

            [Column(TypeName = "decimal(10,2)")]
            public decimal Cantidad { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            [Display(Name = "Precio Venta")]
            public decimal PrecioVenta { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            [Display(Name = "Precio Costo")]
            public decimal PrecioCosto { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            public decimal Utilidad { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            [Display(Name = "Existencia Antes")]
            public decimal ExistenciaAntes { get; set; }

            [Column(TypeName = "decimal(10,2)")]
            [Display(Name = "Existencia Después")]
            public decimal ExistenciaDespues { get; set; }

            [Display(Name = "Referencia Documento")]
            [StringLength(50)]
            public string ReferenciaDocumento { get; set; } = string.Empty;

            [StringLength(100)]
            public string Usuario { get; set; } = string.Empty;

            public string? Observacion { get; set; }
        }
    }

