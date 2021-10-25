using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator:AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Description).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(c => c.Description).MinimumLength(2).WithMessage(Messages.MinLength);
            RuleFor(c => c.DailyPrice).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage(Messages.GreaterThan);
            //RuleFor(c => c.Description).Must(StartWithUppercase).WithMessage("Açıklama kısmı büyük harf ile yazılmalıdır.");
        }

        private bool StartWithUppercase(string arg)
        {
            char chr = arg[0];
            if (chr >= 65 && chr <= 90)
            {
                return true;
            }
            return false;
        }
    }
}
