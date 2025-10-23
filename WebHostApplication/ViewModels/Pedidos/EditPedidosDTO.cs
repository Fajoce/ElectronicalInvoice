namespace WebHostApplication.ViewModels.Pedidos
{
    public class EditPedidosDTO
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; }
        public int IdDetalle { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public List<DetallesDTO> Detalles { get; set; } = new List<DetallesDTO>();
    }
}
