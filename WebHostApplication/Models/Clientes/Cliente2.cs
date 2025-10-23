using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Detalles2;
//using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models.Clientes
{
    [Table("clientes2")]
    public class Cliente2
    {
        [Key]
        [Column("idcliente2")]
        public int IdCliente2 { get; set; }

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

        [Column("correo")]
        [StringLength(50)]
        public string? Correo { get; set; }

        [Column("tipo")]
        [StringLength(50)]
        public string? Tipo { get; set; }

        [Column("porcentaje", TypeName = "decimal(9,2)")]
        public decimal? Porcentaje { get; set; }

        [Column("condicion")]
        public int? Condicion { get; set; }
        public ICollection<Detalle2> Facturas { get; set; } = new List<Detalle2>();

    }
}
