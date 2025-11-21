using Model;
using Model.DTO.Gestor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IGestorService
    {
        Task<IEnumerable<GestorDto>> GetAllAsync();
        Task<GestorDto?> GetByIdAsync(int id);
        Task<GestorDto> CreateAsync(CreateGestorDto dto);
        Task<GestorDto> UpdateAsync(int id, UpdateGestorDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
