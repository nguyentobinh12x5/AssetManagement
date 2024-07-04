using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ardalis.GuardClauses;

using AssetManagement.Application.Assets.Commands.Delete;
using AssetManagement.Application.Assignments.Commands.Delete;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assigments.Commands.Delete
{

    [TestFixture]
    public class DeleteAssignmentCommandHandlerTests
    {
        private Mock<IApplicationDbContext> _contextMock;
        private DeleteAssignmentCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _handler = new DeleteAssignmentCommandHandler(_contextMock.Object);
        }

        [Test]
        public async Task Handle_ShouldDeleteAssignment_WhenAssetExists()
        {
            // Arrange
            var assigmentId = 1;
            var assginment = new Assignment { Id = assigmentId };
            var assginmentList = new List<Assignment> { assginment }.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(x => x.Assignments).Returns(assginmentList.Object);
            _contextMock.Setup(x => x.Assignments.FindAsync(assigmentId))
                .ReturnsAsync(assginment);
            _contextMock.Setup(x => x.Assignments.Remove(assginment));
            _contextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

            var command = new DeleteAssignmentCommand(assigmentId);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _contextMock.Verify(x => x.Assignments.Remove(assginment), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenAssigmentDoesNotExist()
        {
            // Arrange
            var assigmentId = 1;
            var assginmentList = new List<Assignment>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(x => x.Assignments).Returns(assginmentList.Object);
            _contextMock.Setup(x => x.Assignments.FindAsync(assigmentId));

            Guard.Against.NotFound(assigmentId, assginmentList);

            var command = new DeleteAssignmentCommand(assigmentId);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None));
        }
    }
}