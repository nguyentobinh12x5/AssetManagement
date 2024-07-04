using System.Data.Common;

using AssetManagement.Infrastructure.Data;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Web.IntegrationTests.Data;

namespace Web.IntegrationTests.Helpers;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ApplicationDbContext>));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));

            if (dbConnectionDescriptor != null)
            {
                services.Remove(dbConnectionDescriptor);
            }

            // // Remove the existing UserManager registration
            // var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(UserManager<ApplicationUser>));
            // if (descriptor != null)
            // {
            //     services.Remove(descriptor);
            // }

            // // Add the custom mock UserManager
            // services.AddSingleton<UserManager<ApplicationUser>, MockUserManager>();

            // // Add a mock IIdentityService
            // services.AddSingleton<IIdentityService, MockIdentityService>();

            // Add test authentication handler
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "TestScheme";
                options.DefaultChallengeScheme = "TestScheme";
            }).AddScheme<TestAuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });

            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase(databaseName: "AssetManagement_Integration_Tests");
            });
        });


        return base.CreateHost(builder);
    }
    public void ResetDatabase()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.SaveChanges();
    }
}