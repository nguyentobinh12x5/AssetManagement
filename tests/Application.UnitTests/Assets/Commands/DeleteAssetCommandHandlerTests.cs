using Ardalis.GuardClauses;

using AssetManagement.Application.Assets.Commands.Delete;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Commands
{
    [TestFixture]
    public class DeleteAssetCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _contextMock;
        private DeleteAssetCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _handler = new DeleteAssetCommandHandler(_contextMock.Object);
        }

        [Test]
        public async Task Handle_ShouldDeleteAsset_WhenAssetExists()
        {
            // Arrange
            var assetId = 1;
            var asset = new Asset { Id = assetId };
            var assetList = new List<Asset> { asset }.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(x => x.Assets).Returns(assetList.Object);
            _contextMock.Setup(x => x.Assets.FindAsync(assetId))
                .ReturnsAsync(asset);
            _contextMock.Setup(x => x.Assets.Remove(asset));
            _contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            var command = new DeleteAssetCommand(assetId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _contextMock.Verify(x => x.Assets.Remove(asset), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenAssetDoesNotExist()
        {
            // Arrange
            var assetId = 1;
            var assetList = new List<Asset>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(x => x.Assets).Returns(assetList.Object);
            _contextMock.Setup(x => x.Assets.FindAsync(assetId));

            Guard.Against.NotFound(assetId, assetList);

            var command = new DeleteAssetCommand(assetId);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None));
        }
    }
}