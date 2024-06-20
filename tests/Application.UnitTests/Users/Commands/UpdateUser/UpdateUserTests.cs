using Ardalis.GuardClauses;

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Domain.Enums;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Commands.UpdateUser;

[TestFixture]
public class GetUpdateUserTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private UpdateUserCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();

        _handler = new UpdateUserCommandHandler(
            _identityServiceMock.Object
        );
    }

    [Test]
    public async Task Handle_UserExistsAndUpdateSuccess_ReturnsResult()
    {
        // Arrange
        var userId = "user-id";
        var user = new UserDto
        {
            Id = userId,
            FirstName = "OldFirstName",
            LastName = "OldLastName",
            DateOfBirth = new DateTime(1990, 1, 1),
            JoinDate = new DateTime(2020, 1, 1),
            Gender = Gender.Male,
            Type = "OldRole"
        };
        var updateCommand = new UpdateUserCommand
        {
            Id = userId,
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            DateOfBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            JoinDate = new DateTime(2020, 1, 1),
            Type = "NewRole"
        };

        var result = Result.Success();
        _identityServiceMock.Setup(x => x.GetUserWithRoleAsync(userId))
            .ReturnsAsync(user);

        _identityServiceMock.Setup(x => x.UpdateUserAsync(It.IsAny<UserDto>()))
            .ReturnsAsync(result);

        _identityServiceMock.Setup(x => x.UpdateUserToRoleAsync(userId, "OldRole", "NewRole"))
            .ReturnsAsync(result);

        // Act
        await _handler.Handle(updateCommand, CancellationToken.None);

        // Assert
        _identityServiceMock.Verify(x => x.GetUserWithRoleAsync(userId), Times.Once);
        _identityServiceMock.Verify(x => x.UpdateUserAsync(It.Is<UserDto>(u =>
            u.Id == userId &&
            u.FirstName == "NewFirstName" &&
            u.LastName == "NewLastName" &&
            u.DateOfBirth == new DateTime(2000, 1, 1) &&
            u.Gender == Gender.Male &&
            u.JoinDate == new DateTime(2020, 1, 1)
        )), Times.Once);
        _identityServiceMock.Verify(x => x.UpdateUserToRoleAsync(userId, "OldRole", "NewRole"), Times.Once);
        _identityServiceMock.Verify(x => x.GetUserWithRoleAsync(userId), Times.Once);
    }

    [Test]
    public async Task Handle_UserDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var userId = "user-id";
        var updateCommand = new UpdateUserCommand
        {
            Id = userId,
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            DateOfBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            JoinDate = new DateTime(2020, 1, 1),
            Type = "NewRole"
        };

        _identityServiceMock.Setup(x => x.GetUserWithRoleAsync(userId))
            .ThrowsAsync(new NotFoundException(userId, userId));

        // Act
        Func<Task> act = async () => await _handler.Handle(updateCommand, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Queried object {userId} was not found, Key: {userId}");

        _identityServiceMock.Verify(x => x.GetUserWithRoleAsync(userId), Times.Once);
        _identityServiceMock.Verify(x => x.UpdateUserAsync(It.IsAny<UserDto>()), Times.Never);
    }
    [Test]
    public async Task Handle_UpdateUserFails_ThrowsException()
    {
        // Arrange
        var userId = "user-id"; ;
        var user = new UserDto
        {
            Id = userId,
            FirstName = "OldFirstName",
            LastName = "OldLastName",
            DateOfBirth = new DateTime(1990, 1, 1),
            JoinDate = new DateTime(2020, 1, 1),
            Gender = Gender.Male,
            Type = "OldRole"
        };
        var updateCommand = new UpdateUserCommand
        {
            Id = userId,
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            DateOfBirth = new DateTime(2000, 1, 1),
            Gender = Gender.Male,
            JoinDate = new DateTime(2020, 1, 1),
            Type = "NewRole"
        };

        _identityServiceMock.Setup(x => x.GetUserWithRoleAsync(userId))
            .ReturnsAsync(user);

        _identityServiceMock.Setup(x => x.UpdateUserAsync(It.IsAny<UserDto>()))
            .ThrowsAsync(new Exception("Update failed"));

        // Act
        Func<Task> act = async () => await _handler.Handle(updateCommand, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<Exception>();

        _identityServiceMock.Verify(x => x.GetUserWithRoleAsync(userId), Times.Once);
        _identityServiceMock.Verify(x => x.UpdateUserToRoleAsync(userId, "OldRole", "NewRole"), Times.Never);
    }
}