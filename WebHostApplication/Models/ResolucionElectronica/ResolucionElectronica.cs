using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.Models
{
    [Table("resoluciones_electronica")]
    public class ResolucionElectronica
    {
        [Key]
        public int Id { get; set; }
        [Column("tipo")]
        public int? Tipo { get; set; }   // 1 = POS, 2 = Electrónica

        [Column("idDian")]
        public int? IdDian { get; set; }

        [Column("inicio")]
        public int? Inicio { get; set; }

        [Column("final")]
        public int? Final { get; set; }

        [Column("consecutivo")]
        public int? Consecutivo { get; set; }

        [Column("estado")]
        [StringLength(2)]
        public string? Estado { get; set; } = "A";

        [Column("fechaFinal", TypeName = "date")]
        public DateTime? FechaFinal { get; set; }

        [Column("prefijo")]
        [StringLength(10)]
        public string? Prefijo { get; set; }

        [Column("resolucion")]
        [StringLength(20)]
        public string? Resolucion { get; set; }

        [Column("fecha", TypeName = "date")]
        public DateTime? Fecha { get; set; }

        [Column("idResolDian")]
        public int? IdResolDian { get; set; }
    }
}

