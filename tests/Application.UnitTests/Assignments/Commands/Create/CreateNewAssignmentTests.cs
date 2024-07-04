using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.GuardClauses;

using AssetManagement.Application.Assignments.Commands.Create;
using AssetManagement.Application.Common.Exceptions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assignments.Commands.Create
{
    [TestFixture]
    public class CreateNewAssignmentCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _contextMock;
        private Mock<IIdentityService> _identityServiceMock;
        private Mock<IUser> _currentUserMock;
        private CreateNewAssignmentCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _identityServiceMock = new Mock<IIdentityService>();
            _currentUserMock = new Mock<IUser>();
            _handler = new CreateNewAssignmentCommandHandler(_contextMock.Object, _identityServiceMock.Object, _currentUserMock.Object);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenAssetNotFound()
        {
            var mockAssetSet = new List<Asset>().AsQueryable().BuildMockDbSet();
            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);

            var command = new CreateNewAssignmentCommand { AssetId = 1, UserId = "User1", Note = "Note" };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<NotFoundException>().WithMessage("Entity \"Asset\" (1) was not found.");
        }

        [Test]
        public void Handle_ShouldThrowBadRequestException_WhenAssetIsUnavailable()
        {
            var asset = new Asset { Id = 1, AssetStatus = new AssetStatus { Name = "Not Available" } };
            var mockAssetSet = new List<Asset> { asset }.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);

            var command = new CreateNewAssignmentCommand { AssetId = 1, UserId = "User1", Note = "Note" };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<BadRequestException>().WithMessage("asset is unavailable, please refresh the page and try again");
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenUserNotFound()
        {
            var asset = new Asset { Id = 1, AssetStatus = new AssetStatus { Name = "Available" } };
            var mockAssetSet = new List<Asset> { asset }.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);
            _identityServiceMock.Setup(m => m.GetUserNameAsync(It.IsAny<string>())).ReturnsAsync((string?)null);

            var command = new CreateNewAssignmentCommand { AssetId = 1, UserId = "User1", Note = "Note" };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            act.Should().ThrowAsync<NotFoundException>().WithMessage("Entity \"User1\" was not found.");
        }
    }
}