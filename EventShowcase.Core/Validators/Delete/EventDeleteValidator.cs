using EventShowcase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Core.Validators.Delete
{
    public class EventDeleteValidator : AbstractValidator<Event>
    {
        public EventDeleteValidator()
        {
            RuleFor(e => e.Id)
               .NotEmpty();
        }
    }
}
