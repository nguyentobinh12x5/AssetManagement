

using System.Net;
using System.Net.Http.Json;

using AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;
using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.ChangePassword.Commands.UpdatePassword;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

using Web.IntegrationTests.Data;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

using Assert = Xunit.Assert;

namespace Web.IntegrationTests.Endpoints;

[Collection("Sequential"), TestCaseOrderer("TestOrderExamples.TestCaseOrdering.TestPriorityOrderer", "TestOrderExamples")]
public class AuthTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public AuthTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.GetApplicationHttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("TestScheme");
    }

    [Fact, TestPriority(1)]
    public async Task Login_ShouldReturnEmpty_WhenValidLoginRequest()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "administrator@localhost",
            Password = "Administrator1!"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Empty(content);
    }

    [Fact, TestPriority(2)]
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

    [Fact, TestPriority(3)]
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

    [Fact, TestPriority(4)]
    public async Task GetUserInfo_ShouldReturnUserInfo()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "administrator@localhost",
            Password = "Administrator1!"
        };
        await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);

        // Act
        var response = await _httpClient.GetAsync("/api/auth/manage/info");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var userInfo = await response.Content.ReadFromJsonAsync<UserInfoDto>();
        Assert.NotNull(userInfo);
    }

    [Fact, TestPriority(5)]
    public async Task ChangePasswordFirstTime_ShouldReturnBadRequest_OnInputOldPassword()
    {
        // Arrange
        var command = new ChangePasswordFirstTimeCommand("Administrator1!");
        var loginRequest = new LoginRequest
        {
            Email = "administrator@localhost",
            Password = "Administrator1!"
        };
        await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);

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
        var command = new ChangePasswordFirstTimeCommand("NewPassword123!");
        var resetPasswordCommand = new ChangePasswordFirstTimeCommand("Administrator1!");

        var loginRequest = new LoginRequest
        {
            Email = "administrator@localhost",
            Password = "Administrator1!"
        };
        await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password-first-time", command);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        await _httpClient.PostAsJsonAsync("/api/auth/change-password-first-time", resetPasswordCommand);
    }


    [Fact, TestPriority(6)]
    public async Task ChangePassword_ShouldReturnNoContent_OnSuccessfulChange()
    {
        // Arrange
        var currentPassword = "Administrator1!";
        var newPassword = "NewPassword123!";

        var updatePasswordCommand = new UpdatePasswordCommand(currentPassword, newPassword);

        var loginRequest = new LoginRequest
        {
            Email = "administrator@localhost",
            Password = currentPassword
        };
        await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);

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
        var updatePasswordCommand = new UpdatePasswordCommand("Administrator1!", "NewPassword123!");

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password", updatePasswordCommand);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnBadRequest_WhenIncorrectPassword()
    {
        // Arrange
        var currentPassword = "IncorrectPassword";
        var newPassword = "NewPassword123!";

        var updatePasswordCommand = new UpdatePasswordCommand(currentPassword, newPassword);

        var loginRequest = new LoginRequest
        {
            Email = "administrator@localhost",
            Password = "Administrator1!"
        };
        await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/auth/change-password", updatePasswordCommand);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetails);
        Assert.Equal("Password is incorrect", problemDetails.Detail);
    }
}