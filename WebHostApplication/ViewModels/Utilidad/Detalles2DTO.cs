namespace WebHostApplication.ViewModels.Utilidad
{
    public class Detalles2DTO
    {
        public int NumeroFactura { get; set; }
        public string ClienteNombre { get; set; } = "";
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Costo { get; set; }
        public decimal Utilidad { get; set; }
    }
}
