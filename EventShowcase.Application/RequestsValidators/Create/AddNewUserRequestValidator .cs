using EventShowcase.API.Contracts.Users.Requests;
using EventShowcase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.RequestsValidators.Create
{
    public class AddNewUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public AddNewUserRequestValidator()
        {
            RuleFor(u => u.UserName)
               .NotEmpty();
            RuleFor(u => u.Email)
               .NotEmpty();
            RuleFor(u => u.Password)
               .NotEmpty();
        }
    }
}
