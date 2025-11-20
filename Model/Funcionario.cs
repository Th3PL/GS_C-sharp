using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class Funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_FUNCIONARIO")]
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

        // FK opcional para permitir funcionário sem gestor; torne Required se quiser forçar
        [Column("ID_GESTOR")]
        public int? GestorId { get; set; }

        // Navegação N:1
        [ForeignKey(nameof(GestorId))]
        public Gestor? Gestor { get; set; }

        [Column("CRIADO_EM")]
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        [Column("ATUALIZADO_EM")]
        public DateTime? AtualizadoEm { get; set; }
    }

}
