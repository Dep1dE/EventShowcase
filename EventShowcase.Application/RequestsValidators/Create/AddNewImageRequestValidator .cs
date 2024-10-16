using EventShowcase.Application.Contracts.Images.Requests;
using EventShowcase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Application.RequestsValidators.Create
{
    public class AddNewImageRequestValidator : AbstractValidator<AddImageRequest>
    {
        public AddNewImageRequestValidator()
        {
            RuleFor(i => i.IdEvent)
               .NotEmpty();
            RuleFor(i => i.ImageData)
               .NotEmpty();
            RuleFor(i => i.ImageType)
               .NotEmpty();
        }
    }
}
