namespace WebHostApplication.ViewModels.OrdenPedidos
{
    public class CreatePedidosDTO
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdCliente { get; set; }
        public int IdCliente2 { get; set; }
        public int IdVendedor { get; set; }
        public int IdCajero { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Efectivo { get; set; }
        public decimal Dinero { get; set; }
        public decimal Tdebito { get; set; }
        public decimal Tcredito { get; set; }
        public decimal Transferencia { get; set; }
        public decimal Credito { get; set; }
        public List<PedidosDetalleDTO> Detalles { get; set; } = new List<PedidosDetalleDTO>();
    }
}
