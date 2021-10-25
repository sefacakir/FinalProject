using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Id).Empty().WithMessage(Messages.Empty);
            RuleFor(c => c.UserId).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(c => c.CompanyName).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(c => c.CompanyName).MinimumLength(2).WithMessage(Messages.MinLength);
        }
    }
}
