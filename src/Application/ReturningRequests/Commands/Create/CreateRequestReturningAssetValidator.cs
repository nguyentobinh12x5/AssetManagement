using FluentValidation;

namespace AssetManagement.Application.ReturningRequests.Commands.Create
{
    public class CreateRequestReturningAssetCommandValidator : AbstractValidator<CreateRequestReturningAssetCommand>
    {
        public CreateRequestReturningAssetCommandValidator()
        {
            RuleFor(x => x.AssignmentId)
                .NotEmpty().WithMessage("Assignment ID is required.");

        }
    }
}