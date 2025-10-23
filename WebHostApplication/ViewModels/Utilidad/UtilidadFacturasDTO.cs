namespace WebHostApplication.ViewModels.Utilidad
{
    public class UtilidadFacturasDTO
    {
       public int NumeroFactura { get; set; }
       public DateTime Fecha { get; set; }
       public string ClienteCompleto { get; set; } = string.Empty;
       public decimal TotalSubtotal { get; set; }
       public decimal TotalCosto { get; set; }
       public decimal TotalUtilidad { get; set; }
        
    }
}
