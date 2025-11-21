using Data;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTO.Gestor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{

    public class GestorService : IGestorService
    {
        private readonly ApplicationDbContext _context;

        public GestorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GestorDto>> GetAllAsync()
        {
            return await _context.Gestores
                .Select(g => new GestorDto(
                    g.Id,
                    g.Nome,
                    g.Email,
                    g.Matricula,
                    g.Funcionarios.Select(f => new FuncionarioLiteDto(
                        f.Id, f.Nome, f.Email, f.Matricula
                    ))
                ))
                .ToListAsync();
        }

        public async Task<GestorDto?> GetByIdAsync(int id)
        {
            return await _context.Gestores
                .Where(g => g.Id == id)
                .Select(g => new GestorDto(
                    g.Id,
                    g.Nome,
                    g.Email,
                    g.Matricula,
                    g.Funcionarios.Select(f => new FuncionarioLiteDto(
                        f.Id, f.Nome, f.Email, f.Matricula
                    ))
                ))
                .FirstOrDefaultAsync();
        }

        public async Task<GestorDto> CreateAsync(CreateGestorDto dto)
        {
            var entity = new Gestor
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Matricula = dto.Matricula,
                CriadoEm = DateTime.UtcNow
            };

            _context.Gestores.Add(entity);
            await _context.SaveChangesAsync();

            return new GestorDto(
                entity.Id,
                entity.Nome,
                entity.Email,
                entity.Matricula,
                Enumerable.Empty<FuncionarioLiteDto>()
            );
        }

        public async Task<GestorDto> UpdateAsync(int id, UpdateGestorDto dto)
        {
            var entity = await _context.Gestores.FindAsync(id);
            if (entity == null) throw new KeyNotFoundException("Gestor não encontrado");

            entity.Nome = dto.Nome;
            entity.Email = dto.Email;
            entity.Matricula = dto.Matricula;
            entity.AtualizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var funcionariosLite = await _context.Funcionarios
                .Where(f => f.GestorId == entity.Id)
                .Select(f => new FuncionarioLiteDto(f.Id, f.Nome, f.Email, f.Matricula))
                .ToListAsync();

            return new GestorDto(entity.Id, entity.Nome, entity.Email, entity.Matricula, funcionariosLite);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Gestores.FindAsync(id);
            if (entity == null) return false;

            _context.Gestores.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
