using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace JobApplicationFluentValidation.Models
{
    public class DateOfBirth : ValueObject
    {
        public DateTime Value { get; }

        private DateOfBirth(DateTime value)
        {
            Value = value;
        }
        public static Result<DateOfBirth> Create(DateTime value)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()) || value == DateTime.MinValue || value == default)
                return Result.Failure<DateOfBirth>("DateOfBirthNull");

            int age = DateTime.Today.Year - value.Year;

            if (age >= 18 && age <= 65)
            {
                return Result.Ok(new DateOfBirth(value));
            }
            else
                return Result.Failure<DateOfBirth>("AgeRestriction");
        }



        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
