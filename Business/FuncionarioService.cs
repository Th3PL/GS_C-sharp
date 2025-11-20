using Data;
using Microsoft.EntityFrameworkCore;
using Model;
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


        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            return await _context.Funcionarios
                                 .Include(f => f.Gestor)
                                 .ToListAsync();
        }

        public async Task<Funcionario?> GetByIdAsync(int id)
        {
            return await _context.Funcionarios
                                 .Include(f => f.Gestor)
                                 .FirstOrDefaultAsync(f => f.Id == id);
        }


        public async Task<Funcionario> CreateAsync(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return funcionario;
        }

        public async Task<Funcionario> UpdateAsync(int id, Funcionario funcionario)
        {
            var existing = await _context.Funcionarios.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Funcionário não encontrado");

            existing.Nome = funcionario.Nome;
            existing.Email = funcionario.Email;
            existing.Matricula = funcionario.Matricula;
            existing.GestorId = funcionario.GestorId;
            existing.AtualizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return false;

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
