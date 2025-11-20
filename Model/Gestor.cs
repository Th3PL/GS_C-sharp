using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
public class Gestor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_GESTOR")]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        [Column("NOME")]
        public string Nome { get; set; } = string.Empty;

        [EmailAddress, MaxLength(200)]
        [Column("EMAIL")]
        public string? Email { get; set; }

        [MaxLength(50)]
        [Column("MATRICULA")]
        public string? Matricula { get; set; }

        // Navegação 1:N
        public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();

        [Column("CRIADO_EM")]
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        [Column("ATUALIZADO_EM")]
        public DateTime? AtualizadoEm { get; set; }
    }


}
