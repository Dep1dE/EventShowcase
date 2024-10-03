using EventShowcase.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventShowcase.Core.Validators.Create
{
    public class ImageCreateValidator : AbstractValidator<Image>
    {
        public ImageCreateValidator()
        {
            RuleFor(i => i.EventId)
               .NotEmpty();
            RuleFor(i => i.ImageData)
               .NotEmpty();
            RuleFor(i => i.ImageType)
               .NotEmpty();
        }
    }
}
