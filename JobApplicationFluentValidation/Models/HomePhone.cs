using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace JobApplicationFluentValidation.Models
{
    public class HomePhone : ValueObject
    {
        public string Value { get; }

        private HomePhone(string value)
        {
            Value = value;
        }

        public static Result<HomePhone> Create(string input)
        {
            if(input==null)
                return Result.Ok(new HomePhone(input));

            string phone = input.Trim().Replace(" ", "");
            if (!Regex.IsMatch(phone, "^(03[0-9]|04[9]|05[0-9])\\d{6}$"))
                return Result.Failure<HomePhone>("HomePhoneRegex");

            return Result.Ok(new HomePhone(phone));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
