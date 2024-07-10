namespace AssetManagement.Application.Assignments.Commands.Update;

public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
{
    public UpdateAssignmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.AssetId)
            .NotEmpty().WithMessage("AssetId is required");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.Note)
            .NotEmpty().WithMessage("Specification is required.")
            .MaximumLength(1200).WithMessage("Specification cannot be longer than 1200 characters.");

        RuleFor(x => x.AssignedDate)
            .NotEmpty().WithMessage("Assigned date is required.")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Assigned date cannot be in the past.");
    }

}