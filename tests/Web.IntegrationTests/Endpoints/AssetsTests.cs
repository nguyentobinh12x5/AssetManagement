using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using AssetManagement.Application.Assets.Commands.Create;
using AssetManagement.Application.Assets.Commands.Update;
using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Application.Common.Models;
using AssetManagement.Domain.Entities;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

using Web.IntegrationTests.Data;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

using YamlDotNet.Core.Tokens;


using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

using Assert = Xunit.Assert;

namespace Web.IntegrationTests.Endpoints;

[Collection("Sequential")]
public class AssetTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public AssetTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.GetApplicationHttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");
    }

    [Fact]
    public async Task GetAssetsWithPagination_ShouldReturnAssetsData()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Equal(2, assets.Items.Count);
    }

    [Fact]
    public async Task GetAssetsWithPaginationAndFilterCategory_ShouldReturnFilteredAssetsData()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&CategoryName=Laptop"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Equal(2, assets.Items.Count());
    }
    [Fact]
    public async Task GetAssetsWithPaginationAndFilterStatus_ShouldReturnFilteredAssetsData()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&AssetStatusName=Available"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Equal(2, assets.Items.Count());
    }

    [Fact]
    public async Task GetAssetsWithPaginationAndFilterCategoryAndStatus_ShouldReturnFilteredAssetsData()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);


        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&CategoryName=Laptop&AssetStatusName=Available"
            );

        //Assert
        Assert.NotNull(assets);
    }

    [Fact]
    public async Task GetAssetsWithPagination_ShouldReturnEmptyAssets()
    {
        //Arrange 
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=3&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Empty(assets.Items);
    }
    [Fact]
    public async Task GetAsset_ShouldReturnAssetData()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        // Act
        var asset = await _httpClient.GetFromJsonAsync<AssetDto>("/api/Assets/2");

        // Assert
        Assert.NotNull(asset);
        Assert.Equal(2, asset.Id);
        Assert.Equal("ASSET-00002", asset.Code);
        Assert.Equal("Desktop Dell", asset.Name);
    }

    [Fact]
    public async Task CreateAsset_ValidCommand_ShouldReturnAssetData()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var command = new CreateNewAssetCommand
        {
            Name = "Desktop Hp",
            Category = "Desktop",
            InstalledDate = DateTime.UtcNow,
            Specification = "Spec for asset",
            State = "Available"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Assets", command);

        var assetId = await response.Content.ReadFromJsonAsync<int>();

        var actualResponse = await _httpClient.GetAsync($"/api/Assets/{assetId}");

        var actualAsset = await actualResponse.Content.ReadFromJsonAsync<Asset>();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(actualAsset);
        Assert.Equal(actualAsset.Name, command.Name);
    }

    [Fact]
    public async Task CreateAsset_InvalidName_ShouldReturnValidationError()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var command = new CreateNewAssetCommand
        {
            Name = "",
            Category = "Desktop",
            InstalledDate = DateTime.UtcNow,
            Specification = "Spec for asset",
            State = "Available"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Assets", command);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var validationErrors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(validationErrors);
        Assert.Contains("Name", validationErrors.Errors.Keys);
    }

    [Fact]
    public async Task CreateAsset_NameTooLong_ShouldReturnValidationError()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var command = new CreateNewAssetCommand
        {
            Name = new string('a', 257),
            Category = "Desktop",
            InstalledDate = DateTime.UtcNow,
            Specification = "Spec for asset",
            State = "Available"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Assets", command);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var validationErrors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(validationErrors);
        Assert.Contains("Name", validationErrors.Errors.Keys);
    }

    [Fact]
    public async Task CreateAsset_InvalidCategory_ShouldReturnValidationError()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var command = new CreateNewAssetCommand
        {
            Name = "Desktop Hp",
            Category = "",
            InstalledDate = DateTime.UtcNow,
            Specification = "Spec for asset",
            State = "Available"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Assets", command);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var validationErrors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(validationErrors);
        Assert.Contains("Category", validationErrors.Errors.Keys);
    }

    [Fact]
    public async Task CreateAsset_SpecificationTooLong_ShouldReturnValidationError()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var command = new CreateNewAssetCommand
        {
            Name = "Desktop Hp",
            Category = "Desktop",
            InstalledDate = DateTime.UtcNow,
            Specification = new string('a', 1201),
            State = "Available"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Assets", command);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var validationErrors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(validationErrors);
        Assert.Contains("Specification", validationErrors.Errors.Keys);
    }

    [Fact]
    public async Task CreateAsset_InstallDateInFuture_ShouldReturnValidationError()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var command = new CreateNewAssetCommand
        {
            Name = "Desktop Hp",
            Category = "Desktop",
            InstalledDate = DateTime.UtcNow.AddDays(1),
            Specification = "Spec for asset",
            State = "Available"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Assets", command);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var validationErrors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(validationErrors);
        Assert.Contains("InstalledDate", validationErrors.Errors.Keys);
    }

    [Fact]
    public async Task CreateAsset_InvalidState_ShouldReturnValidationError()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var command = new CreateNewAssetCommand
        {
            Name = "Desktop Hp",
            Category = "Desktop",
            InstalledDate = DateTime.UtcNow,
            Specification = "Spec for asset",
            State = ""
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/Assets", command);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var validationErrors = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.NotNull(validationErrors);
        Assert.Contains("State", validationErrors.Errors.Keys);
    }


    [Fact]
    public async Task GetAsset_InvalidId_ShouldReturnNotFound()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        // Act
        var response = await _httpClient.GetAsync("/api/Assets/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAsset_ShouldReturnNoContent_WhenAssetExists()
    {
        // Arrange
        await AssetsDataHelper.CreateSampleData(_factory);
        await UsersDataHelper.CreateSampleData(_factory);

        var asset = await _httpClient.GetFromJsonAsync<AssetDto>("/api/Assets/3");
        Assert.NotNull(asset);

        // Act
        var response = await _httpClient.DeleteAsync("/api/Assets/3");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


        var getResponse = await _httpClient.GetAsync("/api/Assets/3");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteAsset_ShouldReturnNotFound_WhenAssetDoesNotExist()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        // Act
        var response = await _httpClient.DeleteAsync("/api/Assets/100");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAsset_ShouldReturnNoContent()
    {
        // Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        var asset = await _httpClient.GetFromJsonAsync<AssetDto>("/api/Assets/1");
        Assert.NotNull(asset);

        var updateCommand = new UpdateAssetCommand
        {
            Id = asset.Id,
            Name = "Macbook",
            Specification = "MacOS",
            InstalledDate = DateTime.UtcNow.AddDays(-5),
            State = "Available"
        };

        var response = await _httpClient.PutAsJsonAsync($"/api/Assets/{asset.Id}", updateCommand);

        // Verify update

        var updatedAsset = await _httpClient.GetFromJsonAsync<AssetDto>($"/api/Assets/{asset.Id}");
        Assert.NotNull(updatedAsset);
        Assert.Equal("Macbook", updatedAsset.Name);
        Assert.Equal("MacOS", updatedAsset.Specification);
        Assert.Equal(updateCommand.InstalledDate, updatedAsset.InstalledDate);
        Assert.Equal("Available", updatedAsset.AssetStatusName);
        Assert.Equal("Available", updatedAsset.AssetStatusName);
    }

    [Fact]
    public async Task GetAssetsWithPaginationAndSearchByName_ShouldReturnFilteredAssetsData()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&SearchTerm=Macbook"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Single(assets.Items);
    }
}