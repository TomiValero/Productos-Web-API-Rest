using FluentValidation;
using ProductosAPI.DTOs;

namespace ProductosAPI.Validators
{
    public class ArticuloUpdateValidation : AbstractValidator<ArticuloUpdateDto>
    {
        public ArticuloUpdateValidation()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Codigo).NotEmpty();
            RuleFor(x => x.Descripcion).NotEmpty();
            RuleFor(x => x.IdMarca).NotEmpty();
            RuleFor(x => x.IdCategoria).NotEmpty();
            RuleFor(x => x.Precio).NotEmpty();
            RuleFor(x => x.Precio).GreaterThan(0);
        }
    }
}
