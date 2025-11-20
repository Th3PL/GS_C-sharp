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


    public class GestorService : IGestorService
    {
        private readonly ApplicationDbContext _context;

        public GestorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gestor>> GetAllAsync()
        {
            return await _context.Gestores
                                 .Include(g => g.Funcionarios)
                                 .ToListAsync();
        }

        public async Task<Gestor?> GetByIdAsync(int id)
        {
            return await _context.Gestores
                                 .Include(g => g.Funcionarios)
                                 .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Gestor> CreateAsync(Gestor gestor)
        {
            _context.Gestores.Add(gestor);
            await _context.SaveChangesAsync();
            return gestor;
        }

        public async Task<Gestor> UpdateAsync(int id, Gestor gestor)
        {
            var existing = await _context.Gestores.FindAsync(id);
            if (existing == null) throw new KeyNotFoundException("Gestor não encontrado");

            existing.Nome = gestor.Nome;
            existing.Email = gestor.Email;
            existing.Matricula = gestor.Matricula;
            existing.AtualizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gestor = await _context.Gestores.FindAsync(id);
            if (gestor == null) return false;

            _context.Gestores.Remove(gestor);
            await _context.SaveChangesAsync();
            return true;
        }
    }


}
