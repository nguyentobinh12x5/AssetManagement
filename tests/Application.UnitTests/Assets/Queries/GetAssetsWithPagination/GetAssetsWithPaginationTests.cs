using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.UnitTests.Helpers;
using AssetManagement.Application.UnitTests.MappingProfiles;
using AssetManagement.Domain.Entities;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Moq;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAssetsWithPagination
{
    [TestFixture]
    public class GetAssetsWithPaginationQueryHandlerTests
    {
        private Mock<IApplicationDbContext> _mockContext;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            // Mock data
            var assets = new List<Asset>
            {
                new Asset { Id = 1, Name = "Asset1", Category = new Category { Name = "Category1" }, AssetStatus = new AssetStatus { Name = "Active" } },
                new Asset { Id = 2, Name = "Asset2", Category = new Category { Name = "Category2" }, AssetStatus = new AssetStatus { Name = "Inactive" } },
                new Asset { Id = 3, Name = "Asset3", Category = new Category { Name = "Category1" }, AssetStatus = new AssetStatus { Name = "Active" } }
            }.AsQueryable();

            // Mock DbContext and IMapper
            _mockContext = new Mock<IApplicationDbContext>();
            _mockContext.Setup(c => c.Assets).Returns(DbSetMockProvider.GetDbSetMock(assets).Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AssetProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Test]
        public void Handle_ExceptionInDatabaseQuery_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(c => c.Assets).Throws(new Exception("Simulated database exception"));

            var query = new GetAssetsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Name",
                SortColumnDirection = "asc",
                CategoryName = "Category1",
                AssetStatusName = "Active",
                SearchTerm = "Asset"
            };

            var handler = new GetAssetsWithPaginationQueryHandler(_mockContext.Object, _mapper);

            // Act + Assert
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }
    }
}
