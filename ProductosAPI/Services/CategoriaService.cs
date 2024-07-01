using ProductosAPI.Models;
using ProductosAPI.Repository;

namespace ProductosAPI.Services
{
    public class CategoriaService : ICommonService<Categoria, Categoria, Categoria>
    {
        public List<string> Errors { get;}

        private IRepository<Categoria> _categoriaRepository;

        public CategoriaService(IRepository<Categoria> categoriaRepository) { 
            Errors = new List<string>();
            _categoriaRepository = categoriaRepository;
        }


        public async Task<Categoria> Add(Categoria insertDto)
        {
            await _categoriaRepository.Add(insertDto);
            await _categoriaRepository.Save();
            return insertDto;
        }

        public async Task<Categoria> Delete(int id)
        {
            var categoria = await _categoriaRepository.GetById(id);
            if (categoria != null)
            {
                _categoriaRepository.Delete(categoria);
                await _categoriaRepository.Save();
                return categoria;
            }
            return null;
        }

        public async Task<IEnumerable<Categoria>> Get()
        {
            return await _categoriaRepository.Get();
        }

        public async Task<Categoria> GetById(int id)
        {
            return await _categoriaRepository.GetById(id); 
        }

        public async Task<Categoria> Update(Categoria updateDto, int id = -1)
        {
            var categoria = await _categoriaRepository.GetById(id);
            if (categoria != null)
            {
                categoria.Descripcion = updateDto.Descripcion;

                _categoriaRepository.Update(categoria);
                await _categoriaRepository.Save();
                return categoria;
            }
            return null;
            
        }

        public bool ValidateInsert(Categoria dto)
        {
            bool valid = true;

            if (_categoriaRepository.Search(b => b.Descripcion == dto.Descripcion).Count() > 0)
            {
                Errors.Add("No puedo existir un articulo con una Descripcion ya existente");
                valid = false;
            }
            
            return valid;
        }

        public bool ValidateUpdate(Categoria dto)
        {
            bool valid = true;

            if (_categoriaRepository.Search(b => b.Descripcion == dto.Descripcion && b.Id != dto.Id).Count() > 0 )
            {
                Errors.Add("No puedo existir un articulo con una Descripcion ya existente");
                valid = false;
            }

            return valid;
        }
    }
}
