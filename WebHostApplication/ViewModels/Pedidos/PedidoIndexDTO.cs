using WebHostApplication.Models.Pedidos;

namespace WebHostApplication.ViewModels.Pedidos
{
    public class PedidoIndexDTO
    {
            public Pedido Pedido { get; set; } = null!;
            public decimal TotalCalculado { get; set; }
       
    }
}
