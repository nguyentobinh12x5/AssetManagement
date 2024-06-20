using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersByType;

using FluentAssertions;

using Moq;

using NUnit.Framework;
namespace AssetManagement.Application.UnitTests.Users.Queries.GetUsersByType
{
    [TestFixture]
    public class GetUsersByTypeTests
    {
        private Mock<IIdentityService> _mockIdentityService;
        private GetUsersByTypeQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockIdentityService = new Mock<IIdentityService>();
            _handler = new GetUsersByTypeQueryHandler(_mockIdentityService.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnUsers_WhenTypesAreValid()
        {
            // Arrange
            var query = new GetUsersByTypeQuery
            {
                Types = "Administrator,Staff",
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "StaffCode",
                SortColumnDirection = "Descending"
            };

            var expectedUsers = new PaginatedList<UserBriefDto>(
                new List<UserBriefDto>
                {
                    new UserBriefDto { UserName = "AdministratorUser" },
                    new UserBriefDto { UserName = "StaffUser" }
                },
                2, 1, 5);

            _mockIdentityService.Setup(s => s.GetUsersByTypesAsync(query))
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedUsers);
            _mockIdentityService.Verify(s => s.GetUsersByTypesAsync(query), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoUsersFoundForTypes()
        {
            // Arrange
            var query = new GetUsersByTypeQuery
            {
                Types = "NonExistentType",
                PageNumber = 1,
                PageSize = 5,
                SortColumnName = "id",
                SortColumnDirection = "Descending"
            };

            var expectedUsers = new PaginatedList<UserBriefDto>(new List<UserBriefDto>(), 0, 1, 5);

            _mockIdentityService.Setup(s => s.GetUsersByTypesAsync(query))
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Items.Should().BeEmpty();
            _mockIdentityService.Verify(s => s.GetUsersByTypesAsync(query), Times.Once);
        }
    }
}