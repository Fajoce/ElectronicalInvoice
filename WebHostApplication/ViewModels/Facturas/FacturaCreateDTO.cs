using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.ViewModels.Facturas
{
    public class FacturaCreateDTO
    {
        public int IdCliente { get; set; }
        public int IdCliente2 { get; set; }
        public int IdCajero { get; set; }
        public int IdVendedor { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal? Subtotal { get; set; }
        public decimal? Subtotal2 { get; set; }
        public decimal? Subtotal3 { get; set; }
        public decimal? Dinero { get; set; }
        public decimal? Servicio1 { get; set; }
        public decimal? Servicio2 { get; set; }
        public decimal? Servicio3 { get; set; }
        public decimal? Credito { get; set; }
        public decimal? Efectivo { get; set; }
        public int? Electronica { get; set; }
        public decimal? Tdebito { get; set; }
        public decimal?  Tcredito { get; set; }
        public decimal?  Transferencia { get; set; }
        public string? FormaPago { get; set; }
        public decimal? MontoPago { get; set; }
        [NotMapped]
        public int Pedido { get; set; }
        // 🔹 Inicialización
        public List<DetalleCreateDTO> Detalles { get; set; } = new List<DetalleCreateDTO>();
    }
}
