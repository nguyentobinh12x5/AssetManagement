using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Security;
using AssetManagement.Application.ReturningRequests.Queries.GetReturningRequestsWithPagination;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using AutoMapper;

using FluentAssertions;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.ReturningRequests.Queries.GetReturningRequestsWithPagination;

[TestFixture]
public class GetReturningRequestsWithPaginationQueryHandlerTests
{
    private readonly List<ReturningRequest> mockDatas = new List<ReturningRequest>
        {
            new ReturningRequest
            {
                Id = 1,
                AssignmentId = 1,
                RequestedBy = "admin",
                ReturnedDate = new DateTime(2024, 7,12),
                State = ReturningRequestState.WaitingForReturning,
                Assignment = new Assignment
                {
                    Id = 1,
                    AssignedDate = new DateTime(2023, 7, 1),
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin",
                    AssignedBy = "Admin",
                    Asset = new Asset { Id = 1, Code = "ASSET-0001", Name = "Laptop", Location = "HCM" }
                }
            },
            new ReturningRequest
            {
                Id = 2,
                AssignmentId = 2,
                RequestedBy = "staff",
                ReturnedDate = new DateTime(2024, 7,13),
                State = ReturningRequestState.Completed,
                Assignment = new Assignment
                {
                    Id = 2,
                    AssignedDate = new DateTime(2023, 7, 2),
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin2",
                    AssignedBy = "Admin2",
                    Asset = new Asset { Id = 2, Code = "ASSET-0002", Name = "Monitor", Location = "HCM" }
                }
            }
        };
    private Mock<IApplicationDbContext> _contextMock;
    private IMapper _mapperMock;
    private Mock<IUser> _userMock;
    private GetReturningRequestsWithPaginationQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _contextMock = new Mock<IApplicationDbContext>();

        var mockMapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile(new ReturningRequestBriefDto.Mapping());
           });
        _mapperMock = mockMapper.CreateMapper();
        _userMock = new Mock<IUser>();
        _handler = new GetReturningRequestsWithPaginationQueryHandler(
            _contextMock.Object,
            _mapperMock,
            _userMock.Object
        );
    }

    [Test]
    public async Task Handler_ShouldReturnPaginated()
    {
        var mockDbSet = mockDatas.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(c => c.ReturningRequests).Returns(mockDbSet.Object);
        _userMock.Setup(u => u.Location).Returns("HCM");

        var request = new GetReturningRequestsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 10,
            SortColumnName = "Id",
            SortColumnDirection = "asc"
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(mockDatas.Count);
        result.PageNumber.Should().Be(request.PageNumber);
    }

    [Test]
    public async Task Handle_ShouldFilterByState_WhenStateIsProvided()
    {
        var mockDbSet = mockDatas.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(c => c.ReturningRequests).Returns(mockDbSet.Object);
        _userMock.Setup(u => u.Location).Returns("HCM");

        var request = new GetReturningRequestsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 10,
            SortColumnName = "Id",
            SortColumnDirection = "asc",
            State = ["Completed"]
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(1);
        result.Items.Should().AllSatisfy(i => i.State.Should().Be(ReturningRequestState.Completed));
    }

    [Test]
    public async Task Handle_ShouldFilterByReturnedDate_WhenReturnedDAteIsProvided()
    {
        var mockDbSet = mockDatas.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(c => c.ReturningRequests).Returns(mockDbSet.Object);
        _userMock.Setup(u => u.Location).Returns("HCM");

        var request = new GetReturningRequestsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 10,
            SortColumnName = "Id",
            SortColumnDirection = "asc",
            ReturnedDate = "2024-07-13",
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(1);
        result.Items.Should().AllSatisfy(i => i.ReturnedDate.Should().Be(new DateTime(2024, 7, 13)));
    }

    [Test]
    [TestCase("ASSET-0001")]
    [TestCase("Monitor")]
    [TestCase("staff")]
    public async Task Handle_ShouldFilterBySearchTerm_WhenSearchTermProvided(string searchTerm)
    {
        var mockDbSet = mockDatas.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(c => c.ReturningRequests).Returns(mockDbSet.Object);
        _userMock.Setup(u => u.Location).Returns("HCM");

        var request = new GetReturningRequestsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 10,
            SortColumnName = "Id",
            SortColumnDirection = "asc",
            SearchTerm = searchTerm
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().BeGreaterThan(0);
        var returnedRequest = result.Items.First();

        switch (searchTerm)
        {
            case "ASSET-0001":
                {
                    returnedRequest.AssetCode.Should().Be("ASSET-0001");
                    break;
                }
            case "Monitor":
                {
                    returnedRequest.AssetName.Should().Be("Monitor");
                    break;
                }
            case "staff":
                {
                    returnedRequest.RequestedBy.Should().Be("staff");
                    break;
                }
            default:
                break;
        }

    }

    [Test]
    public async Task Handle_ShouldReturnEmptyPaginatedList_WhenNoRequestsExist()
    {
        var mockset = new List<ReturningRequest>().AsQueryable().BuildMockDbSet();

        _contextMock.Setup(m => m.ReturningRequests).Returns(mockset.Object);
        _userMock.Setup(u => u.Location).Returns("HCM");

        var request = new GetReturningRequestsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 10,
            SortColumnName = "Id",
            SortColumnDirection = "asc"
        };

        var result = await _handler.Handle(request, CancellationToken.None);


        result.Should().NotBeNull();
        result.Items.Count.Should().Be(0);
    }

    [Test]
    [TestCase("Id")]
    [TestCase("Assignment.Asset.Name")]
    [TestCase("Assignment.Asset.Code")]
    [TestCase("RequestedBy")]
    [TestCase("Assignment.AssignedDate")]
    [TestCase("AcceptedBy")]
    [TestCase("ReturnedDate")]
    [TestCase("State")]
    public async Task Handle_ShouldSortByColum(string sortColumn)
    {
        //Arrange

        var mockDbSet = mockDatas.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(c => c.ReturningRequests).Returns(mockDbSet.Object);
        _userMock.Setup(u => u.Location).Returns("HCM");

        IEnumerable<object> expected = sortColumn switch
        {
            "Id" => mockDatas.Select(rr => (object)rr.Id).OrderBy(id => id),
            "Assignment.Asset.Name" => mockDatas.Select(rr => rr.Assignment.Asset.Name).OrderBy(name => name),
            "Assignment.Asset.Code" => mockDatas.Select(rr => rr.Assignment.Asset.Code).OrderBy(code => code),
            "RequestedBy" => mockDatas.Select(rr => rr.RequestedBy).Order(),
            "Assignment.AssignedDate" => mockDatas.Select(rr => (object)rr.Assignment.AssignedDate).OrderBy(date => date),
            "AcceptedBy" => mockDatas.Select(rr => rr.AcceptedBy ?? String.Empty).Order(),
            "ReturnedDate" => mockDatas.Select(rr => rr.ReturnedDate ?? (object)(new DateTime(0, 1, 1))).OrderBy(date => date),
            "State" => mockDatas.Select(rr => (object)rr.State).OrderBy(state => state),
            _ => throw new ArgumentException($"Invalid sort column name: {sortColumn}")
        };

        var request = new GetReturningRequestsWithPaginationQuery
        {
            PageNumber = 1,
            PageSize = 10,
            SortColumnName = sortColumn,
            SortColumnDirection = "asc",
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);


        // Assert
        result.Should().NotBeNull();
        result.Items.Should().NotBeEmpty();

        IEnumerable<object> sortedValues = sortColumn switch
        {
            "Id" => result.Items.Select(m => (object)m.Id).OrderBy(id => id),
            "Assignment.Asset.Name" => result.Items.Select(m => m.AssetName).OrderBy(name => name),
            "Assignment.Asset.Code" => result.Items.Select(m => m.AssetCode).OrderBy(code => code),
            "RequestedBy" => result.Items.Select(m => m.RequestedBy).Order(),
            "Assignment.AssignedDate" => result.Items.Select(m => (object)m.AssignedDate).OrderBy(date => date),
            "AcceptedBy" => result.Items.Select(m => m.AcceptedBy ?? String.Empty).Order(),
            "ReturnedDate" => result.Items.Select(m => m.ReturnedDate ?? (object)(new DateTime(0, 1, 1))).OrderBy(date => date),
            "State" => result.Items.Select(m => (object)m.State).OrderBy(state => state),
            _ => throw new ArgumentException($"Invalid sort column name: {sortColumn}")
        };

        sortedValues.Should().BeEquivalentTo(expected);
    }
}