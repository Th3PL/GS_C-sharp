using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.Funcionario
{
        public record FuncionarioDto(
        int Id,
        string Nome,
        string? Email,
        string? Matricula,
        int? GestorId
    );
}
