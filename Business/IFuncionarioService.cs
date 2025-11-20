using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{

    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task<Funcionario?> GetByIdAsync(int id);
        Task<Funcionario> CreateAsync(Funcionario funcionario);
        Task<Funcionario> UpdateAsync(int id, Funcionario funcionario);
        Task<bool> DeleteAsync(int id);
    }

}
