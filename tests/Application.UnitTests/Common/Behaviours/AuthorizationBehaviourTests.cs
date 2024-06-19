
using AssetManagement.Application.Common.Behaviours;
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;

using FluentAssertions;

using MediatR;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Common.Behaviours;

public class AuthorizationBehaviourTests
{
    private Mock<IUser> _userMock;
    private Mock<IIdentityService> _identityServiceMock;
    private AuthorizationBehaviour<BaseTestRequest, TestResponse> _behaviour;

    [SetUp]
    public void Setup()
    {
        _userMock = new Mock<IUser>();
        _identityServiceMock = new Mock<IIdentityService>();
        _behaviour = new AuthorizationBehaviour<BaseTestRequest, TestResponse>(_userMock.Object, _identityServiceMock.Object);
    }

    [Test]
    public async Task Handle_ShouldNotThrow_WhenNoAuthorizeAttributes()
    {
        // Arrange
        var request = new BaseTestRequest();
        var response = new TestResponse();
        RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(response);

        // Act
        Func<Task> act = async () => await _behaviour.Handle(request, next, CancellationToken.None);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public void Handle_ShouldThrowUnauthorizedAccessException_WhenUserNotAuthenticated()
    {
        // Arrange
        var request = new TestRequestWithAuthorize();
        var response = new TestResponse();
        RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(response);

        _userMock.Setup(u => u.Id).Returns((string?)null);

        // Act
        Func<Task> act = async () => await _behaviour.Handle(request, next, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Test]
    public void Handle_ShouldThrowForbiddenAccessException_WhenUserNotInRole()
    {
        // Arrange
        var request = new TestRequestWithAuthorizeRole();
        var response = new TestResponse();
        RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(response);

        _userMock.Setup(u => u.Id).Returns("123");
        _identityServiceMock.Setup(s => s.IsInRoleAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        Func<Task> act = async () => await _behaviour.Handle(request, next, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public void Handle_ShouldThrowForbiddenAccessException_WhenUserNotAuthorizedByPolicy()
    {
        // Arrange
        var request = new TestRequestWithAuthorizePolicy();
        var response = new TestResponse();
        RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(response);

        _userMock.Setup(u => u.Id).Returns("123");
        _identityServiceMock.Setup(s => s.AuthorizeAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        Func<Task> act = async () => await _behaviour.Handle(request, next, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task Handle_ShouldProceed_WhenUserAuthorized()
    {
        // Arrange
        var request = new TestRequestWithAuthorize();
        var response = new TestResponse();
        RequestHandlerDelegate<TestResponse> next = () => Task.FromResult(response);

        _userMock.Setup(u => u.Id).Returns("123");
        _identityServiceMock.Setup(s => s.IsInRoleAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _behaviour.Handle(request, next, CancellationToken.None);

        // Assert
        result.Should().Be(response);
    }

    private class BaseTestRequest { }

    [Authorize]
    private class TestRequestWithAuthorize : BaseTestRequest { }

    [Authorize(Roles = "Admin")]
    private class TestRequestWithAuthorizeRole : BaseTestRequest { }

    [Authorize(Policy = "TestPolicy")]
    private class TestRequestWithAuthorizePolicy : BaseTestRequest { }

    private class TestResponse { }
}