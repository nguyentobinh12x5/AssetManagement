
namespace AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;

using FluentValidation.TestHelper;

using NUnit.Framework;

[TestFixture]
public class GetReturningRequestsWithPaginationQueryValidatorTests
{
    private GetReturningRequestsWithPaginationQueryValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new GetReturningRequestsWithPaginationQueryValidator();
    }

    [Test]
    public void Should_DoNothing_WhenValidField()
    {
        var query = new GetReturningRequestsWithPaginationQuery
        {
            SortColumnDirection = "asc",
            SortColumnName = "Id",
            PageNumber = 1,
            PageSize = 5
        };
        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Should_HaveError_WhenFieldEmpty()
    {
        var query = new GetReturningRequestsWithPaginationQuery
        {
            SortColumnDirection = "",
            SortColumnName = ""
        };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(c => c.SortColumnName).WithErrorMessage("SortColumnName is required.");
        result.ShouldHaveValidationErrorFor(c => c.SortColumnDirection).WithErrorMessage("SortColumnDirection is required.");
    }

    [Test]
    public void Should_HaveError_WhenPageNumberAndSizeLowerThanOne()
    {
        var query = new GetReturningRequestsWithPaginationQuery
        {
            SortColumnDirection = "asc",
            SortColumnName = "Id",
            PageNumber = 0,
            PageSize = 0
        };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(c => c.PageNumber).WithErrorMessage("PageNumber at least greater than or equal to 1.");
        result.ShouldHaveValidationErrorFor(c => c.PageSize).WithErrorMessage("PageSize at least greater than or equal to 1.");
    }
}