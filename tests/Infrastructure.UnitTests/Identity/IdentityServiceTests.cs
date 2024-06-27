using System.Security.Claims;

using Ardalis.GuardClauses;

using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Application.Users.Queries.GetUser;
using AssetManagement.Infrastructure.Data;
using AssetManagement.Infrastructure.Identity;

using AutoMapper;

using FluentAssertions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Infrastructure.UnitTests.Identity;

public class IdentityServicesTests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<SignInManager> _signInManagerMock;
    private Mock<ApplicationDbContext> _applicationDbContextMock;
    private Mock<IUserClaimsPrincipalFactory<ApplicationUser>> _userClaimsPrincipalFactoryMock;
    private Mock<IAuthorizationService> _authorizationServiceMock;
    private Mock<IMapper> _mapperMock;
    private Mock<IUser> _currentUserMock;
    private IdentityService _identityService;

    [SetUp]
    public void SetUp()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        var optionsMock = new Mock<IOptions<IdentityOptions>>();
        var passwordHasherMock = new Mock<IPasswordHasher<ApplicationUser>>();
        var userValidators = new List<IUserValidator<ApplicationUser>> { new Mock<IUserValidator<ApplicationUser>>().Object };
        var passwordValidators = new List<IPasswordValidator<ApplicationUser>> { new Mock<IPasswordValidator<ApplicationUser>>().Object };
        var keyNormalizerMock = new Mock<ILookupNormalizer>();
        var errorsMock = new Mock<IdentityErrorDescriber>();
        var authSchemeMock = new Mock<IAuthenticationSchemeProvider>();
        var userCofirmMock = new Mock<IUserConfirmation<ApplicationUser>>();
        var servicesMock = new Mock<IServiceProvider>();
        var loggerMock = new Mock<ILogger<UserManager<ApplicationUser>>>();
        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var signInManagerLoggerMock = new Mock<ILogger<SignInManager<ApplicationUser>>>();

        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object,
            optionsMock.Object,
            passwordHasherMock.Object,
            userValidators,
            passwordValidators,
            keyNormalizerMock.Object,
            errorsMock.Object,
            servicesMock.Object,
            loggerMock.Object);

        _userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();


        _signInManagerMock = new Mock<SignInManager>(
            _userManagerMock.Object,
            contextAccessorMock.Object,
            _userClaimsPrincipalFactoryMock.Object,
            optionsMock.Object,
            signInManagerLoggerMock.Object,
            authSchemeMock.Object,
            userCofirmMock.Object);

        _authorizationServiceMock = new Mock<IAuthorizationService>();
        _mapperMock = new Mock<IMapper>();
        _currentUserMock = new Mock<IUser>();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("TestDatabase");
        _applicationDbContextMock = new Mock<ApplicationDbContext>(optionsBuilder.Options);

        _identityService = new IdentityService(
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _userClaimsPrincipalFactoryMock.Object,
            _authorizationServiceMock.Object,
            _applicationDbContextMock.Object,
            _mapperMock.Object,
            _currentUserMock.Object);
    }

    [Test]
    public async Task Logout_ShouldSignOutUser()
    {
        // Arrange

        // Act
        await _identityService.Logout();

        // Assert
        _signInManagerMock.Verify(s => s.SignOutAsync(), Times.Once);
    }

    [Test]
    public async Task GetUserNameAsync_ShouldReturnUserName()
    {
        // Arrange
        var userId = "user1";
        var user = new ApplicationUser { UserName = "testuser" };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _identityService.GetUserNameAsync(userId);

        // Assert
        result.Should().Be(user.UserName);
    }

    [Test]
    public async Task CreateUserAsync_ShouldCreateUserSuccessfully()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.Parse("2000-01-01"),
            JoinDate = DateTime.Parse("2022-01-01"),
            Role = "User"
        };
        var expectedResult = IdentityResult.Success;
        _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(expectedResult);
        _userManagerMock.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        // Act
        var (result, userId) = await _identityService.CreateUserAsync(createUserDto);

        // Assert
        result.Succeeded.Should().BeTrue();
        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    }


    [Test]
    public async Task GetUserWithRoleAsync_ShouldReturnUserDto()
    {
        // Arrange
        var userId = "user1";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        var roles = new List<string> { "Admin" };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(roles);
        var userDto = new UserDto { Id = userId, Username = "testuser", Type = "Admin" };
        _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = await _identityService.GetUserWithRoleAsync(userId);

        // Assert
        userDto.Should().NotBeNull();
        userDto.Should().BeEquivalentTo(result);

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(u => u.GetRolesAsync(It.IsAny<ApplicationUser>()), Times.Once);
        _mapperMock.Verify(m => m.Map<UserDto>(It.IsAny<ApplicationUser>()), Times.Once);
    }

    [Test]
    public async Task UpdateUserAsync_ShouldUpdateUserSuccessfully_WhenUserExist()
    {
        // Arrange
        var userDto = new UserDto { Id = "user1", Username = "testuser" };
        var user = new ApplicationUser { Id = "user1", UserName = "testuser" };

        _userManagerMock.Setup(u => u.FindByIdAsync(userDto.Id)).ReturnsAsync(user);
        _mapperMock.Setup(m => m.Map(userDto, user)).Returns(user);
        _userManagerMock.Setup(u => u.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _identityService.UpdateUserAsync(userDto);

        // Assert
        result.Succeeded.Should().BeTrue();

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<UserDto>(), It.IsAny<ApplicationUser>()), Times.Once);
        _userManagerMock.Verify(u => u.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Once);
    }

    [Test]
    public async Task UpdateUserAsync_ShouldThrowNotFoundException_WhenUserNotExist()
    {
        // Arrange
        var userDto = new UserDto { Id = "user1", Username = "testuser" };

        _userManagerMock.Setup(u => u.FindByIdAsync(userDto.Id)).ReturnsAsync((ApplicationUser?)null);

        // Act
        var act = async () => await _identityService.UpdateUserAsync(userDto);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<UserDto>(), It.IsAny<ApplicationUser>()), Times.Never);
        _userManagerMock.Verify(u => u.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Never);
    }

    [Test]
    public async Task UpdateUserToRoleAsync_ShouldUpdateUserRoleSuccessfully()
    {
        // Arrange
        var userId = "user1";
        var currentRole = "User";
        var newRole = "Admin";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(u => u.RemoveFromRoleAsync(user, currentRole)).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(u => u.AddToRoleAsync(user, newRole)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _identityService.UpdateUserToRoleAsync(userId, currentRole, newRole);

        // Assert
        result.Succeeded.Should().BeTrue();

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(u => u.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);

    }

    [Test]
    public async Task UpdateUserToRoleAsync_ShouldThrowNotFoundException_WhenUserNotExist()
    {
        // Arrange
        var userId = "user1";
        var currentRole = "Staff";
        var newRole = "Admin";
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser?)null);

        // Act
        var act = async () => await _identityService.UpdateUserToRoleAsync(userId, currentRole, newRole);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(u => u.RemoveFromRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
        _userManagerMock.Verify(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task IsInRoleAsync_ShouldReturnTrueIfUserIsInRole()
    {
        // Arrange
        var userId = "user1";
        var role = "Admin";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(u => u.IsInRoleAsync(user, role)).ReturnsAsync(true);

        // Act
        var result = await _identityService.IsInRoleAsync(userId, role);

        // Assert
        result.Should().BeTrue();

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task IsInRoleAsync_ShouldReturnFalse_WhenUserNotExist()
    {
        // Arrange
        var userId = "user1";
        var role = "Admin";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser?)null);
        _userManagerMock.Setup(u => u.IsInRoleAsync(user, role)).ReturnsAsync(true);

        // Act
        var result = await _identityService.IsInRoleAsync(userId, role);

        // Assert
        result.Should().BeFalse();

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task IsInRoleAsync_ShouldReturnFalse_WhenUserNotInRole()
    {
        // Arrange
        var userId = "user1";
        var role = "Admin";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(u => u.IsInRoleAsync(user, role)).ReturnsAsync(false);
        // Act
        var result = await _identityService.IsInRoleAsync(userId, role);

        // Assert
        result.Should().BeFalse();

        _userManagerMock.Verify(u => u.FindByIdAsync(It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(u => u.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
    }

    [Ignore("asd")]
    [Test]
    public async Task AuthorizeAsync_ShouldReturnTrueIfAuthorized()
    {
        // Arrange
        var userId = "user1";
        var policyName = "TestPolicy";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        var principal = new ClaimsPrincipal();
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        _userClaimsPrincipalFactoryMock.Setup(f => f.CreateAsync(user)).ReturnsAsync(principal);
        _authorizationServiceMock.Setup(a => a.AuthorizeAsync(principal, policyName)).ReturnsAsync(AuthorizationResult.Success);

        // Act
        var result = await _identityService.AuthorizeAsync(userId, policyName);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task DeleteUserAsync_ShouldDeleteUserSuccessfully()
    {
        // Arrange
        var userId = "user1";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _identityService.DeleteUserAsync(userId);

        // Assert
        result.Succeeded.Should().BeTrue();
    }

    [Test]
    public async Task CheckCurrentPassword_ShouldThrowExceptionIfPasswordIsIncorrect()
    {
        // Arrange
        var currentPassword = "wrongpassword";
        var userId = "user1";
        var user = new ApplicationUser { Id = userId, UserName = "testuser" };
        _currentUserMock.Setup(c => c.Id).Returns(userId);
        _userManagerMock.Setup(u => u.FindByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(u => u.CheckPasswordAsync(user, currentPassword)).ReturnsAsync(false);

        // Act
        var act = async () => await _identityService.CheckCurrentPassword(currentPassword);

        // Act & Assert
        await act.Should().ThrowAsync<IncorrectPasswordException>();
    }
}