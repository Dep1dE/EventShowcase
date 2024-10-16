using EventShowcase.API.Contracts.Events.Requests;
using EventShowcase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.RequestsValidators.Delete
{
    public class DeleteEventRequestValidator : AbstractValidator<DeleteEventRequest>
    {
        public DeleteEventRequestValidator()
        {
            RuleFor(e => e.idEvent)
               .NotEmpty();
        }
    }
}
