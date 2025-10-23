namespace WebHostApplication.ViewModels.Pedidos
{
    public class PedidosListDTO
    {
        public int IdPedido { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Vendedor { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public int CantidadDetalles { get; set; }
    }
}
