using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using AssetManagement.Application.Assignments.Commands.Create;
using AssetManagement.Application.Assignments.Queries.GetAssignment;
using AssetManagement.Application.Assignments.Queries.GetMyAssignments;
using AssetManagement.Application.Common.Models;
using AssetManagement.Infrastructure.Identity;

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
        _httpClient.DefaultRequestHeaders.Add("Authorization", "TestScheme admin=true");
    }

    [Fact(Skip = "For smoke tests")]
    public async Task GetMyAssignments_ShouldReturnAssignments()
    {
        // Arrange
        await AssetsDataHelper.CreateSampleData(_factory);
        await UsersDataHelper.CreateSampleData(_factory);
        await AssignmentsDataHelper.CreateSampleDataAsync(_factory);

        var loginRequest = new LoginRequest
        {
            Email = "user2@test.com",
            Password = "Password123!"
        };

        await _httpClient.PostAsJsonAsync("/api/auth/login?useCookies=true", loginRequest);
        // Act
        var assignments = await _httpClient.GetFromJsonAsync<PaginatedList<MyAssignmentDto>>(
            "/api/Assignments/me?pageNumber=1&pageSize=5&sortColumnName=Asset.Code&sortColumnDirection=Ascending"
        );

        // Assert
        Assert.NotNull(assignments);
        Assert.Equal(2, assignments.Items.Count);
    }

    [Fact(Skip = "Unusable")]
    public async Task AddAssignment_ValidCommand_ShouldReturnId()
    {

        // Assert
        await AssetsDataHelper.CreateSampleData(_factory);
        await UsersDataHelper.CreateSampleData(_factory);

        using var scope = _factory.Services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user = await userManager.FindByEmailAsync("user1@test.com");
        Assert.NotNull(user);

        var command = new CreateNewAssignmentCommand
        {
            UserId = user.Id,
            AssetId = 1,
            AssignedDate = DateTime.UtcNow,
            Note = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget"
        };

        var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        // Act
        var response = await _httpClient.PostAsync("/api/assignment", content);
        var responseUser = await response.Content.ReadFromJsonAsync<AssignmentDto>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(responseUser);

        Assert.Equal(user.UserName, responseUser.AssignedTo);
        // Assert.Equal(,responseUser.AssignedBy)

    }
}