using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Data
{
    public static class AssignmentsDataHelper
    {
        public static readonly List<Category> Categories = new()
        {
            new Category() { Name = "Laptop", Code = "LAPTOP"},
            new Category() { Name = "Desktop", Code = "DESKTOP"},
            new Category() {  Name = "Monitor", Code = "MONITOR"}
        };

        public static readonly List<AssetStatus> AssetStatuses = new()
        {
            new AssetStatus() {  Name = "Available" },
            new AssetStatus() { Name = "Assigned" },
            new AssetStatus() {  Name = "Under Maintenance" }
        };

        public static readonly List<Asset> AssetsLists = new()
        {
            new Asset()
            {
                Code = "ASSET-00001",
                Name = "Laptop HP",
                Category = Categories[0],
                Location = UsersDataHelper.TestLocation,
                Specification = "HP EliteBook 840 G7",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = AssetStatuses[0],
            },
            new Asset()
            {
                Code = "ASSET-00002",
                Name = "Desktop Dell",
                Category = Categories[1],
                Location = UsersDataHelper.TestLocation,
                Specification = "Dell OptiPlex 7070",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = AssetStatuses[1],
            },
            new Asset()
            {
                Code = "ASSET-00003",
                Name = "Monitor Samsung",
                Category = Categories[2],
                Location = UsersDataHelper.TestLocation,
                Specification = "Samsung 27\" Curved",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = AssetStatuses[2]
            }
        };

        public static readonly List<Assignment> AssignmentLists = new()
        {
            new Assignment()
            {
                Id = 1,
                AssignedDate = DateTime.UtcNow,
                State = AssignmentState.Accepted,
                AssignedBy = "user1@test.com",
                AssignedTo = "user2@test.com",
                Note = "Some note1",
                Asset = AssetsLists[0],

            },
            new Assignment()
            {
                Id = 2,
                AssignedDate = DateTime.UtcNow,
                State = AssignmentState.Accepted,
                AssignedBy = "user1@test.com",
                AssignedTo = "user2@test.com",
                Note = "Some note2",
                Asset = AssetsLists[1]

            },
            new Assignment()
            {
                Id = 3,
                AssignedDate = DateTime.UtcNow,
                State = AssignmentState.Accepted,
                AssignedBy = "user1@test.com",
                AssignedTo = "user2@test.com",
                Note = "Some note3",
                Asset = AssetsLists[2]
            }
        };

        public static async Task CreateSampleDataAsync(
            TestWebApplicationFactory<Program> factory
        )
        {
            using (var scope = factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (db != null && !db.Assignments.Any())
                {
                    db.Categories.AddRange(Categories);
                    db.AssetStatuses.AddRange(AssetStatuses);
                    db.Assets.AddRange(AssetsLists);
                    db.Assignments.AddRange(AssignmentLists);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}