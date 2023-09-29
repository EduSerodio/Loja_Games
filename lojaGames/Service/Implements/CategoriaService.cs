using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lojaGames.Data;
using lojaGames.Model;
using Microsoft.EntityFrameworkCore;

namespace lojaGames.Service.Implements
{
    public class CategoriaService : ICategoriaService
    {   
        //implementanto os metodos da AppDbContext
        private readonly AppDbContext _context;

        //construtor
        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Categoria?> Create(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task Delete(Categoria categoria)
        {
            _context.Remove(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
           return await _context.Categorias
                    .Include(c => c.Produto)
                    .ToListAsync();
        }

        public async Task<Categoria?> GetById(long id)
        {
            try
            {
                var Categoria = await _context.Categorias
                                        .Include(c => c.Produto)
                                        .FirstAsync(c => c.Id == id);
                return Categoria;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Categoria>> GetByTipo(string tipo)
        {
            var Categoria = await _context.Categorias
                                    .Include(c => c.Produto)
                                    .Where(c => c.Tipo.Contains(tipo))
                                    .ToListAsync();
            return Categoria;
        }

        public async Task<Categoria?> Update(Categoria categoria)
        {
            var CategoriaUpdate = await _context.Categorias.FindAsync(categoria.Id);

            if (CategoriaUpdate is null)
            {
                return null;
            }

            _context.Entry(CategoriaUpdate).State = EntityState.Detached;
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return categoria;

        }
    }
}