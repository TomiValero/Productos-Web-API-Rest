using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.DTOs;
using ProductosAPI.Services;
using ProductosAPI.Models;
using ProductosAPI.Validators;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private ICommonService<Categoria, Categoria, Categoria> _categoriaService;

        private IValidator<Categoria> _categoriaInsertValidator;
        private IValidator<Categoria> _categoriaUpdateValidator;

 

        public CategoriaController(
            [FromKeyedServices("categoriaService")] ICommonService<Categoria, Categoria, Categoria> categoriaService,
            [FromKeyedServices("categoriaInsertValidator")] IValidator<Categoria> categoriaInsertValidator,
            [FromKeyedServices("categoriaUpdateValidator")] IValidator<Categoria> categoriaUpdateValidator
            )
        {
            _categoriaService = categoriaService;
            _categoriaInsertValidator = categoriaInsertValidator;
            _categoriaUpdateValidator = categoriaUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<Categoria>> Get()
        {
            return await _categoriaService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            var categoria = await _categoriaService.GetById(id);

            return categoria == null ? NotFound() : Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Add(Categoria categoria)
        {
            var validationResult = await _categoriaInsertValidator.ValidateAsync(categoria);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (!_categoriaService.ValidateInsert(categoria))
            {
                return BadRequest(_categoriaService.Errors);
            }
            var categoriaInsert = await _categoriaService.Add(categoria);

            return CreatedAtAction(nameof(GetById), new { id = categoriaInsert.Id }, categoriaInsert);
        }


        [HttpPut("{id}")] //Arreglar
        public async Task<ActionResult<Categoria>> Update(Categoria categoria,int id)
        {
            var validationResult = await _categoriaUpdateValidator.ValidateAsync(categoria);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            if (!_categoriaService.ValidateUpdate(categoria))
            {
                return BadRequest(_categoriaService.Errors);
            }
            var categoriaUpdate = await _categoriaService.Update(categoria, id);

            return categoriaUpdate == null ? NotFound() : Ok(categoriaUpdate);
        }


        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoria = await _categoriaService.Delete(id);

            return categoria == null ? NotFound() : Ok(categoria);
        }
    }
}
