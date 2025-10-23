using WebHostApplication.ViewModels.Facturas;

namespace WebHostApplication.ViewModels.Invoices
{
    public class FacturaCreateDTO
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdCliente { get; set; }
        public int IdCliente2 { get; set; } = 0;  // Por defecto si no hay otro cliente
        public int IdVendedor { get; set; }
        public int IdCajero { get; set; }
        public decimal? Subtotal { get; set; } = 0;
        public decimal? Dinero { get; set; } = 0;
        public decimal? Servicio1 { get; set; } = 0;
        public decimal? Servicio2 { get; set; } = 0;
        public decimal? Servicio3 { get; set; } = 0;
        public decimal? Subtotal2 { get; set; } = 0;
        public decimal? Subtotal3 { get; set; } = 0;
        public decimal? TotalDescuento { get; set; } = 0;
        public decimal? Cambio { get; set; } = 0;
        public decimal? Efectivo { get; set; } = 0;
        public decimal? Tcredito { get; set; } = 0;
        public decimal? Tdebito { get; set; } = 0;
        public decimal? Credito { get; set; } = 0;
        public decimal? Transferencia { get; set; } = 0;
        public string? TT { get; set; }
        public decimal? Gratis { get; set; } = 0;
        public int? Z { get; set; }
        public string? Fechahora { get; set; }
        public string? Mesa { get; set; }
        public string? Pedido { get; set; }
        public string? Devol { get; set; }
        public TimeSpan? Hora { get; set; }
        public string? Observacion { get; set; }
        public int Electronica { get; set; } = 0;
        public string? Maq { get; set; }
        public int? Z2 { get; set; }
        public List<DetalleCreateDTO> Detalles { get; set; } = new();
    }
}
