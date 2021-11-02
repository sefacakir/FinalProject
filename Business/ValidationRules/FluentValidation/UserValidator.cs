using Business.Constants;
using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            //RuleFor(u => u.Password).NotEmpty().WithMessage(Messages.NotEmpty);
            //RuleFor(u => u.Password).Must(IsPasswordValid).WithMessage("Parolanız en az 8 karakter, en az bir harf ve bir sayı içermelidir.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.").When(u => !string.IsNullOrEmpty(u.Email));
        }

        private bool IsPasswordValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(arg);
        }
    }
}
