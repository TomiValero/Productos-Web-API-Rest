using FluentValidation;
using ProductosAPI.Models;

namespace ProductosAPI.Validators
{
    public class CategoriaInsertValidaton : AbstractValidator<Categoria>
    {
       public CategoriaInsertValidaton() 
       {
           RuleFor(x => x.Descripcion).NotEmpty();
       }
    }
}
