using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AssetManagement.Application.Assignments.Queries.GetMyAssignments;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;
using AssetManagement.Domain.Enums;

using AutoMapper;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assignments.Queries.GetMyAssignments
{
    [TestFixture]
    public class GetMyAssignmentsTests
    {
        private GetMyAssignmentsQueryHandler _handler;
        private Mock<IApplicationDbContext> _contextMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUser> _currentUserMock;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _mapperMock = new Mock<IMapper>();
            _currentUserMock = new Mock<IUser>();

            _handler = new GetMyAssignmentsQueryHandler(_contextMock.Object, _mapperMock.Object, _currentUserMock.Object);
        }


        [Test]
        public async Task Handle_ShouldReturnEmptyPaginatedList_WhenNoAssignmentsExist()
        {
            // Arrange
            var mockSet = new List<Assignment>().AsQueryable().BuildMockDbSet();
            _contextMock.Setup(m => m.Assignments).Returns(mockSet.Object);

            _currentUserMock.Setup(u => u.UserName).Returns("Admin");

            var request = new GetMyAssignmentsQuery("AssignedDate", 1, 10, "asc");

            _mapperMock.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MyAssignmentDto.Mapping());
            }));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count(), Is.EqualTo(0));
        }
    }
}