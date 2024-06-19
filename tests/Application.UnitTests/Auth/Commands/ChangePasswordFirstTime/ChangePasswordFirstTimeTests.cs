using AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Auth.Commands.ChangePasswordFirstTime;


public class ChangePasswordFirstTimeTest
{
    private Mock<IIdentityService> _mockIdentityService = null!;
    private ChangePasswordFirstTimeCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockIdentityService = new Mock<IIdentityService>();
        _handler = new ChangePasswordFirstTimeCommandHandler(_mockIdentityService.Object);
    }


    [Test]
    public async Task ShouldChangePassword_WithValidNewPassword()
    {
        // Arrange
        var command = new ChangePasswordFirstTimeCommand("NewPassword123!");
        _mockIdentityService.Setup(x => x.IsSameOldPassword(command.NewPassword))
                            .ReturnsAsync(false);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockIdentityService.Verify(i => i.IsSameOldPassword(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldThrowBadRequestExp_WhenInputOldPassword()
    {
        // Arrange
        var command = new ChangePasswordFirstTimeCommand("OldPassword123!");
        _mockIdentityService.Setup(x => x.IsSameOldPassword(command.NewPassword))
                            .ReturnsAsync(true);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage("New password must not the same as old one.");
        _mockIdentityService.Verify(i => i.IsSameOldPassword(It.IsAny<string>()), Times.Once);
    }
}