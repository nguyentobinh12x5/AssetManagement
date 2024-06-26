using AssetManagement.Domain.Entities;
using AssetManagement.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Data
{
    public static class AssetsDataHelper
    {
        private static readonly List<Category> Categories = new()
        {
            new Category() { Name = "Laptop", Code = "LAPTOP"},
            new Category() { Name = "Desktop", Code = "DESKTOP"},
            new Category() {  Name = "Monitor", Code = "MONITOR"}
        };

        private static readonly List<AssetStatus> AssetStatuses = new()
        {
            new AssetStatus() {  Name = "Available" },
            new AssetStatus() { Name = "Assigned" },
            new AssetStatus() {  Name = "Under Maintenance" }
        };

        private static readonly List<Asset> AssetsLists = new()
        {
            new Asset()
            {
                Code = "ASSET-00001",
                Name = "Laptop HP",
                Category = Categories[0],
                Location = "Office",
                Specification = "HP EliteBook 840 G7",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = AssetStatuses[0]
            },
            new Asset()
            {
                Code = "ASSET-00002",
                Name = "Desktop Dell",
                Category = Categories[1],
                Location = "Office",
                Specification = "Dell OptiPlex 7070",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = AssetStatuses[1]
            },
            new Asset()
            {
                Code = "ASSET-00003",
                Name = "Monitor Samsung",
                Category = Categories[2],
                Location = "Office",
                Specification = "Samsung 27\" Curved",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = AssetStatuses[2]
            }
        };

        public static async Task CreateSampleData(
            TestWebApplicationFactory<Program> factory
        )
        {
            using (var scope = factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (db != null && !db.Assets.Any())
                {
                    db.Categories.AddRange(Categories);
                    db.AssetStatuses.AddRange(AssetStatuses);
                    db.Assets.AddRange(AssetsLists);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}