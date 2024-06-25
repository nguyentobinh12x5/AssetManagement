using AssetManagement.Application.Auth.Commands.Logout;
using AssetManagement.Application.Common.Interfaces;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Auth.Commands.Logout;

public class LogutTests
{
    private Mock<IIdentityService> _mockIdentityService = null!;
    private LogoutCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockIdentityService = new Mock<IIdentityService>();
        _handler = new LogoutCommandHandler(_mockIdentityService.Object);
    }

    [Test]
    public async Task ShoudlCallIdentityServiceOnce()
    {
        // Arrange
        var command = new LogoutCommand();
        _mockIdentityService.Setup(x => x.Logout());

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockIdentityService.Verify(i => i.Logout(), Times.Once);
    }
}