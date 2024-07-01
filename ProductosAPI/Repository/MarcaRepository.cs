using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductosAPI.Models;

namespace ProductosAPI.Repository
{
    public class MarcaRepository : IRepository<Marca>
    {
        private CatalogoP3DbContext _context;

        public MarcaRepository(CatalogoP3DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> Get()
        {
            return await _context.Marcas.ToListAsync();
        }

        public async Task<Marca> GetById(int id)
        {
            return await _context.Marcas.FindAsync(id);
        }

        public async Task Add(Marca marca)
        {
            await _context.Marcas.AddAsync(marca);
        }

        public void Update(Marca marca)
        {
            _context.Marcas.Attach(marca); //Aca falla
            _context.Marcas.Entry(marca).State = EntityState.Modified;
        }

        public void Delete(Marca marca)
        {
            _context.Marcas.Remove(marca);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Marca> Search(Func<Marca, bool> filter)
        {
            return _context.Marcas.Where(filter).ToList();
        }

    }
}
