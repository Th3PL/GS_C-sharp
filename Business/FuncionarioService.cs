using Data;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTO.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{


    public class FuncionarioService : IFuncionarioService
    {
        private readonly ApplicationDbContext _context;

        public FuncionarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FuncionarioDto>> GetAllAsync()
        {
            return await _context.Funcionarios
                .Select(f => new FuncionarioDto(
                    f.Id,
                    f.Nome,
                    f.Email,
                    f.Matricula,
                    f.GestorId,
                    f.Gestor != null ? f.Gestor.Nome : null
                ))
                .ToListAsync();
        }

        public async Task<FuncionarioDto?> GetByIdAsync(int id)
        {
            return await _context.Funcionarios
                .Where(f => f.Id == id)
                .Select(f => new FuncionarioDto(
                    f.Id,
                    f.Nome,
                    f.Email,
                    f.Matricula,
                    f.GestorId,
                    f.Gestor != null ? f.Gestor.Nome : null
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<FuncionarioDto> CreateAsync(CreateFuncionarioDto dto)
        {
            var entity = new Funcionario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Matricula = dto.Matricula,
                GestorId = dto.GestorId,
                CriadoEm = DateTime.UtcNow
            };

            _context.Funcionarios.Add(entity);
            await _context.SaveChangesAsync();

            return new FuncionarioDto(entity.Id, entity.Nome, entity.Email, entity.Matricula, entity.GestorId, null);
        }

        public async Task<FuncionarioDto> UpdateAsync(int id, UpdateFuncionarioDto dto)
        {
            var entity = await _context.Funcionarios.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Funcionário não encontrado");

            entity.Nome = dto.Nome;
            entity.Email = dto.Email;
            entity.Matricula = dto.Matricula;
            entity.GestorId = dto.GestorId;
            entity.AtualizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new FuncionarioDto(entity.Id, entity.Nome, entity.Email, entity.Matricula, entity.GestorId, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Funcionarios.FindAsync(id);
            if (entity == null) return false;

            _context.Funcionarios.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
