using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Detalles2;
using WebHostApplication.Models.DetallesPedido;
//using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models.Clientes
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("idcliente")]
        public int IdCliente { get; set; }

        [StringLength(30)]
        [Column("id")]
        public string? Id { get; set; }

        [StringLength(30)]
        [Column("nombre")]
        public string? Nombre { get; set; }

        [StringLength(30)]
        [Column("apellido1")]
        public string? Apellido1 { get; set; }

        [StringLength(30)]
        [Column("apellido2")]
        public string? Apellido2 { get; set; }

        [StringLength(30)]
        [Column("dir")]
        public string? Dir { get; set; }

        [StringLength(30)]
        [Column("tel")]
        public string? Tel { get; set; }

        [StringLength(50)]
        [Column("correo")]
        public string? Correo { get; set; }

        [StringLength(50)]
        [Column("tipo")]
        public string? Tipo { get; set; }

        [Column("porcentaje", TypeName = "decimal(9,2)")]
        public decimal? Porcentaje { get; set; }

        [Column("condicion")]
        public int? Condicion { get; set; }

        [Column("doc_id")]
        public int DocId { get; set; } = 3;  // valor por defecto en la BD

        [Column("tipotercero")]
        public int TipoTercero { get; set; } = 2; // valor por defecto en la BD

        [Column("MAX", TypeName = "decimal(9,2)")]
        public decimal? Maximo { get; set; }
        public ICollection<Detalle2> Factura { get; set; } = new List<Detalle2>();
        //public List<PedidoDetalles> PedidoDetalles { get; set; } = new();

    }
}
