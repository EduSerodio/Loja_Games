using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lojaGames.Model;

namespace lojaGames.Service
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAll();
        Task<Categoria?> GetById(long id);
        Task<IEnumerable<Categoria>> GetByTipo(string tipo);
        Task<Categoria?> Create(Categoria categoria);
        Task<Categoria?> Update(Categoria categoria);
        Task Delete(Categoria categoria);
    }
}