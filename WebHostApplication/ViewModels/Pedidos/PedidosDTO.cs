using WebHostApplication.Models.DetallesPedido;

namespace WebHostApplication.ViewModels.Pedidos
{
    public class PedidosDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; }
        public List<DetallesDTO> Detalles { get; set; } = new List<DetallesDTO>();
    }
}
