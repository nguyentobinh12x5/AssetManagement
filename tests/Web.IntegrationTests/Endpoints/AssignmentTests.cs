using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Assignments.Commands.Create;
using AssetManagement.Application.Assignments.Commands.Update;
using AssetManagement.Application.Assignments.Queries.GetAssignment;
using AssetManagement.Application.Assignments.Queries.GetMyAssignments;
using AssetManagement.Application.Common.Models;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Identity;

using FluentValidation.Results;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Data;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

using Assert = Xunit.Assert;

namespace Web.IntegrationTests.Endpoints;

[Collection("Sequential")]
public class AssignmentTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public AssignmentTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.GetApplicationHttpClient();
    }

    [Fact]
    public async Task AddAssignment_ValidCommand_ShouldReturnId()
    {
        // Assert
        _factory.ResetDatabase();

        await AssetsDataHelper.CreateSampleData(_factory);
        await UsersDataHelper.CreateSampleData(_factory);

        using var scope = _factory.Services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync("user2@test.com");

        Assert.NotNull(user);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId};" +
            $"UserName={user.UserName}");

        var command = new CreateNewAssignmentCommand
        {
            UserId = user.Id,
            AssetId = 1,
            AssignedDate = DateTime.UtcNow.AddDays(1),
            Note = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget"
        };

        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/assignments", content);

        var assignmentId = await response.Content.ReadFromJsonAsync<int>();

        var actualResponse = await _httpClient.GetAsync($"/api/assignments/{assignmentId}");

        var actualAssignment = await actualResponse.Content.ReadFromJsonAsync<AssignmentDto>();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(actualAssignment);
        Assert.Equal(actualAssignment.AssignedTo, user.UserName);
        Assert.Equal(actualAssignment.AssignedDate.Date, DateTime.UtcNow.AddDays(1).Date);
    }

    [Fact]
    public async Task AddAssignment_InvalidAssignedDate_ShouldReturnId()
    {
        // Assert
        await AssetsDataHelper.CreateSampleData(_factory);
        await UsersDataHelper.CreateSampleData(_factory);

        using var scope = _factory.Services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync("user2@test.com");

        Assert.NotNull(user);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}" +
            $"&UserName={user.UserName}");

        var command = new CreateNewAssignmentCommand
        {
            UserId = user.Id,
            AssetId = 1,
            AssignedDate = DateTime.UtcNow.AddDays(-1),
            Note = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget"
        };

        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/assignments", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetMyAssignments_ShouldReturnAssignments()
    {
        // Arrange
        await AssetsDataHelper.CreateSampleData(_factory);
        await UsersDataHelper.CreateSampleData(_factory);
        await AssignmentsDataHelper.CreateSampleDataAsync(_factory);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId};" +
            $"UserName=user2@test.com");

        // Act
        var assignments = await _httpClient.GetFromJsonAsync<PaginatedList<MyAssignmentDto>>(
            "/api/Assignments/me?pageNumber=1&pageSize=5&sortColumnName=Asset.Code&sortColumnDirection=Ascending"
        );

        // Assert
        Assert.NotNull(assignments);
        Assert.Equal(2, assignments.Items.Count);
    }

    [Fact]
    public async Task UpdateMyAssignment_ValidCommand_ShouldReturnNoContent()
    {
        // Arrange
        await AssetsDataHelper.CreateSampleData(_factory);
        await UsersDataHelper.CreateSampleData(_factory);
        await AssignmentsDataHelper.CreateSampleDataAsync(_factory);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}" +
            $"&Location={UsersDataHelper.TestLocation}");

        // Act
        var command = new UpdateMyAssignmentStateCommand
        {
            Id = 1,
            State = AssignmentState.Accepted
        };

        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync("/api/Assignments/1", content);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        _factory.ResetDatabase();
    }

    [Fact]
    public async Task UpdateMyAssignment_InvalidId_ShouldReturnBadRequest()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssignmentsDataHelper.CreateSampleDataAsync(_factory);
        // Act
        var command = new UpdateMyAssignmentStateCommand
        {
            Id = 2,
            State = AssignmentState.Accepted
        };

        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync("/api/Assignments/1", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    [Fact]
    public async Task DeleteAssignment_ShouldReturnNoContent_WhenAssignmentExists()
    {
        // Arrange
        await AssignmentsDataHelper.CreateSampleDataAsync(_factory);
        await UsersDataHelper.CreateSampleData(_factory);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");

        var assignment = await _httpClient.GetFromJsonAsync<AssignmentDto>("/api/Assignments/1");
        Assert.NotNull(assignment);

        // Act
        var response = await _httpClient.DeleteAsync("/api/Assignments/1");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


        var getResponse = await _httpClient.GetAsync("/api/Assignments/1");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        _factory.ResetDatabase();
    }

    [Fact]
    public async Task DeleteAssignment_ShouldReturnNotFound_WhenAssignmentDoesNotExist()
    {
        // Arrange
        await AssignmentsDataHelper.CreateSampleDataAsync(_factory);
        await UsersDataHelper.CreateSampleData(_factory);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");

        // Act
        var response = await _httpClient.DeleteAsync("/api/Assignments/100");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        _factory.ResetDatabase();
    }
}