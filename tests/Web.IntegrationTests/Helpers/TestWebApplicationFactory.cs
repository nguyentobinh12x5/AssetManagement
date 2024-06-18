using System.Data.Common;

using AssetManagement.Application.Auth.Queries.GetCurrentUserInfo;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Infrastructure.Data;
using AssetManagement.Infrastructure.Identity;

using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Web.IntegrationTests.Mocks;

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

            // // Add test authentication handler
            // services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = "TestScheme";
            //     options.DefaultChallengeScheme = "TestScheme";
            // }).AddScheme<TestAuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });
        });

        return base.CreateHost(builder);
    }


}