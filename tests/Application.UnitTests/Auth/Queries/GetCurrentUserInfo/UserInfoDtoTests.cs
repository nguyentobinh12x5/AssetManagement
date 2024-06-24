using System.Collections.Generic;

using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;

using FluentAssertions;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Auth.Queries.GetCurrentUserInfo;
public class UserInfoDtoTests
{
    [Test]
    public void Constructor_Should_Initialize_Required_Properties()
    {
        // Arrange & Act
        var userInfoDto = new UserInfoDto
        {
            Username = "test@example.com",
            MustChangePassword = false
        };

        // Assert
        userInfoDto.Username.Should().Be("test@example.com");
        userInfoDto.MustChangePassword.Should().BeFalse();
    }

    [Test]
    public void Roles_Should_Be_Initialized_As_Empty_List()
    {
        // Arrange & Act
        var userInfoDto = new UserInfoDto
        {
            Username = "test@example.com",
            MustChangePassword = false
        };

        // Assert
        userInfoDto.Roles.Should().NotBeNull();
        userInfoDto.Roles.Should().BeEmpty();
    }

    [Test]
    public void Should_Be_Able_To_Initialize_Roles_Property()
    {
        // Arrange
        var roles = new List<string> { "Admin", "User" };

        // Act
        var userInfoDto = new UserInfoDto
        {
            Username = "test@example.com",
            MustChangePassword = false,
            Roles = roles
        };

        // Assert
        userInfoDto.Roles.Should().BeEquivalentTo(roles);
    }
}