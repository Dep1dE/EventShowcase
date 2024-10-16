using EventShowcase.API.Contracts.Events.Requests;
using FluentValidation;

namespace EventShowcase.Application.RequestsValidators.Create
{
    public class AddNewEventRequestValidator : AbstractValidator<AddNewEventRequest>
    {
        public AddNewEventRequestValidator()
        {
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
