using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

using AssetManagement.Application.Auth.Commands.ChangePassword;
using AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;
using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

using Web.IntegrationTests.Data;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

using Assert = Xunit.Assert;

namespace Web.IntegrationTests.Endpoints;

[Collection("Sequential")]
public class AuthTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public AuthTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.GetApplicationHttpClient();
        //_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("TestScheme");
    }

    [Fact]
    public async Task Login_ShouldReturnEmpty_WhenValidLoginRequest()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        var loginRequest = new LoginRequest
        {
            Email = "user1@test.com",
            Password = "Password123!"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Empty(content);
    }

    [Fact]
    public async Task Login_ShouldReturnProperMessage_OnInvalidCredentials()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "administrator@localhost",
            Password = "WrongPassword"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetails);
        Assert.Equal("Username or password is incorrect. Please try again", problemDetails.Detail);
    }

    [Fact]
    public async Task Login_ShouldReturnProperMessage_OnUserDisabled()
    {
        // Arrange
        await AuthDataHelper.CreateSampleData(_factory);
        var loginRequest = new LoginRequest
        {
            Email = "disabledAdmin@localhost",
            Password = "Administrator1!"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetails);
        Assert.Equal("Your account is disabled. Please contact with IT Team", problemDetails.Detail);
    }

    [Fact]
    public async Task GetUserInfo_ShouldReturnUserInfo()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");

        // Act
        var response = await _httpClient.GetAsync("/api/auth/manage/info");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var userInfo = await response.Content.ReadFromJsonAsync<UserInfoDto>();
        Assert.NotNull(userInfo);
    }

    [Fact]
    public async Task ChangePasswordFirstTime_ShouldReturnBadRequest_OnInputOldPassword()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");

        var command = new ChangePasswordFirstTimeCommand("Password123!");

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password-first-time", command);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetails);
        Assert.Equal("New password must not the same as old one.", problemDetails.Detail);
    }

    [Fact]
    public async Task ChangePasswordFirstTime_ShouldReturnSuccessStatus_OnValidNewPassword()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
                    $"UserId={UsersDataHelper.TestUserId}");

        var command = new ChangePasswordFirstTimeCommand("NewPassword123!");
        var resetPasswordCommand = new ChangePasswordFirstTimeCommand("Administrator1!");

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password-first-time", command);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        await _httpClient.PostAsJsonAsync("/api/auth/change-password-first-time", resetPasswordCommand);

        _factory.ResetDatabase();
    }


    [Fact]
    public async Task ChangePassword_ShouldReturnNoContent_OnSuccessfulChange()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");

        var currentPassword = "Password123!";
        var newPassword = "NewPassword123!";

        var updatePasswordCommand = new UpdatePasswordCommand(currentPassword, newPassword);

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password", updatePasswordCommand);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Clean up: Resetting password back to original
        var resetPasswordCommand = new UpdatePasswordCommand(newPassword, currentPassword);
        var resetResponse = await _httpClient.PostAsJsonAsync("/api/auth/change-password", resetPasswordCommand);
        Assert.Equal(HttpStatusCode.NoContent, resetResponse.StatusCode);
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnUnauthorized_WhenNotLoggedIn()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
                    $"UserId=");

        var updatePasswordCommand = new UpdatePasswordCommand("Password123!", "NewPassword123!");

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password", updatePasswordCommand);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnBadRequest_WhenIncorrectPassword()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");

        var currentPassword = "IncorrectPassword";
        var newPassword = "NewPassword123!";

        var updatePasswordCommand = new UpdatePasswordCommand(currentPassword, newPassword);

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password", updatePasswordCommand);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetails);
        Assert.Equal("Password is incorrect", problemDetails.Detail);
    }

    [Fact]
    public async Task Logout_ShouldReturnNoContent_WhenAuthorizedUserLogout()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
                    $"UserId={UsersDataHelper.TestUserId}");

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/logout", new { });

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        // Check if cookies are removed
        var setCookieHeaders = response.Headers.GetValues("Set-Cookie").ToList();
        Assert.NotEmpty(setCookieHeaders);

        var identityCookie = setCookieHeaders.FirstOrDefault(header => header.Contains(".AspNetCore.Identity.Application"));
        Assert.NotNull(identityCookie);
        Assert.Contains("expires=", identityCookie);
        Assert.Contains(".AspNetCore.Identity.Application=;", identityCookie);

    }

    [Fact]
    public async Task Logout_ShouldReturnUnauthorized_WhenUserNotLoggedIn()
    {
        // Arrange
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
                    $"UserId=");
        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/logout", new { });

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}