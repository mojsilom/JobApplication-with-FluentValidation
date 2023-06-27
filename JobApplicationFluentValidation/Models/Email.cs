using CSharpFunctionalExtensions;
using Microsoft.Extensions.Localization;
using System;
using System.Text.RegularExpressions;

namespace JobApplicationFluentValidation.Models
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }
        public static Result<Email> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Email>("IsNullOrWhiteSpace");

            string email = input.Trim().Replace(" ", "");

            if (email.Length > 256)
                return Result.Failure<Email>("MaximumLength_Simple");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("RegularExpressionValidator");

            return Result.Ok(new Email(email));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
