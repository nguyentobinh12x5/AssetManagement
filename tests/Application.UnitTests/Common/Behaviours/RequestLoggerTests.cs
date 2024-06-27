using AssetManagement.Application.Common.Behaviours;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Domain.Enums;

using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateUserCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateUserCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateUserCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateUserCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Location = "New York",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Gender.Male,
            JoinDate = DateTime.UtcNow,
            Type = "Employee"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateUserCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new CreateUserCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Location = "New York",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = Gender.Male,
            JoinDate = DateTime.UtcNow,
            Type = "Employee"
        }, new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}