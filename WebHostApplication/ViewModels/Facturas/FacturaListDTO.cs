namespace WebHostApplication.ViewModels.Facturas
{
    public class FacturaListDTO
    {
        public int IdFactura { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Cajero { get; set; } = string.Empty;
        public string Vendedor { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int CantidadDetalles { get; set; }
        public decimal? Total { get; set; }
    }
}
