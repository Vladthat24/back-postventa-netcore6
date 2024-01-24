using FluentValidation;
using POS.Aplication.Dtos.Category.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Aplication.Validations.Category
{
    public class CategoryValidator: AbstractValidator<CategoryRequestDto>
    {
        public CategoryValidator() {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo no puede ser nulo")
                .NotEmpty().WithMessage("El campo no puede ser vacio");
        }

    }
}
