using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AssetManagement.Domain.Entities;
using AssetManagement.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Data
{
    public static class AssetDataHelper
    {
        private static readonly List<Category> Categories = new()
        {
            new Category { Id = 1, Name = "Laptop", Code = "LA001" }
        };

        private static readonly List<AssetStatus> AssetStatuses = new()
        {
            new AssetStatus { Id = 1, Name = "Available" }
        };

        private static readonly List<Asset> Assets = new()
        {
            new Asset
            {
                Id = 1,
                Code = "LA100001",
                Name = "Laptop Probook 450 G1",
                Location = "HCM",
                Specification = "Core i5, 8GB RAM, 750 GB",
                InstalledDate = new DateTime(2021, 1, 16),
                Category = Categories[0],
                AssetStatus = AssetStatuses[0]
            }
        };

        public static async Task CreateSampleData(TestWebApplicationFactory<Program> factory)
        {
            using (var scope = factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (db != null)
                {
                    db.Categories.AddRange(Categories);
                    db.AssetStatuses.AddRange(AssetStatuses);
                    db.Assets.AddRange(Assets);
                    await db.SaveChangesAsync();
                }
            }
        }

    }
}
