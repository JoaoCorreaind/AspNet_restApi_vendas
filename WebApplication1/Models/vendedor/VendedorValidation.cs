using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class VendedorValidation : AbstractValidator<Vendedor>
    {

        public VendedorValidation()
        {
            RuleFor(x => x.nome)
                .NotNull().WithMessage("nome é requerido")
                .MinimumLength(3).WithMessage("Nome requer no minimo 3 caracteres")
                .MaximumLength(20).WithMessage("nome pode conter no maximo 20 caracteres");
            RuleFor(x => x.cpf)
                    .NotNull().WithMessage("nome é requerido")
                    .MinimumLength(14).WithMessage("Nome precisa ter 14 caracteres")
                    .MaximumLength(14).WithMessage("Nome precisa ter 14 caracteres");
        }
        
    }
}
