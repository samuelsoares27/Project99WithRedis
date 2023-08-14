using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project99.Model
{
    [Table("TbIntegrantes")]
    public class Integrantes
    {
        [Key]
        [Column("IdIntegrantes")]
        public int Id { get; set; }

        [Required]
        [Column("Nome")]        
        [MaxLength(100)]        
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column("Apelido")]        
        [MaxLength(50)]
        public string Apelido { get; set; } = string.Empty;

        [Required]
        [Column("Idade")]        
        public int Idade { get; set; }

        [Required]
        [ForeignKey("IdCargo")]        
        public int IdCargo { get; set; }
        public virtual Cargo? Cargo { get; set; }
    }
}
