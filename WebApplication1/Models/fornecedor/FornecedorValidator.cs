using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.fornecedor
{
    public class FornecedorValidator : AbstractValidator<Fornecedor>
    {
        public FornecedorValidator()
        {
            RuleFor(x => x.nome)
                .NotNull().WithMessage("{PropertyName} é requerido")
                .MinimumLength(3).WithMessage("{PropertyName} requer no minimo 3 caracteres")
                .MaximumLength(20).WithMessage("{PropertyName} pode conter no maximo 20 caracteres");
            RuleFor(x => x.nomeFantasia)
                .NotNull().WithMessage("{PropertyName} é requerido")
                .MinimumLength(10).WithMessage("{PropertyName} requer no minimo 10 caracteres")
                .MaximumLength(60).WithMessage("{PropertyName} pode conter no maximo 60 caracteres");
            RuleFor(x => x.email)
                .EmailAddress();
            RuleFor(x => x.telefone)
                .NotNull().WithMessage("{PropertyName} é requerido");
                
        }
        
    }
}
