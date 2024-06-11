using System.Net;
using System.Net.Http.Json;
using Web.IntegrationTests.Helpers;
using Assert = Xunit.Assert;
using Web.IntegrationTests.Data;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Xunit;
using Web.IntegrationTests.Extensions;

namespace Web.IntegrationTests.Endpoints;

[Collection("Sequential")]
public class TodoItemTests: IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public TodoItemTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.GetApplicationHttpClient();
    }

    [Fact]
    public async Task GetTodoItemsWithPagination_ShouldReturnTodoItemsData()
    {
        //Arrange
        await TodoItemsDatahelper.CreateSampleData(_factory);

        //Act
        var todoItems = await _httpClient
            .GetFromJsonAsync<PaginatedList<TodoItemBriefDto>>(
                "/api/TodoItems?ListId=1&PageNumber=1&PageSize=2&SortColumnName=Title&SortColumnDirection=Descending"
            );

        //Assert
        Assert.NotNull(todoItems);
        Assert.Equal(2, todoItems.Items.Count);
    }
}

