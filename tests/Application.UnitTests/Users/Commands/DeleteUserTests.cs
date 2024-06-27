using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.DeleteUser;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Commands
{
    public class DeleteUserCommandHandlerTests
    {
        private Mock<IIdentityService> _identityServiceMock;
        private Mock<IApplicationDbContext> _contextMock;
        private DeleteUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _identityServiceMock = new Mock<IIdentityService>();
            _contextMock = new Mock<IApplicationDbContext>();
            _handler = new DeleteUserCommandHandler(_identityServiceMock.Object, _contextMock.Object);
        }
        [Test]
        public async Task Handle_Success_DeleteUserAndSaveChanges()
        {
            // Arrange
            var userId = "existing-user-id";
            var command = new DeleteUserCommand(userId);
            var expectedResult = Result.Success();

            _identityServiceMock.Setup(x => x.DeleteUserAsync(userId))
                .ReturnsAsync(expectedResult);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _identityServiceMock.Verify(x => x.DeleteUserAsync(userId), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        public async Task Handle_UserExists_SuccessfullyDeletesUser()
        {
            // Arrange
            var userId = "existing-user-id";
            var command = new DeleteUserCommand(userId);
            var expectedResult = Result.Success();

            _identityServiceMock.Setup(x => x.DeleteUserAsync(userId))
                .ReturnsAsync(expectedResult);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _identityServiceMock.Verify(x => x.DeleteUserAsync(userId), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Handle_DeleteUserAsyncThrowsException_ThrowsException()
        {
            // Arrange
            var userId = "existing-user-id";
            var command = new DeleteUserCommand(userId);

            _identityServiceMock.Setup(x => x.DeleteUserAsync(userId))
                .ThrowsAsync(new Exception("Simulated DeleteUserAsync Exception"));

            // Act and Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Simulated DeleteUserAsync Exception");

            _identityServiceMock.Verify(x => x.DeleteUserAsync(userId), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }


        [Test]
        public async Task Handle_SaveChangesAsyncThrowsException_ThrowsException()
        {
            // Arrange
            var userId = "existing-user-id";
            var command = new DeleteUserCommand(userId);
            var expectedResult = Result.Success();

            _identityServiceMock.Setup(x => x.DeleteUserAsync(userId))
                .ReturnsAsync(expectedResult);

            _contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Simulated SaveChangesAsync Exception"));

            // Act and Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Simulated SaveChangesAsync Exception");

            _identityServiceMock.Verify(x => x.DeleteUserAsync(userId), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}