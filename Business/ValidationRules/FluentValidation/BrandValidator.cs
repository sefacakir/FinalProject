using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Id).Empty().WithMessage(Messages.Empty);
            RuleFor(b => b.Name).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(b => b.Name).MinimumLength(2).WithMessage(Messages.MinLength);
        }
    }
}
