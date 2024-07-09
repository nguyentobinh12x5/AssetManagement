
namespace AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;

public class GetReturningRequestsWithPaginationQueryValidator : AbstractValidator<GetReturningRequestsWithPaginationQuery>
{
    public GetReturningRequestsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

        RuleFor(x => x.SortColumnName)
            .NotEmpty().WithMessage("SortColumnName is required.");

        RuleFor(x => x.SortColumnDirection)
            .NotEmpty().WithMessage("SortColumnDirection is required.");
    }
}