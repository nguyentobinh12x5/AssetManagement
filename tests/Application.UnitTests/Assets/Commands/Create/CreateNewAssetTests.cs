using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Assets.Commands.Create;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Commands.Create
{
    [TestFixture]
    public class CreateNewAssetCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _contextMock;
        private Mock<IUser> _currentUserMock;
        private CreateNewAssetCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _currentUserMock = new Mock<IUser>();
            _handler = new CreateNewAssetCommandHandler(_contextMock.Object, _currentUserMock.Object);
        }


        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenCategoryNotExists()
        {
            var categories = new List<Category>().AsQueryable().BuildMockDbSet();
            var mockStateSet = new List<AssetStatus>().AsQueryable().BuildMockDbSet();
            var mockAssetSet = new List<Asset>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Categories).Returns(categories.Object);
            _contextMock.Setup(m => m.AssetStatuses).Returns(mockStateSet.Object);
            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);

            var command = new CreateNewAssetCommand
            {
                Name = "Lenovo",
                Category = "Laptop",
                Specification = "16GB RAM, 512GB SSD",
                InstalledDate = DateTime.UtcNow.AddDays(-10),
                State = "Available"
            };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Laptop was not found.");
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenStateNotExists()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Laptop", Code = "LAP0001" }
            };

            var mockCategorySet = categories.AsQueryable().BuildMockDbSet();
            var mockStateSet = new List<AssetStatus>().AsQueryable().BuildMockDbSet();
            var mockAssetSet = new List<Asset>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Categories).Returns(mockCategorySet.Object);
            _contextMock.Setup(m => m.AssetStatuses).Returns(mockStateSet.Object);
            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);

            var command = new CreateNewAssetCommand
            {
                Name = "Lenovo",
                Category = "Laptop",
                Specification = "16GB RAM, 512GB SSD",
                InstalledDate = DateTime.UtcNow.AddDays(-10),
                State = "Available"
            };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<KeyNotFoundException>().WithMessage("Available was not found.");
        }
    }
}