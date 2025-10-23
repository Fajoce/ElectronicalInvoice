namespace WebHostApplication.ViewModels.Pedidos
{
    public class DetallesDTO
    {
        public int IdDetalle { get; set; }
        public string ProductoId { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
