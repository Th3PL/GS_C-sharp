using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{

    public interface IGestorService
    {
        Task<IEnumerable<Gestor>> GetAllAsync();
        Task<Gestor?> GetByIdAsync(int id);
        Task<Gestor> CreateAsync(Gestor gestor);
        Task<Gestor> UpdateAsync(int id, Gestor gestor);
        Task<bool> DeleteAsync(int id);
    }

}
