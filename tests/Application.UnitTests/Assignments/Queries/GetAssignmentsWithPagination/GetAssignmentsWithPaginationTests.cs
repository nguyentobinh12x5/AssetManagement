using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Assignments.Queries.GetAssignmentsWithPagination;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using AutoMapper;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assignments.Queries.GetAssignmentsWithPagination
{
    [TestFixture]
    public class GetAssignmentsWithPaginationQueryHandlerTests
    {
        private GetAssignmentsWithPaginationQueryHandler _handler;
        private Mock<IApplicationDbContext> _contextMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssignmentBriefDto.Mapping());
            });
            _mapper = mockMapper.CreateMapper();

            _handler = new GetAssignmentsWithPaginationQueryHandler(_contextMock.Object, _mapper);
        }

        [Test]
        public async Task Handle_ShouldReturnPaginatedList_WhenAssignmentsExist()
        {
            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    Id = 1,
                    AssignedDate = DateTime.UtcNow,
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin",
                    AssignedBy = "Admin",
                    Asset = new Asset { Id = 1, Code = "ASSET-0001", Name = "Laptop" }
                },
                new Assignment
                {
                    Id = 2,
                    AssignedDate = DateTime.UtcNow,
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin2",
                    AssignedBy = "Admin2",
                    Asset = new Asset { Id = 2, Code = "ASSET-0002", Name = "Monitor" }
                }
            };

            var mockset = assignments.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockset.Object);

            var request = new GetAssignmentsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Asset.Name",
                SortColumnDirection = "asc"
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.PageNumber, Is.EqualTo(1));
        }

        [Test]
        public async Task Handle_ShouldFilterByState_WhenStateIsProvided()
        {
            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    Id = 1,
                    AssignedDate = DateTime.UtcNow,
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin",
                    AssignedBy = "Admin",
                    Asset = new Asset { Id = 1, Code = "ASSET-0001", Name = "Laptop" }
                },
                new Assignment
                {
                    Id = 2,
                    AssignedDate = DateTime.UtcNow,
                    State = AssignmentState.WaitingForAcceptance,
                    AssignedTo = "Admin2",
                    AssignedBy = "Admin2",
                    Asset = new Asset { Id = 2, Code = "ASSET-0002", Name = "Monitor" }
                }
            };

            var mockset = assignments.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockset.Object);

            var request = new GetAssignmentsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Asset.Name",
                SortColumnDirection = "asc",
                State = new[] { "Accepted" }
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items.First().State, Is.EqualTo(AssignmentState.Accepted));
        }

        [Test]
        public async Task Handle_ShouldFilterByAssignedDate_WhenAssignedDateIsProvided()
        {
            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    Id = 1,
                    AssignedDate = new DateTime(2023, 7, 1),
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin",
                    AssignedBy = "Admin",
                    Asset = new Asset { Id = 1, Code = "ASSET-0001", Name = "Laptop" }
                },
                new Assignment
                {
                    Id = 2,
                    AssignedDate = new DateTime(2023, 7, 2),
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin2",
                    AssignedBy = "Admin2",
                    Asset = new Asset { Id = 2, Code = "ASSET-0002", Name = "Monitor" }
                }
            };

            var mockset = assignments.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockset.Object);

            var request = new GetAssignmentsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Asset.Name",
                SortColumnDirection = "asc",
                AssignedDate = "2023-07-01"
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items.First().AssignedDate, Is.EqualTo(new DateTime(2023, 7, 1)));
        }

        [Test]
        public async Task Handle_ShouldFilterBySearchTerm_WhenSearchTermIsProvided()
        {
            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    Id = 1,
                    AssignedDate = DateTime.UtcNow,
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin",
                    AssignedBy = "Admin",
                    Asset = new Asset { Id = 1, Code = "ASSET-0001", Name = "Laptop" }
                },
                new Assignment
                {
                    Id = 2,
                    AssignedDate = DateTime.UtcNow,
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin2",
                    AssignedBy = "Admin2",
                    Asset = new Asset { Id = 2, Code = "ASSET-0002", Name = "Monitor" }
                }
            };

            var mockset = assignments.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockset.Object);

            var request = new GetAssignmentsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Asset.Name",
                SortColumnDirection = "asc",
                SearchTerm = "Laptop"
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items.First().AssetName, Is.EqualTo("Laptop"));
        }

        [Test]
        public void Handle_ShouldReturnEmptyPaginatedList_WhenNoAssignmentsExist()
        {
            var mockset = new List<Assignment>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockset.Object);

            var request = new GetAssignmentsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Asset.Name",
                SortColumnDirection = "asc"
            };

            var result = _handler.Handle(request, CancellationToken.None).Result;

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(0));
            Assert.That(result.PageNumber, Is.EqualTo(1));
        }
    }
}