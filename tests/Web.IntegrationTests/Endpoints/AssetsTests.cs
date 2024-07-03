using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using AssetManagement.Application.Assets.Commands.Update;
using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Application.Common.Models;

using Microsoft.AspNetCore.Identity.Data;

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

    [Fact(Skip = "Smoke test")]
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