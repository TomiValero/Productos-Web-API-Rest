using ProductosAPI.DTOs;
using ProductosAPI.Models;
using ProductosAPI.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductosAPI.Services
{
    public class ArticuloService : ICommonService<ArticuloDto, ArticuloInsertDto, ArticuloUpdateDto>
    {
        private IRepository<Articulo> _articuloRepository;
        private IRepository<Marca> _marcaRepository;
        private IRepository<Categoria> _categoriaRepository;

        public List<string> Errors { get; }

        public ArticuloService(IRepository<Articulo> articuloRepository,
            IRepository<Marca> marcaRepository,
            IRepository<Categoria> categoriaRepository)
        {
            _articuloRepository = articuloRepository;
            Errors = new List<string>();
            _marcaRepository = marcaRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<ArticuloDto>> Get()
        {
           var articulos = await _articuloRepository.Get();

            return articulos.Select(x => new ArticuloDto()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                IdMarca = x.IdMarca,
                IdCategoria = x.IdCategoria,
                Precio = x.Precio,
            });
           
        }

        public async Task<ArticuloDto> GetById(int id)
        {
            var articulo = await _articuloRepository.GetById(id);

            var articuloDto = new ArticuloDto()
            {
                Id = articulo.Id,
                Nombre = articulo.Nombre,
                Codigo = articulo.Codigo,
                Descripcion = articulo.Descripcion,
                IdMarca = articulo.IdMarca,
                IdCategoria = articulo.IdCategoria,
                Precio = articulo.Precio,
            };

            return articuloDto;
        }

        public async Task<ArticuloDto> Add(ArticuloInsertDto insertDto)
        {
            var articulo = new Articulo()
            {
                Nombre = insertDto.Nombre,
                Codigo = insertDto.Codigo,
                Descripcion = insertDto.Descripcion,
                IdMarca = insertDto.IdMarca,
                IdCategoria = insertDto.IdCategoria,
                Precio = insertDto.Precio,
            };
            await _articuloRepository.Add(articulo);
            await _articuloRepository.Save();

            var articuloDto = new ArticuloDto()
            {
                Id = articulo.Id,
                Nombre = articulo.Nombre,
                Codigo = articulo.Codigo,
                Descripcion = articulo.Descripcion,
                IdMarca = articulo.IdMarca,
                IdCategoria = articulo.IdCategoria,
                Precio = articulo.Precio,
            };

            return articuloDto;
        }

        public async Task<ArticuloDto> Update(ArticuloUpdateDto updateDto, int id)
        {
            var articulo = await _articuloRepository.GetById(id);
            if (articulo != null)
            {
                articulo.Codigo = updateDto.Codigo;
                articulo.Descripcion = updateDto.Descripcion;
                articulo.Nombre = updateDto.Nombre;
                articulo.IdMarca = updateDto.IdMarca;
                articulo.IdCategoria = updateDto.IdCategoria;
                articulo.Precio = updateDto.Precio;

                _articuloRepository.Update(articulo);
                await _articuloRepository.Save();

                var articuloDto = new ArticuloDto()
                {
                    Id = articulo.Id,
                    Nombre = articulo.Nombre,
                    Codigo = articulo.Codigo,
                    Descripcion = articulo.Descripcion,
                    IdMarca = articulo.IdMarca,
                    IdCategoria = articulo.IdCategoria,
                    Precio = articulo.Precio,
                };

                return articuloDto;
            }
            return null;
        }

        public async Task<ArticuloDto> Delete(int id)
        {
            var articulo = await _articuloRepository.GetById(id);
            if (articulo != null)
            {
                var articuloDto = new ArticuloDto()
                {
                    Id = articulo.Id,
                    Nombre = articulo.Nombre,
                    Codigo = articulo.Codigo,
                    Descripcion = articulo.Descripcion,
                    IdMarca = articulo.IdMarca,
                    IdCategoria = articulo.IdCategoria,
                    Precio = articulo.Precio,
                };

                _articuloRepository.Delete(articulo);
                await _articuloRepository.Save();

                return articuloDto;
            }
            return null;
            
        }

        public bool ValidateInsert(ArticuloInsertDto dto)
        {
            bool valid = true;

            if (_articuloRepository.Search(b => b.Nombre == dto.Nombre).Count() > 0)
            {
                Errors.Add("No puedo existir un articulo con un nombre ya existente");
                valid = false;
            }
            if (_articuloRepository.Search(b => b.Codigo == dto.Codigo).Count() > 0)
            {
                Errors.Add("No puedo existir un articulo con un Codigo ya existente");
                valid = false;
            }
            if (_marcaRepository.Search(b => b.Id == dto.IdMarca).Count() <= 0)
            {
                Errors.Add("No puedo existir un articulo con un idMarca que no este registrado");
                valid = false;
            }
            if (_categoriaRepository.Search(b => b.Id == dto.IdCategoria).Count() <= 0)
            {
                Errors.Add("No puedo existir un articulo con un IdCategoria que no este registrado");
                valid = false;
            }
            return valid;
        }

        public bool ValidateUpdate(ArticuloUpdateDto dto)
        {
            bool valid = true;

            if (_articuloRepository.Search(b => b.Nombre == dto.Nombre && b.Id != dto.Id).Count() > 0)
            {
                Errors.Add("No puedo existir un articulo con un nombre ya existente");
                valid = false;
            }
            if (_articuloRepository.Search(b => b.Codigo == dto.Codigo && b.Id != dto.Id).Count() > 0)
            {
                Errors.Add("No puedo existir un articulo con un Codigo ya existente");
                valid = false;
            }
            if (_marcaRepository.Search(b => b.Id == dto.IdMarca).Count() <= 0)
            {
                Errors.Add("No puedo existir un articulo con un idMarca que no este registrado");
                valid = false;
            }
            if (_categoriaRepository.Search(b => b.Id == dto.IdCategoria).Count() <= 0)
            {
                Errors.Add("No puedo existir un articulo con un IdCategoria que no este registrado");
                valid = false;
            }
            return valid;
        }
    }
}
