using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebHostApplication.Models.Detalles2;
//using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models
{
  
        [Table("cajero")] // 👈 Cambia el nombre si tu tabla se llama distinto
        public class Cajero
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdCajero { get; set; }

            [StringLength(30)]
            public string? Nombre { get; set; }

            [Column("primerapellido")]
            [StringLength(30)]
            public string? PrimerApellido { get; set; }

        [Column("segundopellido")]
        [StringLength(30)]
            public string? SegundoApellido { get; set; }

            [StringLength(30)]
            public string? dato { get; set; }
        [NotMapped]
        public int HacerVentas { get; set; }
        [NotMapped]
        public int CambiarCantidad { get; set; }
        [NotMapped]
        public int CambiarPrecio { get; set; }
        [NotMapped]
        public int EliminarFila { get; set; }
        [NotMapped]
        public int HacerDevolucion { get; set; }
        [NotMapped]
        public int VerVentas { get; set; }
        [NotMapped]
        public int HacerCierres { get; set; }
        [NotMapped]
        public int VerUtilidad { get; set; }
        [NotMapped]
        public int VerCompras { get; set; }
        [NotMapped]
        public int HacerCompras { get; set; }
        [NotMapped]
        public int VerPagos { get; set; }
        [NotMapped]
        public int EliminarPagos { get; set; }
        [NotMapped]
        public int EditarClientes { get; set; }
        [NotMapped]
        public int HacerAbonos { get; set; }
        [NotMapped]
        public int EliminarAbonos { get; set; }
        [NotMapped]
        public int EntrarBotones { get; set; }
        [NotMapped]
        public int VerProductos { get; set; }
        [NotMapped]
        public int EditarProductos { get; set; }
        [NotMapped]
        public int EditarVendedor { get; set; }
        [NotMapped]
        public int EntrarSeguridad { get; set; }
        public ICollection<Detalle2> Facturas { get; set; } = new List<Detalle2>();
       // public virtual ICollection<Detalle1> Facturas2 { get; set; }
    }
    }

