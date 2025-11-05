using FluentValidation;
using FluentValidation.Results;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.Validators
{
    public class UserValidate : AbstractValidator<UserCreateRequest>
    {
        public UserValidate()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required.")
                .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required.")
                .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");
            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("DateOfBirth must be in the past.");
            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("PhoneNumber is required.")
                .Matches(@"^(?:\+84|0)(?:3[2-9]|5[689]|7[06-9]|8[1-5]|9\d)\d{7}$")
                .WithMessage("Invalid phone number format.");
        }
    }

    public class UserLoginValidate : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidate()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
