using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Users.Queries.GetUsersBySearch
{
    public class GetUsersBySearchWithPaginationQueryValidator : AbstractValidator<GetUsersBySearchQuery>
    {
        public GetUsersBySearchWithPaginationQueryValidator()
        {
            RuleFor(x => x.SortColumnName)
                .NotEmpty().WithMessage("SortColumnName is required.")
                .Must(BeAValidColumn).WithMessage("Sorting by Username is not allowed.");

            RuleFor(x => x.SearchTerm)
                .MaximumLength(256).WithMessage("Search term cannot exceed 256 characters.")
                .Matches(@"^[a-zA-Z0-9\s]*$").WithMessage("Search term can only contain letters, numbers, and spaces.");

            RuleFor(x => x.SearchTerm)
                .NotEmpty().WithMessage("Search term is required.")
                .When(x => string.IsNullOrWhiteSpace(x.SortColumnName) && string.IsNullOrWhiteSpace(x.SortColumnDirection));
        }

        private bool BeAValidColumn(string sortColumnName)
        {
            return !string.Equals(sortColumnName, "Username", StringComparison.OrdinalIgnoreCase);
        }
    }
}
