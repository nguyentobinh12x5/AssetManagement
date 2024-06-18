namespace AssetManagement.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(v => v.FirstName)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(v => v.LastName)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(v => v.DateOfBirth)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of Birth cannot be in the future.");

        RuleFor(v => v.JoinDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Join Date cannot be in the future.");

        RuleFor(v => v.Gender)
            .IsInEnum().WithMessage("Gender must be a valid value.");

        RuleFor(v => v.Type)
            .NotEmpty()
            .MaximumLength(256);
        
    }
    
}