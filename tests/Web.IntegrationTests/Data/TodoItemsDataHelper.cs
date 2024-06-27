using AssetManagement.Domain.Entities;
using AssetManagement.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Data;

public static class TodoItemsDatahelper
{
    private static readonly List<TodoItem> ToDoItemsLists = new() {
        new TodoItem { ListId = 1, Title = "Make a todo list 📃" },
        new TodoItem { ListId = 1, Title = "Check off the first item ✅" },
        new TodoItem { ListId = 2, Title = "Realise you've already done two things on the list! 🤯"},
        new TodoItem { ListId = 2, Title = "Reward yourself with a nice, long nap 🏆" },
    };

    public static async Task CreateSampleData(
        TestWebApplicationFactory<Program> factory
    )
    {
        using (var scope = factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            if (db != null && !db.TodoItems.Any())
            {
                db.TodoItems.AddRange(ToDoItemsLists);
                await db.SaveChangesAsync();
            }
        }
    }
}