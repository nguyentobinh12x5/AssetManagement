namespace AssetManagement.Application.Assets.Commands.Create
{
    public class CreateNewAssetCommandValidator : AbstractValidator<CreateNewAssetCommand>
    {
        public CreateNewAssetCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(256).WithMessage("Name cannot be longer than 256 characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.Specification)
                .NotEmpty().WithMessage("Specification is required.")
                .MaximumLength(1200).WithMessage("Specification cannot be longer than 1200 characters.");

            RuleFor(x => x.InstallDate)
                .NotEmpty().WithMessage("Install date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Install date cannot be in the future.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(256).WithMessage("Location cannot be longer than 256 characters.");
        }
    }
}