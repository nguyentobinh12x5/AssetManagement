using Ardalis.GuardClauses;

using AssetManagement.Application.Assignments.Commands.Delete;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assignments.Commands.Delete
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
            var assignment = new Assignment
            {
                Id = 1,
                State = AssignmentState.WaitingForAcceptance,
                Asset = new Asset { Id = 1, Code = "A1" },
            };

            var availableStatus = new AssetStatus { Id = 1, Name = "Available" };
            var mockAssignmentSet = new List<Assignment> { assignment }
                .AsQueryable()
                .BuildMockDbSet();
            var mockAssetSet = new List<Asset> { assignment.Asset }
                .AsQueryable()
                .BuildMockDbSet();
            var mockAssetStatusSet = new List<AssetStatus> { availableStatus }
                .AsQueryable()
                .BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockAssignmentSet.Object);
            _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);
            _contextMock.Setup(m => m.AssetStatuses).Returns(mockAssetStatusSet.Object);

            var command = new DeleteAssignmentCommand(assignment.Id);

            // Act
            await _handler.Handle(command, CancellationToken.None);
            // Assert
            _contextMock.Verify(m => m.Assignments.Remove(assignment), Times.Once);
            _contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenAssigmentDoesNotExist()
        {
            // Arrange
            var mockAssignmentSet = new List<Assignment>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(x => x.Assignments).Returns(mockAssignmentSet.Object);

            var command = new DeleteAssignmentCommand(1);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Act & Assert

            act.Should().ThrowAsync<NotFoundException>();
        }
    }
}