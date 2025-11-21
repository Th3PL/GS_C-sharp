using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.Gestor
{
    public record FuncionarioLiteDto(
        int Id,
        string Nome,
        string? Email,
        string? Matricula
    );

}
