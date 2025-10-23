using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.ViewModels.Facturas
{
    public class DetalleCreateDTO
    {
        public string IdProducto { get; set; }
        public string CodigoFijo { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal? Precio { get; set; }
        public decimal? Cantidad { get; set; }       
       
        public decimal? Costo { get; set; } 
        public decimal? Subtotal { get; set; } = 0;
        public int IdVendedor2 { get; set; } = 0;
        public decimal Exis { get; set; } = 0;
        public string U { get; set; } = string.Empty;
    }
}
