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
                .NotNull().WithMessage("nome é requerido")
                .MinimumLength(3).WithMessage("Nome requer no minimo 3 caracteres")
                .MaximumLength(20).WithMessage("nome pode conter no maximo 20 caracteres");
            RuleFor(x => x.descricao)
                .NotNull().WithMessage("nome é requerido")
                .MinimumLength(3).WithMessage("Nome requer no minimo 3 caracteres")
                .MaximumLength(20).WithMessage("nome pode conter no maximo 20 caracteres");

        }
    }
}
