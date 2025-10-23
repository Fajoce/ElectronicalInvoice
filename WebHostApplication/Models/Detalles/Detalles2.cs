using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Clientes;

namespace WebHostApplication.Models.Detalles
{
    
    public class Detalles2
    {
     [Key]
     [Column("numeroFactura")]
     public int NumeroFactura { get; set; }

            public DateTime? Fecha { get; set; }

            public int IdVendedor { get; set; }
            public int IdCliente { get; set; }
            public int IdCajero { get; set; }
            public int? IdCliente2 { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Subtotal { get; set; }

            [Column(TypeName = "decimal(15,2)")]
            public decimal? Dinero { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? TotalIva { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? TotalDescuento { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Subtotal2 { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Servicio1 { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Servicio2 { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Servicio3 { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Subtotal3 { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Cambio { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Efectivo { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Tcredito { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Tdebito { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Credito { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Transferencia { get; set; }

            public string? TT { get; set; }
            public int? Z { get; set; }
            public int? Nfact2 { get; set; }
            public string? Fechahora { get; set; }
            public string? Mesa { get; set; }
            public string? Pedido { get; set; }
            public string? Devol { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? V1 { get; set; }

            [Column(TypeName = "decimal(9,2)")]
            public decimal? Gratis { get; set; }

            public TimeSpan? Hora { get; set; }
            public string? Observacion { get; set; }

            [Column(TypeName = "decimal(9,2)")]
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
            public string? Response { get; set; }

            public int Electronica { get; set; }
            public string? Maq { get; set; }
            public string? JsonBody { get; set; }
            public string? Error { get; set; }

            public int TipoDoc { get; set; }
            public int Intentos { get; set; }
            public int? Z2 { get; set; }

            public DateTime Fecha_Electronica { get; set; }
            public bool ValidaNC { get; set; }
            public string? NCelectronica { get; set; }
            public string? CufeNC { get; set; }
            public string? QrNC { get; set; }
            public string? ResponseNC { get; set; }
            public string? JsonBodyNC { get; set; }
            public int IntentosNC { get; set; }
            public string? ErrorNC { get; set; }
            public DateTime? Fecha_NC_Electronica { get; set; }
            public string? NoPrecuenta { get; set; }
        [ForeignKey("Idcajero")]
        public int? Idcajero { get; set; }

        // Propiedades de navegación
        public virtual Vendedor Vendedor { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual Cliente2? Cliente2 { get; set; }
        public virtual Cajero? Cajero { get; set; } // <-- Asegúrate de que sea virtual
        public virtual ICollection<Detalles1> Detalles { get; set; }
    }
}
