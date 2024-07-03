using System.Data.Common;

using AssetManagement.Infrastructure.Data;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web.IntegrationTests.Helpers;

public class TestWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    public string TestUserId { get; set; } = UsersDataHelper.TestUserId;

    public string TestUserName { get; set; } = UsersDataHelper.TestUserName;

    public string TestUserLocation { get; set; } = UsersDataHelper.TestLocation;

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

            // Remove existing authentication services
            var authServiceDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(Microsoft.AspNetCore.Authentication.IAuthenticationService));
            if (authServiceDescriptor != null)
            {
                services.Remove(authServiceDescriptor);
            }

            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase(databaseName: "AssetManagement_Integration_Tests");
            });

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
            }).AddScheme<TestAuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options =>
            {
                options.UserId = UsersDataHelper.TestUserId;
                options.UserName = TestUserName ?? String.Empty;
                options.Location = TestUserLocation ?? String.Empty;
            });
        });


        return base.CreateHost(builder);
    }
}