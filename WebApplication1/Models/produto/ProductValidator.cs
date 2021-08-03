using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.produto
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("{PropertyName} é requerido")
                .MinimumLength(3).WithMessage("{PropertyName} requer no minimo 3 caracteres")
                .MaximumLength(45).WithMessage("{PropertyName} pode conter no maximo 45 caracteres");
            RuleFor(x => x.ValorCompra)
                .NotNull().WithMessage("{PropertyName} é requerido");
            RuleFor(x => x.ValorVenda)
                .NotNull().WithMessage("{PropertyName} é requerido");
            RuleFor(x => x.QuantidadeEstoque)
                .NotNull().WithMessage("{PropertyName} é requerido");
        }
    }
}
