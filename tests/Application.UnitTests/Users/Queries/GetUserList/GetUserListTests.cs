﻿using Ardalis.GuardClauses;

using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Application.Users.Queries.GetUsers;
using AssetManagement.Domain.Enums;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Queries.GetUserList;

[TestFixture]
public class GetUsersQueryHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private GetUsersQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _handler = new GetUsersQueryHandler(_identityServiceMock.Object);
    }

    [Test]
    public async Task Handle_UsersExists_ReturnsUserList()
    {
        // Arrange
        var query = new GetUsersQuery
        {
            PageNumber = 1,
            PageSize = 5,
            SortColumnName = "StaffCode",
            SortColumnDirection = "Descending",
            Location = "HCM"
        };

        var expectedUsers = new PaginatedList<UserBriefDto>(
            new List<UserBriefDto>
            {
                new UserBriefDto { UserName = "JohnDoe" }
            },
            1, 1, 5);

        _identityServiceMock.Setup(s => s.GetUserBriefsAsync(query))
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedUsers);
        _identityServiceMock.Verify(s => s.GetUserBriefsAsync(query), Times.Once);

    }

    [Test]
    public async Task Handle_UserListEmpty_ThrowsExceptionAsync()
    {
        // Arrange

        var query = new GetUsersQuery
        {
            PageNumber = 1,
            PageSize = 5,
            SortColumnName = "StaffCode",
            SortColumnDirection = "Descending",
            Location = "HCM"
        };

        var expectedUsers = new PaginatedList<UserBriefDto>(new List<UserBriefDto>(), 0, 1, 5);

        _identityServiceMock.Setup(s => s.GetUserBriefsAsync(query))
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Items.Should().BeEmpty();
        _identityServiceMock.Verify(s => s.GetUserBriefsAsync(query), Times.Once);
    }

    [Test]
    public async Task Handle_SortByTypeAscending_ReturnsUserList()
    {
        // Arrange
        var query = new GetUsersQuery
        {
            PageNumber = 1,
            PageSize = 5,
            SortColumnName = "Type",
            SortColumnDirection = "Ascending",
            Location = "HCM"
        };

        var expectedUsers = new PaginatedList<UserBriefDto>(
            new List<UserBriefDto>
            {
                new() { UserName = "JohnDoe" }
            },
            1, 1, 5);

        _identityServiceMock.Setup(s => s.GetUserBriefsAsync(query))
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedUsers);
        _identityServiceMock.Verify(s => s.GetUserBriefsAsync(query), Times.Once);
    }


}