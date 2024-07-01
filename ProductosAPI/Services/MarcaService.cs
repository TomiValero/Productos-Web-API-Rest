using ProductosAPI.Models;
using ProductosAPI.Repository;

namespace ProductosAPI.Services
{
    public class MarcaService : ICommonService<Marca, Marca, Marca>
    {
        public List<string> Errors { get;}

        private IRepository<Marca> _marcaRepository;

        public MarcaService(IRepository<Marca> marcaRepository) { 
            Errors = new List<string>();
            _marcaRepository = marcaRepository;
        }


        public async Task<Marca> Add(Marca insertDto)
        {
            await _marcaRepository.Add(insertDto);
            await _marcaRepository.Save();
            return insertDto;
        }

        public async Task<Marca> Delete(int id)
        {
            var marca = await _marcaRepository.GetById(id);
            if (marca != null)
            {
                _marcaRepository.Delete(marca);
                await _marcaRepository.Save();
                return marca;
            }
            return null;
        }

        public async Task<IEnumerable<Marca>> Get()
        {
            return await _marcaRepository.Get();
        }

        public async Task<Marca> GetById(int id)
        {
            return await _marcaRepository.GetById(id); 
        }

        public async Task<Marca> Update(Marca updateDto, int id = -1)
        {
            var marca = await _marcaRepository.GetById(id);
            if (marca != null)
            {
                marca.Descripcion = updateDto.Descripcion;

                _marcaRepository.Update(marca);
                await _marcaRepository.Save();
                return marca;
            }
            return null;
            
        }

        public bool ValidateInsert(Marca dto)
        {
            bool valid = true;

            if (_marcaRepository.Search(b => b.Descripcion == dto.Descripcion).Count() > 0)
            {
                Errors.Add("No puedo existir un articulo con una Descripcion ya existente");
                valid = false;
            }
            
            return valid;
        }

        public bool ValidateUpdate(Marca dto)
        {
            bool valid = true;

            if (_marcaRepository.Search(b => b.Descripcion == dto.Descripcion && b.Id != dto.Id).Count() > 0 )
            {
                Errors.Add("No puedo existir un articulo con una Descripcion ya existente");
                valid = false;
            }

            return valid;
        }
    }
}
