using EventShowcase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Core.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(u => u.Email)
               .NotEmpty();
            RuleFor(u => u.Name)
               .NotEmpty();
            RuleFor(u => u.PasswordHash)
               .NotEmpty();
        }
    }
}
