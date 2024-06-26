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

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAssetCategories
{
    [TestFixture]
    public class GetAssetCategoryHandlerTests
    {
        private Mock<IApplicationDbContext> _mockContext;
        private GetAssetCategoryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            // Mock data
            var categories = new List<Category>
            {
                new Category { Name = "Category1" },
                new Category { Name = "Category2" }
            }.AsQueryable();

            // Mock DbSet
            var mockDbSet = new Mock<DbSet<Category>>();
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Category>(categories.Provider));
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

            // Mock DbContext
            _mockContext = new Mock<IApplicationDbContext>();
            _mockContext.Setup(c => c.Categories).Returns(mockDbSet.Object);

            // Handler instance
            _handler = new GetAssetCategoryHandler(_mockContext.Object);
        }

        [Test]
        public async Task Handle_ReturnsListOfCategoryNames()
        {
            // Act
            var result = await _handler.Handle(new Application.Assets.Queries.GetAsset.GetAssetCategories(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Does.Contain("Category1"));
            Assert.That(result, Does.Contain("Category2"));
        }

        [Test]
        public async Task Handle_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            var emptyCategories = new List<Category>().AsQueryable();
            var mockDbSet = new Mock<DbSet<Category>>();
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Category>(emptyCategories.Provider));
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(emptyCategories.Expression);
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(emptyCategories.ElementType);
            mockDbSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(emptyCategories.GetEnumerator());

            _mockContext.Setup(c => c.Categories).Returns(mockDbSet.Object);

            // Act
            var result = await _handler.Handle(new Application.Assets.Queries.GetAsset.GetAssetCategories(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Handle_ExceptionInDatabaseQuery_ThrowsException()
        {
            // Arrange
            _mockContext.Setup(c => c.Categories).Throws(new Exception("Simulated database exception"));

            // Act + Assert
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await _handler.Handle(new Application.Assets.Queries.GetAsset.GetAssetCategories(), CancellationToken.None);
            });
        }
    }
}