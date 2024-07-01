using FluentValidation;
using ProductosAPI.Models;

namespace ProductosAPI.Validators
{
    public class MarcaInsertValidaton : AbstractValidator<Marca>
    {
       public MarcaInsertValidaton() 
       {
           RuleFor(x => x.Descripcion).NotEmpty();
       }
    }
}
