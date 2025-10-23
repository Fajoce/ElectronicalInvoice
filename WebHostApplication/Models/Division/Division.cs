using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.Models.Division
{
    [Table("division")]
    public class Division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [StringLength(50)]
        [Column("boton")]
        public string? Boton { get; set; }

        [StringLength(100)]
        [Column("nombre")]
        public string? Nombre { get; set; }

        [StringLength(255)]
        [Column("RutaImagen")]
        public string? RutaImagen { get; set; }
    }
}
