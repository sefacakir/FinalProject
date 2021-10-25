using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator:AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.Id).Empty().WithMessage(Messages.Empty);
            RuleFor(r => r.CarId).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(r => r.CustomerId).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(r => r.RentDate).NotEmpty().WithMessage(Messages.NotEmpty);
            RuleFor(r => r.ReturnDate).NotEmpty().WithMessage(Messages.NotEmpty);
        }
    }
}
