using Ardalis.GuardClauses;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Domain.Enums;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Queries;

[TestFixture]
public class GetUserQueryHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private GetUserQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _handler = new GetUserQueryHandler(_identityServiceMock.Object);
    }

    [Test]
    public async Task Handle_UserExists_ReturnsUser()
    {
        // Arrange
        var userId = "user-id";
        var user = new UserDto
        {
            Id = userId,
            FirstName = "TestFirstName",
            LastName = "TestLastName",
            DateOfBirth = new DateTime(1990, 1, 1),
            JoinDate = new DateTime(2020, 1, 1),
            Gender = Gender.Male,
            Type = "TestRole"
        };

        _identityServiceMock.Setup(x => x.GetUserWithRoleAsync(userId))
            .ReturnsAsync(user);

        var query = new GetUserQuery { Id = userId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        result.Should().BeEquivalentTo(user);
        result.Type.Should().BeEquivalentTo(user.Type);
        _identityServiceMock.Verify(x => x.GetUserWithRoleAsync(userId), Times.Once);
    }

    [Test]
    public void Handle_UserDoesNotExist_ThrowsException()
    {
        // Arrange
        var userId = "non-existent-user-id";
        _identityServiceMock.Setup(x => x.GetUserWithRoleAsync(userId))
            .ThrowsAsync(new NotFoundException(userId, userId));

        var query = new GetUserQuery { Id = userId };

        // Act & Assert
        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);
        act.Should().ThrowAsync<NotFoundException>().WithMessage($"Queried object {userId} was not found, Key: {userId}");
    }

}