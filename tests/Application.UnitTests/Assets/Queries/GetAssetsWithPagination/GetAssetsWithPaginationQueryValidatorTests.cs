using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAssetsWithPagination
{
    [TestFixture]
    internal class GetAssetsWithPaginationQueryValidatorTests
    {
        private GetAssetsWithPaginationQueryValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new GetAssetsWithPaginationQueryValidator();
        }

        [Test]
        public void Should_HaveError_WhenPageNumberIsLessThan1()
        {
            // Arrange
            var query = new GetAssetsWithPaginationQuery
            {
                PageNumber = 0,
                SortColumnName = "Name",
                SortColumnDirection = "ASC"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.PageNumber)
                .WithErrorMessage("PageNumber at least greater than or equal to 1.");
        }

        [Test]
        public void Should_HaveError_WhenPageSizeIsLessThan1()
        {
            // Arrange
            var query = new GetAssetsWithPaginationQuery
            {
                PageSize = 0,
                SortColumnName = "Name",
                SortColumnDirection = "ASC"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.PageSize)
                .WithErrorMessage("PageSize at least greater than or equal to 1.");
        }

        [Test]
        public void Should_HaveError_WhenSortColumnNameIsEmpty()
        {
            // Arrange
            var query = new GetAssetsWithPaginationQuery
            {
                SortColumnName = string.Empty,
                SortColumnDirection = "ASC"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.SortColumnName)
                .WithErrorMessage("SortColumnName is required.");
        }

        [Test]
        public void Should_HaveError_WhenSortColumnDirectionIsEmpty()
        {
            // Arrange
            var query = new GetAssetsWithPaginationQuery
            {
                SortColumnName = "Name",
                SortColumnDirection = string.Empty
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.SortColumnDirection)
                .WithErrorMessage("SortColumnDirection is required.");
        }

        [Test]
        public void Should_NotHaveError_WhenQueryIsValid()
        {
            // Arrange
            var query = new GetAssetsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Name",
                SortColumnDirection = "ASC"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}