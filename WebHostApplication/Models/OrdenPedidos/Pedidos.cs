using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Clientes;
using WebHostApplication.Models.DetallesPedido;

namespace WebHostApplication.Models.OrdenPedidos
{
    [Table("pedidos")]
    public class Pedidos
    {
        [Key]
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Clientes { get; set; }
        public int IdCliente2 { get; set; }
        public int IdCajero { get; set; }

        [ForeignKey("IdCajero")]
        public Cajero Cajero { get; set; }
        public int? IdVendedor { get; set; }
        [ForeignKey("IdVendedor")]
        public Vendedor Vendedores { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Subtotal { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? Efectivo { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Dinero { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Tcredito { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Tdebito { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Credito { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Transferencia { get; set; }
        [NotMapped]
        public int IdVendedor2 { get; set; }
        public bool Estado { get; set; }
        //public decimal? Subtotal { get; set; }
        //public decimal? Total { get; set; }

        public ICollection<PedidosDetalles> Detalles { get; set; } = new List<PedidosDetalles>();
    }
}
