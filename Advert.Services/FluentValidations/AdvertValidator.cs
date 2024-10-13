using Advert.Entity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.FluentVali
{
    public class AdvertValidator : AbstractValidator<AdvertsEntity>
    {
        public AdvertValidator()
        {
            RuleFor(x => x.Description)
              .NotEmpty()
              .NotNull()
              .MinimumLength(3)
              .MaximumLength(150)
              .WithName("Başlık");

            RuleFor(x => x.Address)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(150)
                .WithName("İçerik");
        }
    }
}
