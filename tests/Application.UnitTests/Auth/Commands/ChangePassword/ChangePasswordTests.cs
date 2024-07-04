using AssetManagement.Application.Auth.Commands.ChangePassword;
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Auth.Commands.ChangePassword
{
    public class ChangePasswordTests
    {
        private Mock<IIdentityService> _mockIdentityService = null!;
        private UpdatePasswordCommandHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _mockIdentityService = new Mock<IIdentityService>();
            _handler = new UpdatePasswordCommandHandler(_mockIdentityService.Object);
        }

        [Test]
        public async Task ShouldUpdatePassword_WithValidCurrentAndNewPassword()
        {
            // Arrange
            var command = new UpdatePasswordCommand("ValidCurrentPassword123", "NewPassword123!");
            _mockIdentityService.Setup(x => x.CheckCurrentPassword(command.CurrentPassword)).ReturnsAsync(true);
            _mockIdentityService.Setup(x => x.ChangePasswordAsync(command.CurrentPassword, command.NewPassword)).ReturnsAsync(Result.Success());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockIdentityService.Verify(i => i.CheckCurrentPassword(It.IsAny<string>()), Times.Once);
            _mockIdentityService.Verify(i => i.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldThrowIncorrectPasswordException_WhenCurrentPasswordIsInvalid()
        {
            // Arrange
            var command = new UpdatePasswordCommand("InvalidCurrentPassword123", "NewPassword123!");
            _mockIdentityService.Setup(x => x.CheckCurrentPassword(command.CurrentPassword)).ThrowsAsync(new IncorrectPasswordException());

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<IncorrectPasswordException>();
            _mockIdentityService.Verify(i => i.CheckCurrentPassword(It.IsAny<string>()), Times.Once);
            _mockIdentityService.Verify(i => i.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task ShouldThrowApplicationException_WhenChangePasswordFails()
        {
            // Arrange
            var command = new UpdatePasswordCommand("ValidCurrentPassword123", "NewPassword123!");
            _mockIdentityService.Setup(x => x.CheckCurrentPassword(command.CurrentPassword)).ReturnsAsync(true);
            _mockIdentityService.Setup(x => x.ChangePasswordAsync(command.CurrentPassword, command.NewPassword)).ReturnsAsync(Result.Failure(new List<string> { "Error" }));

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ApplicationException>().WithMessage("Failed to update password for the current user");
            _mockIdentityService.Verify(i => i.CheckCurrentPassword(It.IsAny<string>()), Times.Once);
            _mockIdentityService.Verify(i => i.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}