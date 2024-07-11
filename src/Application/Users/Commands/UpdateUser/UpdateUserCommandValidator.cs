namespace AssetManagement.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
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