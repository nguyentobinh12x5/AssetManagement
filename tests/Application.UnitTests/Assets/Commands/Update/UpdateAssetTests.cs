using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Commands.Update;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Commands.Update
{
    [TestFixture]
    public class UpdateAssetCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _contextMock;
        private Mock<IUser> _currentUserMock;
        private UpdateAssetCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _currentUserMock = new Mock<IUser>();
            _handler = new UpdateAssetCommandHandler(_contextMock.Object, _currentUserMock.Object);
        }

        [Test]
        public async Task Handle_ShouldUpdateAsset_WhenAssetExists()
        {
            var asset = new Asset
            {
                Id = 1,
                Name = "Macbook M2",
                Specification = "MacOS",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
            };

            var state = new AssetStatus { Id = 2, Name = "Available" };

            var mockAssetSet = new List<Asset> { asset }
                .AsQueryable()
                .BuildMockDbSet();

            var mockStateSet = new List<AssetStatus> { state }
                .AsQueryable()
                .BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);
            _contextMock.Setup(m => m.AssetStatuses).Returns(mockStateSet.Object);

            var command = new UpdateAssetCommand
            {
                Id = asset.Id,
                Name = "Macbook M1",
                Specification = "MacOS",
                InstalledDate = DateTime.UtcNow.AddDays(-7),
                State = "Available"
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().Be(asset.Id);
            asset.Name.Should().Be(command.Name);
            asset.Specification.Should().Be(command.Specification);
            asset.InstalledDate.Should().Be(command.InstalledDate);
            asset.AssetStatus.Name.Should().Be(command.State);

            _contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenAssetNotExists()
        {
            var mockAssetSet = new List<Asset>()
                .AsQueryable()
                .BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);

            var command = new UpdateAssetCommand
            {
                Id = 1,
                Name = "Macbook",
                Specification = "MacOS",
                InstalledDate = DateTime.UtcNow.AddDays(-1),
                State = "Available"
            };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenStateNotExists()
        {
            var asset = new Asset
            {
                Id = 1,
                Name = "Macbook",
                Specification = "MacOS",
                InstalledDate = DateTime.UtcNow,
                AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
            };

            var mockAssetSet = new List<Asset> { asset }
                .AsQueryable()
                .BuildMockDbSet();

            var mockStateSet = new List<AssetStatus>()
                .AsQueryable()
                .BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);
            _contextMock.Setup(m => m.AssetStatuses).Returns(mockStateSet.Object);

            var command = new UpdateAssetCommand
            {
                Id = asset.Id,
                Name = "Macbook M3",
                Specification = "MacOS",
                InstalledDate = DateTime.UtcNow.AddDays(-1),
                State = "Available"
            };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}