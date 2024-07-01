using FluentValidation;
using ProductosAPI.Models;

namespace ProductosAPI.Validators
{
    public class MarcaUpdateValidaton : AbstractValidator<Marca>
    {
       public MarcaUpdateValidaton() 
       {
           RuleFor(x => x.Descripcion).NotEmpty();
           RuleFor(x => x.Id).NotEmpty();
       }
    }
}
