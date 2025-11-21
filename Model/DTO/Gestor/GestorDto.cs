using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.Gestor
{
    public record GestorDto(
        int Id,
        string Nome,
        string? Email,
        string? Matricula,
        IEnumerable<FuncionarioLiteDto> Funcionarios
    );

}
