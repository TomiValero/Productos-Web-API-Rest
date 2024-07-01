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
    public class MarcaController : ControllerBase
    {
        private ICommonService<Marca, Marca, Marca> _marcaService;

        private IValidator<Marca> _marcaInsertValidator;
        private IValidator<Marca> _marcaUpdateValidator;

 

        public MarcaController(
            [FromKeyedServices("marcaService")] ICommonService<Marca, Marca, Marca> marcaService,
            [FromKeyedServices("marcaInsertValidator")] IValidator<Marca> marcaInsertValidator,
            [FromKeyedServices("marcaUpdateValidator")] IValidator<Marca> marcaUpdateValidator
            )
        {
            _marcaService = marcaService;
            _marcaInsertValidator = marcaInsertValidator;
            _marcaUpdateValidator = marcaUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<Marca>> Get()
        {
            return await _marcaService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetById(int id)
        {
            var marca = await _marcaService.GetById(id);

            return marca == null ? NotFound() : Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> Add(Marca marca)
        {
            var validationResult = await _marcaInsertValidator.ValidateAsync(marca);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (!_marcaService.ValidateInsert(marca))
            {
                return BadRequest(_marcaService.Errors);
            }
            var marcaInsert = await _marcaService.Add(marca);

            return CreatedAtAction(nameof(GetById), new { id = marcaInsert.Id }, marcaInsert);
        }


        [HttpPut("{id}")] //Arreglar
        public async Task<ActionResult<Marca>> Update(Marca marca,int id)
        {
            var validationResult = await _marcaUpdateValidator.ValidateAsync(marca);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }


            if (!_marcaService.ValidateUpdate(marca))
            {
                return BadRequest(_marcaService.Errors);
            }
            var marcaUpdate = await _marcaService.Update(marca, id);

            return marcaUpdate == null ? NotFound() : Ok(marcaUpdate);
        }


        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Marca>> Delete(int id)
        {
            var marca = await _marcaService.Delete(id);

            return marca == null ? NotFound() : Ok(marca);
        }
    }
}
