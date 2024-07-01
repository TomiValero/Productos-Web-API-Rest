using ProductosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductosAPI.Repository
{
    public class CategoriaRepository : IRepository<Categoria>
    {
        private CatalogoP3DbContext _context;

        public CategoriaRepository(CatalogoP3DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> Get()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task Add(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
        }

        public void Update(Categoria categoria)
        {
            _context.Categorias.Attach(categoria);
            _context.Categorias.Entry(categoria).State = EntityState.Modified;
        }

        public void Delete(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Categoria> Search(Func<Categoria, bool> filter)
        {
            return _context.Categorias.Where(filter).ToList();
        }

    }
}
