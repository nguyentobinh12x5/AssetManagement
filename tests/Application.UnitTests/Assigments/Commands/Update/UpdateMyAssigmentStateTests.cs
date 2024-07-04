using AssetManagement.Application.Assignments.Commands.Update;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using FluentAssertions;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assigments.Commands.Update;

[TestFixture]
public class UpdateMyAssigmentStateTests
{
    private Mock<IApplicationDbContext> _contextMock;
    private UpdateMyAssignmentStateCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _handler = new UpdateMyAssignmentStateCommandHandler(_contextMock.Object);
    }

    [Test]
    public async Task Handle_ShouldUpdateMyAssignmentStateWithAccepted_WhenAssignmentExists()
    {
        var assignment = new Assignment
        {
            Id = 1,
            State = AssignmentState.WaitingForAcceptance,
            Asset = new Asset { Id = 1, Code = "A1" },
        };


        var assignedStatus = new AssetStatus { Id = 1, Name = "Assigned" };

        var mockAssignmentSet = new List<Assignment> { assignment }
            .AsQueryable()
            .BuildMockDbSet();
        var mockAssetSet = new List<Asset> { assignment.Asset }
            .AsQueryable()
            .BuildMockDbSet();
        var mockAssetStatusSet = new List<AssetStatus> { assignedStatus }
            .AsQueryable()
            .BuildMockDbSet();

        _contextMock.Setup(m => m.Assignments).Returns(mockAssignmentSet.Object);
        _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);
        _contextMock.Setup(m => m.AssetStatuses).Returns(mockAssetStatusSet.Object);

        var command = new UpdateMyAssignmentStateCommand()
        {
            Id = assignment.Id,
            State = AssignmentState.Accepted
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(assignment.Id);
        assignment.State.Should().Be(AssignmentState.Accepted);
        assignment.Asset.AssetStatus.Should().Be(assignedStatus);

        _contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);

    }

    [Test]
    public async Task Handle_ShouldUpdateMyAssignmentStateWithDeclined_WhenAssignmentExists()
    {
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

        var command = new UpdateMyAssignmentStateCommand()
        {
            Id = assignment.Id,
            State = AssignmentState.Declined
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(assignment.Id);
        assignment.State.Should().Be(AssignmentState.Declined);
        assignment.Asset.AssetStatus.Should().Be(availableStatus);

        _contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);

    }

    [Test]
    public void Handle_ShouldThrowNotFoundException_WhenAssignmentNotExist()
    {
        var mockAssignmentSet = new List<Assignment>()
            .AsQueryable()
            .BuildMockDbSet();

        _contextMock.Setup(m => m.Assignments).Returns(mockAssignmentSet.Object);

        var command = new UpdateMyAssignmentStateCommand()
        {
            Id = 1,
            State = AssignmentState.WaitingForAcceptance
        };

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Test]
    public void Handle_ShouldThrowArgumentException_WhenStateIsInvalid()
    {
        // Arrange
        var invalidState = (AssignmentState)999;
        var command = new UpdateMyAssignmentStateCommand()
        {
            Id = 1,
            State = invalidState
        };

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<ArgumentException>();
    }


}