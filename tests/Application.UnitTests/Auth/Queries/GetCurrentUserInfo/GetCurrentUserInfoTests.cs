

using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Common.Interfaces;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Auth.Queries.GetCurrentUserInfo;

public class GetCurrentUserInfoTests
{
    private Mock<IIdentityService> _mockIdentityService;
    private Mock<IUser> _mockCurrentUser;
    private GetCurrentUserInfoQueryHandler _handler;
    [SetUp]
    public void SetUp()
    {
        _mockIdentityService = new Mock<IIdentityService>();
        _mockCurrentUser = new Mock<IUser>();
        _handler = new GetCurrentUserInfoQueryHandler(_mockIdentityService.Object, _mockCurrentUser.Object);
    }

    [Test]
    public async Task Handle_ThrowArgumentException_WhenUserIdNull()
    {
        // Arrange
        _mockCurrentUser.Setup(u => u.Id).Returns(string.Empty);
        var query = new GetCurrentUserInfoQuery();

        // Act
        var act = async () => await _handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
        _mockIdentityService.Verify(iS => iS.GetCurrentUserInfo(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Handle_ShouldReturnUserInfo_WhenUserExist()
    {
        // Arrange
        var userId = "123";
        var expected = new UserInfoDto
        {
            Email = "test@localhost",
            Location = "HCM",
            IsEmailConfirmed = true,
            MustChangePassword = true,
            Roles = ["Administrator"]
        };
        _mockCurrentUser.Setup(u => u.Id).Returns(userId);
        _mockIdentityService.Setup(s => s.GetCurrentUserInfo(userId)).ReturnsAsync(expected);
        var request = new GetCurrentUserInfoQuery();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expected);
        _mockIdentityService.Verify(s => s.GetCurrentUserInfo(userId), Times.Once);
    }
}