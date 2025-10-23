using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Clientes;
using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models.Detalles2
{
    [Table("detalle2")]
    public class Detalle2
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

        private decimal? _efectivo;
        [Column(TypeName = "decimal(9,2)")]
        public decimal? Efectivo {
            get => _efectivo;
            set
            {
                _efectivo = value;
                if (value.HasValue && value.Value > 0)
                {
                    Dinero = value; // 👈 se actualiza automáticamente
                }
            }
        }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Tcredito { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Tdebito { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Credito { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Transferencia { get; set; }
        [Column("TT")]
        public string? TT { get; set; }
        public int? Z { get; set; }
        public int? Nfact2 { get; set; }
        [Column("fechaHora")]
        public string? Fechahora { get; set; }
        [Column("mesa")]
        public string? Mesa { get; set; }
        [Column("pedido")]
        public string? Pedido { get; set; }
        [Column("devol")]
        public string? Devol { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? V1 { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Gratis { get; set; }

        public TimeSpan? Hora { get; set; }
        [Column("observacion")]
        public string? Observacion { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal? Retencion { get; set; }

        public int? NFpos { get; set; }
        [Column("NFelectronica")]
        public string? NFelectronica { get; set; }
        [Column("NoRemision")]
        public string? NoRemision { get; set; }
        [Column("NoCotizacion")]
        public string? NoCotizacion { get; set; }
        [Column("NoTraslado")]
        public string? NoTraslado { get; set; }
        [Column("NoSepare")]
        public string? NoSepare { get; set; }

        public bool Valida { get; set; }
        [Column("cufe")]
        public string? Cufe { get; set; }
        [Column("qr")]
        public string? Qr { get; set; }
        [Column("response")]
        public string? Response { get; set; }

        public int Electronica { get; set; }
        [Column("maq")]
        public string? Maq { get; set; }
        [Column("jsonBody")]
        public string? JsonBody { get; set; }
        [Column("error")]
        public string? Error { get; set; }

        public int TipoDoc { get; set; }
        public int Intentos { get; set; }
        public int? Z2 { get; set; }

        public DateTime? Fecha_Electronica { get; set; }
        public bool ValidaNC { get; set; }
        [Column("NCelectronica")]
        public string? NCelectronica { get; set; }
        [Column("cufeNC")]
        public string? CufeNC { get; set; }
        [Column("qrNC")]
        public string? QrNC { get; set; }
        [Column("responseNC")]
        public string? ResponseNC { get; set; }
        [Column("jsonBodyNC")]
        public string? JsonBodyNC { get; set; }
        public int IntentosNC { get; set; }
        [Column("errorNC")]
        public string? ErrorNC { get; set; }
        public DateTime? Fecha_NC_Electronica { get; set; }
        [Column("NoPrecuenta")]
        [NotMapped] // no se guarda en la base de datos
        public string FormaPago
        {
            get
            {
                if (Efectivo > 0) return "Efectivo";
                if (Tcredito > 0) return "Tarjeta Crédito";
                if (Tdebito > 0) return "Tarjeta Débito";
                if (Transferencia > 0) return "Transferencia";
                if (Credito > 0) return "Crédito";
                return "N/A";
            }
        }
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

