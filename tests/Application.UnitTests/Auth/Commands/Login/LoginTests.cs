using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Auth.Commands.Login;
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Auth.Commands.Login
{
    public class LoginTests
    {
        private Mock<IIdentityService> _mockIdentityService = null!;
        private LoginCommandHandler _handler = null!;

        [SetUp]
        public void SetUp()
        {
            _mockIdentityService = new Mock<IIdentityService>();
            _handler = new LoginCommandHandler(_mockIdentityService.Object);
        }

        [Test]
        public async Task ShouldLogin_WithValidCredentials()
        {
            // Arrange
            var command = new LoginCommand(
                "test@example.com",
                "password123",
                null,
                null,
                false,
                false
            );

            var successResult = Result.Success();

            _mockIdentityService.Setup(x => x.Login(command))
                .ReturnsAsync(successResult);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockIdentityService.Verify(i => i.Login(It.IsAny<LoginCommand>()), Times.Once);
        }

        [Test]
        public async Task ShouldThrowInvalidAuthenticationException_WithInvalidCredentials()
        {
            // Arrange
            var command = new LoginCommand(
                "test@example.com",
                "wrongpassword",
                null,
                null,
                false,
                false
            );

            _mockIdentityService.Setup(x => x.Login(command))
                                .ThrowsAsync(new InvalidAuthenticationException("Username or password is incorrect. Please try again"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidAuthenticationException>()
                .WithMessage("Username or password is incorrect. Please try again");
            _mockIdentityService.Verify(i => i.Login(It.IsAny<LoginCommand>()), Times.Once);
        }

        [Test]
        public async Task ShouldThrowInvalidAuthenticationException_WithLockedOutAccount()
        {
            // Arrange
            var command = new LoginCommand(
                "test@example.com",
                "password123",
                null,
                null,
                false,
                false
            );

            _mockIdentityService.Setup(x => x.Login(command))
                                .ThrowsAsync(new InvalidAuthenticationException("Your account is disabled. Please contact with IT Team"));

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidAuthenticationException>()
                .WithMessage("Your account is disabled. Please contact with IT Team");
            _mockIdentityService.Verify(i => i.Login(It.IsAny<LoginCommand>()), Times.Once);
        }
    }
}