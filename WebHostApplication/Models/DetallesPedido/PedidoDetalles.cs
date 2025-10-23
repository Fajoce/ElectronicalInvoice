using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.Models.DetallesPedido
{
    [Table("pedidodetalle")]
    public class PedidoDetalles
    {
        [Key]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public Productos Producto { get; set; }
    }
}
