using FluentValidation;
using ProductosAPI.Models;

namespace ProductosAPI.Validators
{
    public class CategoriaUpdateValidaton : AbstractValidator<Categoria>
    {
       public CategoriaUpdateValidaton() 
       {
           RuleFor(x => x.Descripcion).NotEmpty();
           RuleFor(x => x.Id).NotEmpty();
       }
    }
}
