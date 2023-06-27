using FluentValidation;
using JobApplicationFluentValidation.Models;
using Microsoft.Extensions.Localization;
using System;

namespace JobApplicationFluentValidation
{
    public class ApplicationValidator : AbstractValidator<JobApplication>
    {
        public ApplicationValidator(IStringLocalizer<JobApplication> localizer)
        {
            RuleFor(x => x.FirstName).NotNull().Length(3, 30).Matches("^([a-zA-Z]{3,30}\\s*)+$").WithName(x => localizer["FirstName"]);
            RuleFor(x => x.LastName).NotEmpty().Length(3, 30).Matches("^([a-zA-Z]{3,30}\\s*)+$").WithName(x => localizer["LastName"]);
            RuleFor(x => x.Address).NotEmpty().Matches(@"^[a-zA-Z0-9\s]+$").WithName(x => localizer["Address"]);
            RuleFor(x => x.City).NotEmpty().Matches(@"^[a-zA-Z0-9\s]+$").WithName(x => localizer["City"]);
            RuleFor(x => x.CountryCode).NotEmpty().WithName(x => localizer["CountryCode"]);
            RuleFor(x => x.Phone).NotEmpty().Matches(@"^\+?\d{0,3}\s?\(?\d{2}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").WithName(x => localizer["Phone"]);
            RuleFor(x => x.WorkingPreferences).NotEmpty().WithName(x => localizer["WorkingPreferences"]);
            RuleFor(x => x.Position).NotEmpty().WithName(x => localizer["Position"]);
        }
    }
}
