namespace WebHostApplication.ViewModels.Facturas
{
    public class FacturaDetalleDTO
    {
       // public int IdProducto { get; set; }       // ⚡ clave del producto
        public string Codigo { get; set; } = "";
        public string Referencia { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public decimal? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
    }
}
