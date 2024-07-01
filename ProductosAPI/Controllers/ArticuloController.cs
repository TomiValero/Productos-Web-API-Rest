using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Models;
using ProductosAPI.DTOs;
using ProductosAPI.Services;
using FluentValidation;


namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private ICommonService<ArticuloDto, ArticuloInsertDto, ArticuloUpdateDto> _articuloService;

        private IValidator<ArticuloInsertDto> _articuloInsertValidator;
        private IValidator<ArticuloUpdateDto> _articuloUpdateValidator;

        public ArticuloController(
            [FromKeyedServices("articuloService")] ICommonService<ArticuloDto, ArticuloInsertDto, ArticuloUpdateDto> articuloService,
            IValidator<ArticuloInsertDto> articuloInsertValidator,
            IValidator<ArticuloUpdateDto> articuloUpdateValidator
            ) 
        {
            _articuloService = articuloService;
            _articuloInsertValidator = articuloInsertValidator;
            _articuloUpdateValidator = articuloUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticuloDto>> Get()
        {
            return await _articuloService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloDto>> GetById(int id)
        {
            var articuloDto =  await _articuloService.GetById(id);

            return articuloDto == null ? NotFound() : Ok(articuloDto);
        }

        [HttpPost]
        public async Task<ActionResult<ArticuloDto>> Add(ArticuloInsertDto articuloInsertDto)
        {
            var validationResult = await _articuloInsertValidator.ValidateAsync(articuloInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_articuloService.ValidateInsert(articuloInsertDto))
            {
                return BadRequest(_articuloService.Errors);
            }

            var articuloDto = await _articuloService.Add(articuloInsertDto);

            return CreatedAtAction(nameof(GetById),new { id = articuloDto.Id }, articuloDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ArticuloDto>> Update(int id, ArticuloUpdateDto articuloUpdateDto)
        {
            var validationResult = await _articuloUpdateValidator.ValidateAsync(articuloUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_articuloService.ValidateUpdate(articuloUpdateDto))
            {
                return BadRequest(_articuloService.Errors);
            }
            var articuloDto = await _articuloService.Update(articuloUpdateDto, id);

            return articuloDto == null ? NotFound() : Ok(articuloDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ArticuloDto>> Delete(int id)
        {
            var articuloDto = await _articuloService.Delete(id);

            return articuloDto == null ? NotFound() : Ok(articuloDto);
        }

    }
}
