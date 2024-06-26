using AssetManagement.Application.Assets.Queries.GetDetailedAssets;
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ardalis.GuardClauses;
using AssetManagement.Application.UnitTests.Helpers;

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAsset
{
    [TestFixture]
    public class GetAssetByIdQueryHandlerTests
    {
        private GetAssetByIdQueryHandler _handler;
        private Mock<IApplicationDbContext> _contextMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssetDto.Mapping());
            });
            _mapper = mockMapper.CreateMapper();

            _handler = new GetAssetByIdQueryHandler(_contextMock.Object, _mapper);
        }

        [Test]
        [Ignore("Skipping this test temporarily")]
        // [Explicit] // Use this if you want to run the test only explicitly
        public async Task Handle_ShouldReturnAssetDto_WhenAssetExists()
        {
            var assetId = 1;
            var asset = new Asset
            {
                Id = assetId,
                Code = "ASSET-0001",
                Name = "Laptop",
                Location = "Office",
                Specification = "HP EliteBook 840 G7",
                InstalledDate = DateTime.UtcNow,
                Category = new Category { Id = 1, Name = "Laptop", Code = "LAPTOP" },
                AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
            };

            var assets = new List<Asset> { asset }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Asset>>();
            mockDbSet.As<IAsyncEnumerable<Asset>>()
                .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<Asset>(assets.GetEnumerator()));
            mockDbSet.As<IQueryable<Asset>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Asset>(assets.Provider));
            mockDbSet.As<IQueryable<Asset>>().Setup(m => m.Expression).Returns(assets.Expression);
            mockDbSet.As<IQueryable<Asset>>().Setup(m => m.ElementType).Returns(assets.ElementType);
            mockDbSet.As<IQueryable<Asset>>().Setup(m => m.GetEnumerator()).Returns(assets.GetEnumerator());

            _contextMock.Setup(c => c.Assets).Returns(mockDbSet.Object);

            // Act
            var result = await _handler.Handle(new GetAssetByIdQuery(assetId), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(asset.Id));
            Assert.That(result.Code, Is.EqualTo(asset.Code));
            Assert.That(result.Name, Is.EqualTo(asset.Name));
            Assert.That(result.Location, Is.EqualTo(asset.Location));
            Assert.That(result.Specification, Is.EqualTo(asset.Specification));
            Assert.That(result.InstalledDate, Is.EqualTo(asset.InstalledDate));
            Assert.That(result.CategoryName, Is.EqualTo(asset.Category.Name));
            Assert.That(result.AssetStatusName, Is.EqualTo(asset.AssetStatus.Name));
        }
    }
}
