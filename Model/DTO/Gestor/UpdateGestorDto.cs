using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO.Gestor
{
    public record UpdateGestorDto(
        string Nome,
        string? Email,
        string? Matricula
    );
}
