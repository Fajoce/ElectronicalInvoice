using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.Models.Usuarios
{

        [Table("usuarios")]
        public class Usuario
        {
            [Key]
            public int Id { get; set; }

            [Required, StringLength(100)]
            public string Username { get; set; }

            [Required, StringLength(255)]
            public string Password { get; set; }

            [StringLength(150)]
            public string Nombre { get; set; }

            [StringLength(150)]
            public string Rol { get; set; }
    }
    }

