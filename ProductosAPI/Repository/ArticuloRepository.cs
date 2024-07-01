using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductosAPI.Models;

namespace ProductosAPI.Repository
{
    public class ArticuloRepository : IRepository<Articulo>
    {
        private CatalogoP3DbContext _context;

        public ArticuloRepository(CatalogoP3DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Articulo>> Get()
        {
            return await _context.Articulos.ToListAsync();
        }

        public async Task<Articulo> GetById(int id)
        {
            return await _context.Articulos.FindAsync(id);
        }

        public async Task Add(Articulo articulo)
        {
             await _context.Articulos.AddAsync(articulo);
        }

        public void Update(Articulo articulo)
        {
            _context.Articulos.Attach(articulo);
            _context.Articulos.Entry(articulo).State = EntityState.Modified;
        }

        public void Delete(Articulo articulo)
        {
            _context.Articulos.Remove(articulo);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Articulo> Search(Func<Articulo, bool> filter)
        {
            return _context.Articulos.Where(filter).ToList();
        }
    }
}
