using Model;
using Model.DTO.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioDto>> GetAllAsync();
        Task<FuncionarioDto?> GetByIdAsync(int id);
        Task<FuncionarioDto> CreateAsync(CreateFuncionarioDto dto);
        Task<FuncionarioDto> UpdateAsync(int id, UpdateFuncionarioDto dto);
        Task<bool> DeleteAsync(int id);
    }


}
