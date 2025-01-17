﻿using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator:AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.Id).Empty().WithMessage(Messages.Empty);
            RuleFor(c => c.Name).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(c => c.Name).MinimumLength(2).WithMessage(Messages.MinLength);
        }
    }
}
