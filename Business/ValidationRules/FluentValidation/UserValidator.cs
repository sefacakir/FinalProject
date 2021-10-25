using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Id).Empty().WithMessage(Messages.Empty);
            RuleFor(u => u.FirstName).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(u => u.LastName).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(u => u.Email).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(u => u.Email).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(u => u.Password).NotEmpty().WithMessage(Messages.NotEmpty);
        }
    }
}
