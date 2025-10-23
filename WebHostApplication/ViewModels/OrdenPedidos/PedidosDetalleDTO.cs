using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.ViewModels.OrdenPedidos
{
    public class PedidosDetalleDTO
    {
        public string IdProducto { get; set; } = string.Empty;
        public string CodigoFijo { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
        public decimal? Cantidad { get; set; }
        [Column("subtotal")]
        public decimal? Subtotal { get; set; }
        public decimal? Costo { get; set; }
        [NotMapped]
        public decimal? Existencia { get; set; }
        public int? IdVendedor2 { get; set; }
    }
}
