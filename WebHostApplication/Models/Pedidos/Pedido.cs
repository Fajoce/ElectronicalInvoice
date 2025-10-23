using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Clientes;
using WebHostApplication.Models.DetallesPedido;

namespace WebHostApplication.Models.Pedidos
{
    [Table("Pedido")]
    public class Pedido
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Cliente es obligatorio")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El Vendedor es obligatorio")]
        public int IdVendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
        [ForeignKey("IdVendedor")]
        public Vendedor Vendedor { get; set; }

        // Inicializar siempre la lista
        public List<PedidoDetalles> Detalles { get; set; } = new List<PedidoDetalles>();
    }
}
