using AssetManagement.Application.Assets.Queries.GetAsset;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAssetStatus
{
    [TestFixture]
    public class GetAssetStatusHandlerTests
    {
        private GetAssetStatusHandler _handler;
        private Mock<IApplicationDbContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _handler = new GetAssetStatusHandler(_contextMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnAssetStatusNames_WhenStatusesExist()
        {
            // Arrange
            var statuses = new List<AssetStatus>
            {
                new AssetStatus { Id = 1, Name = "Available" },
                new AssetStatus { Id = 2, Name = "In Use" }
            };

            var mockset = statuses.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.AssetStatuses).Returns(mockset.Object);

            var request = new Application.Assets.Queries.GetAsset.GetAssetStatus();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Does.Contain("Available"));
            Assert.That(result, Does.Contain("In Use"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyList_WhenNoStatusesExist()
        {
            // Arrange
            var mockset = new List<AssetStatus>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.AssetStatuses).Returns(mockset.Object);

            var request = new Application.Assets.Queries.GetAsset.GetAssetStatus();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}