﻿using System.Net;
using System.Net.Http.Json;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Queries.GetDetailedAssets;

using Web.IntegrationTests.Data;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

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
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&CategoryName=Laptop"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Single(assets.Items);
    }
    [Fact]
    public async Task GetAssetsWithPaginationAndFilterStatus_ShouldReturnFilteredAssetsData()
    {
        //Arrange
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&AssetStatusName=Available"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Single(assets.Items);
    }
    [Fact] 
    public async Task GetAssetsWithPaginationAndSearchByName_ShouldReturnFilteredAssetsData()
    {
        //Arrange
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&SearchTerm=HP"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Single(assets.Items);
    }
    [Fact]
    public async Task GetAssetsWithPaginationAndFilterCategoryAndStatus_ShouldReturnFilteredAssetsData()
    {
        //Arrange
        await AssetsDataHelper.CreateSampleData(_factory);

        //Act
        var assets = await _httpClient
            .GetFromJsonAsync<PaginatedList<AssetBriefDto>>(
                "/api/Assets?PageNumber=1&PageSize=2&SortColumnName=Name&SortColumnDirection=Descending&CategoryName=Laptop&AssetStatusName=Available"
            );

        //Assert
        Assert.NotNull(assets);
        Assert.Single(assets.Items);
    }

    [Fact]
    public async Task GetAssetsWithPagination_ShouldReturnEmptyAssets()
    {
        //Arrange 
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
}