using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using lojaGames.Model;

namespace lojaGames.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(t => t.Tipo)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(250);
        }
    }
}