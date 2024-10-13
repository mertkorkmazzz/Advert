using Advert.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.FluentVali
{
    public class CategoryValidator : AbstractValidator<CategoryEntity>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100)
            .WithName("Kategori Adı");
        }
    }
}
