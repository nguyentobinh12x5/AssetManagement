using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Application.Users.Queries.GetUsersBySearch;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Queries.GetUsersBySearch
{
    [TestFixture]
    public class GetUsersBySearchTests
    {
        private Mock<IIdentityService> _mockIdentityService;
        private GetUsersBySearchQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockIdentityService = new Mock<IIdentityService>();
            _handler = new GetUsersBySearchQueryHandler(_mockIdentityService.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnUsers_WhenSearchTermIsValid()
        {
            // Arrange
            var query = new GetUsersBySearchQuery
            {
                SearchTerm = "John",
                PageNumber = 1,
                PageSize = 5,
                SortColumnName = "id",
                SortColumnDirection = "Descending",
                Location = "HCM"
            };

            var expectedUsers = new PaginatedList<UserBriefDto>(
                new List<UserBriefDto>
                {
                    new UserBriefDto { UserName = "JohnDoe" }
                },
                1, 1, 5);

            _mockIdentityService.Setup(s => s.GetUserBriefsBySearchAsync(query))
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedUsers);
            _mockIdentityService.Verify(s => s.GetUserBriefsBySearchAsync(query), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenSearchTermIsNotFound()
        {
            // Arrange
            var query = new GetUsersBySearchQuery
            {
                SearchTerm = "NonExistentUser",
                PageNumber = 1,
                PageSize = 5,
                SortColumnName = "id",
                SortColumnDirection = "Descending",
                Location = "HCM"
            };

            var expectedUsers = new PaginatedList<UserBriefDto>(new List<UserBriefDto>(), 0, 1, 5);

            _mockIdentityService.Setup(s => s.GetUserBriefsBySearchAsync(query))
                .ReturnsAsync(expectedUsers);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Items.Should().BeEmpty();
            _mockIdentityService.Verify(s => s.GetUserBriefsBySearchAsync(query), Times.Once);
        }
    }
}