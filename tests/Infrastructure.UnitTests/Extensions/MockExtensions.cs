<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;

using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace AssetManagement.Infrastructure.UnitTests.Extensions;
public static class MockExtensions
{
    // Example extension method to mock IQueryable<T>
    public static IQueryable<T> BuildMock<T>(this IEnumerable<T> enumerable) where T : class
    {
        return enumerable.AsQueryable();
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
=======
using System.Collections.Generic;
using System.Linq;

using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace AssetManagement.Infrastructure.UnitTests.Extensions;
public static class MockExtensions
{
    // Example extension method to mock IQueryable<T>
    public static IQueryable<T> BuildMock<T>(this IEnumerable<T> enumerable) where T : class
    {
        return enumerable.AsQueryable();
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
>>>>>>> 6fef882eb1e586771288481f42acbea2ec0231f2
}