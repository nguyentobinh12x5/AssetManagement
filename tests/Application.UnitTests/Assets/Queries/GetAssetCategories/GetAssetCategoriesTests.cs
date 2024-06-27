using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAssetCategories
{
    [TestFixture]
    public class GetAssetCategoryHandlerTests
    {
        private GetAssetCategoryHandler _handler;
        private Mock<IApplicationDbContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _handler = new GetAssetCategoryHandler(_contextMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnCategoryNames_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "Monitor" }
            };

            var mockset = categories.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Categories).Returns(mockset.Object);

            var request = new Application.Assets.Queries.GetAsset.GetAssetCategories();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Does.Contain("Laptop"));
            Assert.That(result, Does.Contain("Monitor"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            var mockset = new List<Category>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Categories).Returns(mockset.Object);

            var request = new Application.Assets.Queries.GetAsset.GetAssetCategories();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}