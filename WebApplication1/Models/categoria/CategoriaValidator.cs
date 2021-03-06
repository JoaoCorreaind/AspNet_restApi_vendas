using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.nome)
                .NotNull().WithMessage("{PropertyName} é requerido")
                .MinimumLength(3).WithMessage("{PropertyName} requer no minimo 3 caracteres")
                .MaximumLength(20).WithMessage("{PropertyName} pode conter no maximo 20 caracteres");
            RuleFor(x => x.descricao)
                .NotNull().WithMessage("{PropertyName} é requerido")
                .MinimumLength(10).WithMessage("{PropertyName} requer no minimo 10 caracteres")
                .MaximumLength(50).WithMessage("{PropertyName} pode conter no maximo 50 caracteres");

        }
    }
}
