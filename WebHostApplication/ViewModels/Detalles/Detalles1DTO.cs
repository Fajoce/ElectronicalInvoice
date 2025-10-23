using WebHostApplication.Models;
using WebHostApplication.Models.Vendedores;

namespace WebHostApplication.ViewModels.Detalles
{
    public class Detalles1DTO
    {
      public int Ind { get; set; }
      public int? NumeroFactura { get; set; }
      public string? CodigoFijo { get; set; }
      public string? Referencia { get; set; }
      public string? CodigoBarras { get; set; }
      public string? Descripcion { get; set; }
      public decimal? Precio { get; set; }
      public decimal? Can { get; set; } 
      public decimal Costo { get; set; }
      public DateTime fecha { get; set; }
      public int IdVendedor2 { get; set; }
      public Vendedor2 Vendedor2 { get; set; }
      public Detalle2 Factura { get; set; }
    }
}
