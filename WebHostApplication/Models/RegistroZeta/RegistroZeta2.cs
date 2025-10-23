using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.Models.RegistroZeta
{
    [Table("registrozeta2")]
    public class RegistroZeta2
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("z2")]
        public int Z2 { get; set; }

        [Column("fecha_hora", TypeName = "datetime")]
        public DateTime FechaHora { get; set; }

        [Column("maq")]
        public int Maq { get; set; }
    }
}
