using System.Security.Claims;

using AssetManagement.Infrastructure.Identity;

using FluentAssertions;

using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Infrastructure.UnitTests.Identity;

public class SignInManagerTests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<IHttpContextAccessor> _contextAccessorMock;
    private Mock<IUserClaimsPrincipalFactory<ApplicationUser>> _claimsFactoryMock;
    private Mock<IOptions<IdentityOptions>> _optionsAccessorMock;
    private Mock<ILogger<SignInManager<ApplicationUser>>> _loggerMock;
    private Mock<IAuthenticationSchemeProvider> _schemesMock;
    private Mock<IUserConfirmation<ApplicationUser>> _confirmationMock;
    private SignInManager _signInManager;

    [SetUp]
    public void SetUp()
    {
        _userManagerMock = MockUserManager();
        _contextAccessorMock = new Mock<IHttpContextAccessor>();
        _claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
        _loggerMock = new Mock<ILogger<SignInManager<ApplicationUser>>>();
        _schemesMock = new Mock<IAuthenticationSchemeProvider>();
        _confirmationMock = new Mock<IUserConfirmation<ApplicationUser>>();

        _signInManager = new SignInManager(
            _userManagerMock.Object,
            _contextAccessorMock.Object,
            _claimsFactoryMock.Object,
            _optionsAccessorMock.Object,
            _loggerMock.Object,
            _schemesMock.Object,
            _confirmationMock.Object
        );
    }

    [Test]
    public async Task PasswordSignInAsync_WithInvalidUsername_ReturnsFailed()
    {
        // Arrange
        var userName = "invalidUser";
        _userManagerMock.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _signInManager.PasswordSignInAsync(userName, "password", false, false);

        // Assert
        result.Should().Be(SignInResult.Failed);
    }

    [Test]
    public async Task PasswordSignInAsync_WithValidUserAndInvalidPassword_ReturnsFailed()
    {
        // Arrange
        var user = new ApplicationUser { UserName = "validUser" };
        _userManagerMock.Setup(um => um.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await _signInManager.PasswordSignInAsync(user.UserName, "password", false, false);

        // Assert
        Assert.IsFalse(result.Succeeded);
    }

    [Test]
    public async Task PasswordSignInAsync_WithDeletedUser_ReturnsLockedOut()
    {
        // Arrange
        var user = new ApplicationUser { UserName = "validUser", IsDelete = true };
        _userManagerMock.Setup(um => um.CheckPasswordAsync(user, It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _signInManager.PasswordSignInAsync(user, "password", false, false);

        // Assert
        result.Should().Be(SignInResult.LockedOut);
    }

    public static Mock<UserManager<ApplicationUser>> MockUserManager()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        var optionsMock = new Mock<IOptions<IdentityOptions>>();
        var passwordHasherMock = new Mock<IPasswordHasher<ApplicationUser>>();
        var userValidators = new List<IUserValidator<ApplicationUser>> { new Mock<IUserValidator<ApplicationUser>>().Object };
        var passwordValidators = new List<IPasswordValidator<ApplicationUser>> { new Mock<IPasswordValidator<ApplicationUser>>().Object };
        var keyNormalizerMock = new Mock<ILookupNormalizer>();
        var errorsMock = new Mock<IdentityErrorDescriber>();
        var servicesMock = new Mock<IServiceProvider>();
        var loggerMock = new Mock<ILogger<UserManager<ApplicationUser>>>();

        return new Mock<UserManager<ApplicationUser>>(
        userStoreMock.Object,
        optionsMock.Object,
        passwordHasherMock.Object,
        userValidators,
        passwordValidators,
        keyNormalizerMock.Object,
        errorsMock.Object,
        servicesMock.Object,
        loggerMock.Object);
    }
}