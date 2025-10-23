namespace WebHostApplication.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace TuProyecto.Models
    {
        [Table("Categoria")]
        public class Categoria
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int IdCategoria { get; set; }

            [Required]
            [StringLength(100)]
            public string ButtonName { get; set; } = string.Empty;

            [Required]
            [StringLength(255)]
            public string NombreCategoria { get; set; } = string.Empty;

            [StringLength(255)]
            public string? ImagenRuta { get; set; }

            [StringLength(100)]
            public string? Fuente { get; set; }
        }
    }
}
