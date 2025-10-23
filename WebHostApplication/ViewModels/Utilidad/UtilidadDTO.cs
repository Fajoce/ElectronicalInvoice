namespace WebHostApplication.ViewModels.Utilidad
{
    public class UtilidadDTO
    {
        public int NumeroFactura { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal Utilidad { get; set; }
    }
}
