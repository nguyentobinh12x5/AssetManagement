using AssetManagement.Application.Users.Queries.GetUsers;

namespace Application.Users.Queries.GetUsers;

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(x => x.SortColumnName)
            .NotEmpty().WithMessage("SortColumnName is required.")
            .Must(BeAValidColumn).WithMessage("Sorting by Username is not allowed.");
    }

    private bool BeAValidColumn(string sortColumnName)
    {
        return !string.Equals(sortColumnName, "Username", StringComparison.OrdinalIgnoreCase);
    }
}