using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Pedidos;

namespace WebHostApplication.Models.OrdenPedidos
{
    [Table("pedido_detalles")]
    public class PedidosDetalles
    {
        [Key]
        public int IdDetalle { get; set; }
        public int IdPedido { get; set; }
        [ForeignKey(nameof(IdPedido))]
        public Pedidos Pedido { get; set; }
        public string IdProducto { get; set; }
        public string CodigoFijo { get; set; }
        public string Referencia { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        [NotMapped]
        public decimal Existencia { get; set; }
        public decimal? Costo { get; set; }
        [NotMapped]
        public int IdVendedor2 { get; set; }
    }
}
