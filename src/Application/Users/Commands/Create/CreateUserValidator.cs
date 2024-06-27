namespace AssetManagement.Application.Users.Commands.Create
{
    public class CreateUserValidat : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidat()
        {
            RuleFor(x => x.FirstName)
                .Matches(@"^[\p{L} ]+$")
                .WithMessage("The First Name field allows only alphabetical characters. Please remove any numbers, special characters, or spaces");

            RuleFor(x => x.LastName)
            .Matches(@"^[\p{L} ]+$")
            .WithMessage("The Last Name field allows only alphabetical characters and spaces. Please remove any numbers or special characters.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .WithMessage("Please Select Date of Birth")
                .Must(BeAtLeast18YearsOld)
                .WithMessage("User is under 18. Please select a different date");

            RuleFor(x => x.JoinDate)
                .GreaterThanOrEqualTo(x => x.DateOfBirth.AddYears(18))
                .WithMessage("User under the age of 18 may not join the company. Please select a different date")
                .Must(NotBeWeekend)
                .WithMessage("Joined date is Saturday or Sunday. Please select a different date");

            RuleFor(x => x.JoinDate)
                .NotEmpty()
                .WithMessage("Please Select Date of Birth")
                .When(x => x.DateOfBirth == default(DateTime));
        }

        private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
        {
            return dateOfBirth <= DateTime.Today.AddYears(-18);
        }

        private bool NotBeWeekend(DateTime joinDate)
        {
            var dayOfWeek = joinDate.DayOfWeek;
            return dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday;
        }
    }
}