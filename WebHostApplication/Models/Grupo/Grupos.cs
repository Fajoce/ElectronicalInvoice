using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHostApplication.Models
{
    [Table("Grupos")]
    public class Grupos
    {
        [Key]
        public int IdGrupo { get; set; }

        public string Grupo { get; set; } = null!;
    }
}
