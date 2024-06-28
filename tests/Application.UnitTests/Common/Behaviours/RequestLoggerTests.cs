﻿using AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;
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
    private Mock<ILogger<ChangePasswordFirstTimeCommand>> _logger = null!;
    private Mock<IUser> _user = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<ChangePasswordFirstTimeCommand>>();
        _user = new Mock<IUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _user.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<ChangePasswordFirstTimeCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new ChangePasswordFirstTimeCommand("NewPassword"), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<ChangePasswordFirstTimeCommand>(_logger.Object, _user.Object, _identityService.Object);

        await requestLogger.Process(new ChangePasswordFirstTimeCommand("New password"), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}