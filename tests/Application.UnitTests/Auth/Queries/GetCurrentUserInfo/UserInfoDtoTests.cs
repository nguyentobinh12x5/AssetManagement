using System.Collections.Generic;

using FluentAssertions;

using NUnit.Framework;

namespace AssetManagement.Application.Auth.Queries.GetCurrentUserInfo.Tests;
public class UserInfoDtoTests
{
    [Test]
    public void Constructor_Should_Initialize_Required_Properties()
    {
        // Arrange & Act
        var userInfoDto = new UserInfoDto
        {
            Email = "test@example.com",
            IsEmailConfirmed = true,
            MustChangePassword = false
        };

        // Assert
        userInfoDto.Email.Should().Be("test@example.com");
        userInfoDto.IsEmailConfirmed.Should().BeTrue();
        userInfoDto.MustChangePassword.Should().BeFalse();
    }

    [Test]
    public void Roles_Should_Be_Initialized_As_Empty_List()
    {
        // Arrange & Act
        var userInfoDto = new UserInfoDto
        {
            Email = "test@example.com",
            IsEmailConfirmed = true,
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
            Email = "test@example.com",
            IsEmailConfirmed = true,
            MustChangePassword = false,
            Roles = roles
        };

        // Assert
        userInfoDto.Roles.Should().BeEquivalentTo(roles);
    }
}