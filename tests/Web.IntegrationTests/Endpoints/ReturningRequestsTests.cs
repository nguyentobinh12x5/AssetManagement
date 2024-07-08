using System.Net.Http.Headers;
using System.Net.Http.Json;

using AssetManagement.Application.Common.Models;
using AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;

using Web.IntegrationTests.Data;
using Web.IntegrationTests.Extensions;
using Web.IntegrationTests.Helpers;

using Xunit;

using Assert = Xunit.Assert;

namespace Web.IntegrationTests.Endpoints;

[Collection("Sequential")]
public class ReturningRequestsTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public ReturningRequestsTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.GetApplicationHttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme",
            $"UserId={UsersDataHelper.TestUserId}");
    }

    [Fact]
    public async Task GetReturningRequestsWithPagination_ShouldReturnDatas()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                "/api/ReturningRequests?PageNumber=1&PageSize=3&SortColumnName=Id&SortColumnDirection=Ascending"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.Equal(3, returnRequests.Items.Count);
    }

    [Fact]
    public async Task GetReturningRequestsWithPagination_ShouldFilter_WhenReturnedDateProvided()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                $"/api/ReturningRequests?PageNumber=1&PageSize=3&SortColumnName=Id&SortColumnDirection=Ascending&ReturnedDate={ReturningRequestsDataHelper.QueryDate}"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.Equal(2, returnRequests.Items.Count);
    }

    [Fact]
    public async Task GetReturningRequestsWithPagination_ShouldFilter_WhenStateFilterProvided()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                $"/api/ReturningRequests?PageNumber=1&PageSize=3&SortColumnName=Id&SortColumnDirection=Ascending&State={ReturningRequestsDataHelper.QueryState}"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.Equal(2, returnRequests.Items.Count);
    }

    [Fact]
    public async Task GetReturningRequestsWithPagination_ShouldFilter_WhenSearchWithAssetCodeProvided()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        var expectedAssetCode = ReturningRequestsDataHelper.SearchByAssetCode;
        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                $"/api/ReturningRequests?PageNumber=1&PageSize=3&SortColumnName=Id&SortColumnDirection=Ascending&SearchTerm={expectedAssetCode}"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.True(returnRequests.Items.All(rr => rr.AssetCode.Contains(expectedAssetCode)));
    }

    [Fact]
    public async Task GetReturningRequestsWithPagination_ShouldFilter_WhenSearchWithAssetNameProvided()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        var expectedAssetName = ReturningRequestsDataHelper.SearchByAssetName;
        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                $"/api/ReturningRequests?PageNumber=1&PageSize=3&SortColumnName=Id&SortColumnDirection=Ascending&SearchTerm={expectedAssetName}"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.True(returnRequests.Items.All(rr => rr.AssetName.Contains(expectedAssetName)));
    }

    [Fact]
    public async Task GetReturningRequestsWithPagination_ShouldFilter_WhenSearchWithRequesterProvided()
    {
        //Arrange
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        var expectedRequester = ReturningRequestsDataHelper.SearchByRequesterUsername;
        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                $"/api/ReturningRequests?PageNumber=1&PageSize=3&SortColumnName=Id&SortColumnDirection=Ascending&SearchTerm={expectedRequester}"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.True(returnRequests.Items.All(rr => rr.RequestedBy.Contains(expectedRequester)));
    }

    [Fact]
    public async Task GetReturningRequestsWithPagination_ShouldReturnEmpty()
    {
        //Arrange 
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                $"/api/ReturningRequests?PageNumber=5&PageSize=3&SortColumnName=Id&SortColumnDirection=Ascending"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.Empty(returnRequests.Items);
    }

    [Theory]
    [InlineData("Id")]
    [InlineData("Assignment.Asset.Name")]
    [InlineData("Assignment.Asset.Code")]
    [InlineData("RequestedBy")]
    [InlineData("Assignment.AssignedDate")]
    [InlineData("AcceptedBy")]
    [InlineData("ReturnedDate")]
    [InlineData("State")]
    public async Task GetReturningRequestsWithPagination_ShouldSortByAssetName(string sortColumnName)
    {
        //Arrange 
        await UsersDataHelper.CreateSampleData(_factory);
        await ReturningRequestsDataHelper.CreateSampleData(_factory);

        IEnumerable<object> expected = sortColumnName switch
        {
            "Id" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => (object)rr.Id).OrderBy(id => id),
            "Assignment.Asset.Name" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => rr.Assignment.Asset.Name).OrderBy(name => name),
            "Assignment.Asset.Code" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => rr.Assignment.Asset.Code).OrderBy(code => code),
            "RequestedBy" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => rr.RequestedBy).Order(),
            "Assignment.AssignedDate" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => (object)rr.Assignment.AssignedDate).OrderBy(date => date),
            "AcceptedBy" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => rr.AcceptedBy ?? String.Empty).Order(),
            "ReturnedDate" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => rr.ReturnedDate ?? (object)(new DateTime(0, 1, 1))).OrderBy(date => date),
            "State" => ReturningRequestsDataHelper.ReturningRequests.Select(rr => (object)rr.Assignment.State).OrderBy(state => state),
            _ => throw new ArgumentException($"Invalid sort column name: {sortColumnName}")
        };

        //Act
        var returnRequests = await _httpClient
            .GetFromJsonAsync<PaginatedList<ReturningRequestBriefDto>>(
                $"/api/ReturningRequests?PageNumber=1&PageSize=5&SortColumnName={sortColumnName}&SortColumnDirection=Ascending"
            );

        //Assert
        Assert.NotNull(returnRequests);
        Assert.NotEmpty(returnRequests.Items);
        IEnumerable<object> sortedValues = sortColumnName switch
        {
            "Id" => ReturningRequestsDataHelper.ReturningRequests.Select(m => (object)m.Id).OrderBy(id => id),
            "Assignment.Asset.Name" => ReturningRequestsDataHelper.ReturningRequests.Select(m => m.Assignment.Asset.Name).OrderBy(name => name),
            "Assignment.Asset.Code" => ReturningRequestsDataHelper.ReturningRequests.Select(m => m.Assignment.Asset.Code).OrderBy(code => code),
            "RequestedBy" => ReturningRequestsDataHelper.ReturningRequests.Select(m => m.RequestedBy).Order(),
            "Assignment.AssignedDate" => ReturningRequestsDataHelper.ReturningRequests.Select(m => (object)m.Assignment.AssignedDate).OrderBy(date => date),
            "AcceptedBy" => ReturningRequestsDataHelper.ReturningRequests.Select(m => m.AcceptedBy ?? String.Empty).Order(),
            "ReturnedDate" => ReturningRequestsDataHelper.ReturningRequests.Select(m => m.ReturnedDate ?? (object)(new DateTime(0, 1, 1))).OrderBy(date => date),
            "State" => ReturningRequestsDataHelper.ReturningRequests.Select(m => (object)m.Assignment.State).OrderBy(state => state),
            _ => throw new ArgumentException($"Invalid sort column name: {sortColumnName}")
        };

        Assert.True(sortedValues.SequenceEqual(expected));
    }
}