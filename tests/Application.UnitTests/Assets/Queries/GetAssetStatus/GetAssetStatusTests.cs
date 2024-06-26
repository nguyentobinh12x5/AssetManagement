using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.UnitTests.Helpers;
using AssetManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAssetStatus
{
    [TestFixture]
    public class GetAssetStatusHandlerTests
    {
        private Mock<IApplicationDbContext> _mockContext;
        private GetAssetStatusHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Mock data
            var assetStatuses = new List<AssetStatus>
            {
                new AssetStatus { Name = "Active" },
                new AssetStatus { Name = "Inactive" }
            }.AsQueryable();

            // Mock DbSet
            var mockDbSet = new Mock<DbSet<AssetStatus>>();
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<AssetStatus>(assetStatuses.Provider));
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.Expression).Returns(assetStatuses.Expression);
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.ElementType).Returns(assetStatuses.ElementType);
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.GetEnumerator()).Returns(assetStatuses.GetEnumerator());

            // Mock DbContext
            _mockContext = new Mock<IApplicationDbContext>();
            _mockContext.Setup(c => c.AssetStatuses).Returns(mockDbSet.Object);

            // Handler instance
            _handler = new GetAssetStatusHandler(_mockContext.Object);
        }

        [Test]
        public async Task Handle_ReturnsListOfAssetStatusNames()
        {
            // Act
            var result = await _handler.Handle(new Application.Assets.Queries.GetAsset.GetAssetStatus(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Does.Contain("Active"));
            Assert.That(result, Does.Contain("Inactive"));
        }

        [Test]
        public async Task Handle_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            var emptyStatuses = new List<AssetStatus>().AsQueryable();
            var mockDbSet = new Mock<DbSet<AssetStatus>>();
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<AssetStatus>(emptyStatuses.Provider));
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.Expression).Returns(emptyStatuses.Expression);
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.ElementType).Returns(emptyStatuses.ElementType);
            mockDbSet.As<IQueryable<AssetStatus>>().Setup(m => m.GetEnumerator()).Returns(emptyStatuses.GetEnumerator());

            _mockContext.Setup(c => c.AssetStatuses).Returns(mockDbSet.Object);

            // Act
            var result = await _handler.Handle(new Application.Assets.Queries.GetAsset.GetAssetStatus(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Handle_ExceptionInDatabaseQuery_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(c => c.AssetStatuses).Throws(new Exception("Simulated database exception"));

            // Act + Assert
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _handler.Handle(new Application.Assets.Queries.GetAsset.GetAssetStatus(), CancellationToken.None);
            });
        }
    }
}