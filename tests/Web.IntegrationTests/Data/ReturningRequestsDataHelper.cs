using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Data;

using Microsoft.Extensions.DependencyInjection;

using Web.IntegrationTests.Helpers;

namespace Web.IntegrationTests.Data;

public static class ReturningRequestsDataHelper
{
    public static readonly DateTime QueryDate = new DateTime(2024, 7, 24);
    public static readonly ReturningRequestState QueryState = ReturningRequestState.WaitingForReturning;
    public static readonly string SearchByAssetCode = AssignmentsDataHelper.AssetsLists[1].Code;
    public static readonly string SearchByAssetName = AssignmentsDataHelper.AssetsLists[2].Name;
    public static readonly string SearchByRequesterUsername = UsersDataHelper.TestUserName;



    public static readonly List<ReturningRequest> ReturningRequests = new List<ReturningRequest>()
    {
        new ReturningRequest()
        {
            ReturnedDate= QueryDate,
            AssignmentId = 1,
            RequestedBy = UsersDataHelper.TestUserName,
            State = QueryState,
        },
        new ReturningRequest()
        {
            ReturnedDate= QueryDate,
            AssignmentId = 2,
            RequestedBy = UsersDataHelper.TestUserName,
            State = QueryState,
        },
        new ReturningRequest()
        {
            ReturnedDate= new DateTime(2024,7,12),
            AssignmentId = 1,
            RequestedBy = "user2@test.com",
            State = ReturningRequestState.Completed,
        }
    };

    public static async Task CreateSampleData(
        TestWebApplicationFactory<Program> factory
    )
    {
        using (var scope = factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            if (db != null)
            {
                if (!db.Categories.Any())
                {
                    db.Categories.AddRange(AssignmentsDataHelper.Categories);
                }

                if (!db.AssetStatuses.Any())
                {
                    db.AssetStatuses.AddRange(AssignmentsDataHelper.AssetStatuses);
                }

                if (!db.Assets.Any())
                {
                    db.Assets.AddRange(AssignmentsDataHelper.AssetsLists);
                }

                if (!db.Assignments.Any())
                {
                    db.Assignments.AddRange(AssignmentsDataHelper.AssignmentLists);
                }

                if (!db.ReturningRequests.Any())
                {
                    db.ReturningRequests.AddRange(ReturningRequests);
                }

                await db.SaveChangesAsync();
            }
        }
    }
}