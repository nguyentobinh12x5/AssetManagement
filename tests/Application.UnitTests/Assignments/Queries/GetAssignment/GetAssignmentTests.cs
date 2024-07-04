using System;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.GuardClauses;

using AssetManagement.Application.Assignments.Queries.GetAssignment;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using AutoMapper;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assignments.Queries.GetAssignment
{
    [TestFixture]
    public class GetAssignmentByIdQueryHandlerTests
    {
        private GetAssignmentByIdQueryHandler _handler;
        private Mock<IApplicationDbContext> _contextMock;
        private IMapper _mapper;
        private Mock<IUser> _currentUserMock;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _currentUserMock = new Mock<IUser>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssignmentDto.Mapping());
            });
            _mapper = mockMapper.CreateMapper();

            _handler = new GetAssignmentByIdQueryHandler(_contextMock.Object, _mapper, _currentUserMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnAssignmentDto_WhenAssignmentExists()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    Id = 1,
                    AssignedDate = DateTime.UtcNow,
                    State = AssignmentState.Accepted,
                    AssignedTo = "Admin",
                    AssignedBy = "Admin",
                    Note = "Note admin pro",
                    Asset = new Asset { Id = 1, Code = "ASSET-0001", Name = "Laptop", Specification = "16Gb Ram i7" }
                }
            };

            var mockSet = assignments.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockSet.Object);

            var request = new GetAssignmentByIdQuery(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.AssetName, Is.EqualTo("Laptop"));
            Assert.That(result.Specification, Is.EqualTo("16Gb Ram i7"));
        }

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenAssignmentDoesNotExist()
        {
            // Arrange
            var mockSet = new List<Assignment>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assignments).Returns(mockSet.Object);

            var request = new GetAssignmentByIdQuery(1);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
        }
    }
}