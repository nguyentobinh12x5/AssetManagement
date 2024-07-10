using Ardalis.GuardClauses;

using AssetManagement.Application.Assignments.Commands.Update;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;
using AssetManagement.Infrastructure.Identity;

using FluentAssertions;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assignments.Commands.Update;

[TestFixture]
public class UpdateAssignmentTests
{
    private Mock<IApplicationDbContext> _contextMock;
    private Mock<IIdentityService> _identityServiceMock;
    private Mock<IUser> _currentUserMock;
    private UpdateAssignmentCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _identityServiceMock = new Mock<IIdentityService>();
        _currentUserMock = new Mock<IUser>();
        _handler = new UpdateAssignmentCommandHandler(_contextMock.Object, _identityServiceMock.Object, _currentUserMock.Object);
    }

    [Test]
    public async Task Handle_ShouldUpdateAssignment_WhenAssignmentExists()
    {
        var assignment = new Assignment
        {
            Id = 1,
            Asset = new Asset { Id = 1, Code = "A1" },
            AssignedTo = "User1"
        };

        var user = new ApplicationUser { Id = "User1", UserName = "TestUser1" };

        var mockAssignmentSet = new List<Assignment> { assignment }
            .AsQueryable()
            .BuildMockDbSet();
        var mockAssetSet = new List<Asset> { assignment.Asset }
            .AsQueryable()
            .BuildMockDbSet();
        var mockUserSet = new List<ApplicationUser>() { user }
            .AsQueryable()
            .BuildMockDbSet();

        _contextMock.Setup(m => m.Assignments).Returns(mockAssignmentSet.Object);
        _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);
        _identityServiceMock.Setup(m => m.GetUserNameAsync("User1")).ReturnsAsync(user.UserName);
        _currentUserMock.Setup(m => m.UserName).Returns("TestUser1");

        var command = new UpdateAssignmentCommand()
        {
            Id = assignment.Id,
            AssetId = assignment.Asset.Id,
            UserId = "User1",
            AssignedDate = DateTime.UtcNow,
            Note = "Note"
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(assignment.Id);
        _identityServiceMock.Verify(m => m.GetUserNameAsync("User1"), Times.Once);
        _contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Test]
    public void Handle_ShouldThrowNotFoundException_WhenAssignmentNotFound()
    {
        var mockAssignmentSet = new List<Assignment>().AsQueryable().BuildMockDbSet();
        _contextMock.Setup(m => m.Assignments).Returns(mockAssignmentSet.Object);

        var command = new UpdateAssignmentCommand { Id = 1, AssetId = 1, UserId = "User1", Note = "Note" };

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public void Handle_ShouldThrowNotFoundException_WhenAssetNotFound()
    {
        var assignment = new Assignment
        {
            Id = 1,
            Asset = new Asset { Id = 1, Code = "A1" },
        };

        var mockAssignmentSet = new List<Assignment> { assignment }
            .AsQueryable()
            .BuildMockDbSet();
        var mockAssetSet = new List<Asset>().AsQueryable().BuildMockDbSet();

        _contextMock.Setup(m => m.Assignments).Returns(mockAssignmentSet.Object);
        _contextMock.Setup(m => m.Assets).Returns(mockAssetSet.Object);

        var command = new UpdateAssignmentCommand { Id = 1, AssetId = 1, UserId = "User1", Note = "Note" };

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

}