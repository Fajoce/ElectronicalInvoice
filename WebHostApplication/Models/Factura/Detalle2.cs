using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Clientes;
using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models
{
    public class Detalle2
    {
        [Key]
        public int NumeroFactura { get; set; }
        public DateTime? Fecha { get; set; }

        // Relaciones foráneas
        public int IdVendedor { get; set; }
        [ForeignKey("IdVendedor")]
        public Vendedor Vendedor { get; set; }

        public int? IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        public int? IdCliente2 { get; set; }
        [ForeignKey("IdCliente2")]
        public Cliente2 Cliente2 { get; set; }

        public int IdCajero { get; set; }
        [ForeignKey("IdCajero")]
        public Cajero Cajero { get; set; }

        // Campos monetarios
        public decimal? Subtotal { get; set; }
        public decimal? Dinero { get; set; }
        public decimal? TotalIva { get; set; }
        public decimal? TotalDescuento { get; set; }
        public decimal? Subtotal2 { get; set; }
        public decimal? Servicio1 { get; set; }
        public decimal? Servicio2 { get; set; }
        public decimal? Servicio3 { get; set; }
        public decimal? Subtotal3 { get; set; }
        public decimal? Cambio { get; set; }
        public decimal? Efectivo { get; set; }
        public decimal? Tcredito { get; set; }
        public decimal? Tdebito { get; set; }
        public decimal? Credito { get; set; }
        public decimal? Transferencia { get; set; }

        // Otros datos
        public string? TT { get; set; }
        public int? Z { get; set; }
        public int? Nfact2 { get; set; }
        public string? FechaHora { get; set; }
        public string? Mesa { get; set; }
        public string? Pedido { get; set; }
        public string? Devol { get; set; }
        public decimal? V1 { get; set; }
        public decimal? Gratis { get; set; }
        public TimeSpan? Hora { get; set; }
        public string? Observacion { get; set; }
        public decimal? Retencion { get; set; }
        public int? NFpos { get; set; }
        public string? NFelectronica { get; set; }
        public string? NoRemision { get; set; }
        public string? NoCotizacion { get; set; }
        public string? NoTraslado { get; set; }
        public string? NoSepare { get; set; }
        public bool Valida { get; set; }
        public string? Cufe { get; set; }
        public string? Qr { get; set; }
        public string Response { get; set; }
        public int Electronica { get; set; }
        public string? Maq { get; set; }
        public string? JsonBody { get; set; }
        public string? Error { get; set; }
        public int? TipoDoc { get; set; }
        public int IntentosNC { get; set; } = 0;
        public int? Z2 { get; set; }
        public DateTime? Fecha_Electronica { get; set; }
        public bool? ValidaNC { get; set; }
        public string? NCelectronica { get; set; }
        public string? CufeNC { get; set; }
        public string? QrNC { get; set; }
        public string? ResponseNC { get; set; }
        public string? JsonBodyNC { get; set; }
       public string? ErrorNC { get; set; }
        public DateTime? Fecha_NC_Electronica { get; set; }
        public string? NoPrecuenta { get; set; }

        // Relación uno-a-muchos con detalle1
        public ICollection<Detalle1> Detalles { get; set; }
    }
}
