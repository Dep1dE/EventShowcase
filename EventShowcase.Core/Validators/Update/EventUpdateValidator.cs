using EventShowcase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Core.Validators.Update
{
    public class EventUpdateValidator : AbstractValidator<Event>
    {
        public EventUpdateValidator()
        {
            RuleFor(e => e.Id)
               .NotEmpty();
            RuleFor(e => e.Title)
               .NotEmpty();
            RuleFor(e => e.Description)
               .NotEmpty();
            RuleFor(e => e.Location)
               .NotEmpty();
            RuleFor(e => e.Category)
               .NotEmpty();
            RuleFor(e => e.Date)
               .NotEmpty();
            RuleFor(e => e.MaxUserCount)
               .NotEmpty();
        }
    }
}
