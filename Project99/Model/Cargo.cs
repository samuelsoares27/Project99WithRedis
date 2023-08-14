using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project99.Model
{
    [Table("TbCargo")]
    public class Cargo
    {
        [Key]
        [Column("IdCargo")]
        public int Id { get; set; }

        [Required]
        [Column("Nome")]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column("Descricao")]
        [MaxLength(100)]
        public string Descricao { get; set; } = string.Empty;

        public virtual IEnumerable<Integrantes>? Integrantes { get; set; }
    }
}
