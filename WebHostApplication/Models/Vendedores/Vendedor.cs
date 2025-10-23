using System.ComponentModel.DataAnnotations;
//using WebHostApplication.Models.Detalles;

namespace WebHostApplication.Models
{
    public class Vendedor
    {
        [Key]
        public int IdVendedor { get; set; } 
        public string? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Dir { get; set; }
        public string? Tel { get; set; }

        public decimal? CreditoT { get; set; }
        public decimal? AbonoT { get; set; }
        public int? Condicion { get; set; }
        public decimal? Comision { get; set; }
        public decimal? Porcentaje { get; set; }
        //public ICollection<Detalle2> Facturas { get; set; } = new List<Detalle2>();
    }
}
