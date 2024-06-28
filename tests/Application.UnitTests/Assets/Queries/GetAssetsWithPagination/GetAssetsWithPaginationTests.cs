using AssetManagement.Application.Assets.Queries.GetAssetsWithPagination;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Domain.Entities;

using AutoMapper;

using MockQueryable.Moq;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Queries.GetAssetsWithPagination
{
    [TestFixture]
    public class GetAssetsWithPaginationQueryHandlerTests
    {
        private GetAssetsWithPaginationQueryHandler _handler;
        private Mock<IApplicationDbContext> _contextMock;
        private Mock<IUser> _currentUserMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IApplicationDbContext>();
            _currentUserMock = new Mock<IUser>();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssetBriefDto.Mapping());
            });
            _mapper = mockMapper.CreateMapper();

            _handler = new GetAssetsWithPaginationQueryHandler(_contextMock.Object, _mapper, _currentUserMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnPaginatedList_WhenAssetsExist()
        {
            var assets = new List<Asset>
            {
                new Asset
                {
                    Id = 1,
                    Code = "ASSET-0001",
                    Name = "Laptop",
                    Location = "Office",
                    Specification = "HP EliteBook 840 G7",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 1, Name = "Laptop", Code = "LAPTOP" },
                    AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
                },
                new Asset
                {
                    Id = 2,
                    Code = "ASSET-0002",
                    Name = "Monitor",
                    Location = "Office",
                    Specification = "Dell UltraSharp",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 2, Name = "Monitor", Code = "MONITOR" },
                    AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
                }
            };

            var mockset = assets.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockset.Object);
            _currentUserMock.Setup(m => m.Location).Returns("Office");

            var request = new GetAssetsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Name",
                SortColumnDirection = "asc"
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(2));
            Assert.That(result.PageNumber, Is.EqualTo(1));
        }

        [Test]
        public async Task Handle_ShouldFilterByCategoryName_WhenCategoryNameIsProvided()
        {
            var assets = new List<Asset>
            {
                new Asset
                {
                    Id = 1,
                    Code = "ASSET-0001",
                    Name = "Laptop",
                    Location = "Office",
                    Specification = "HP EliteBook 840 G7",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 1, Name = "Laptop", Code = "LAPTOP" },
                    AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
                },
                new Asset
                {
                    Id = 2,
                    Code = "ASSET-0002",
                    Name = "Monitor",
                    Location = "Office",
                    Specification = "Dell UltraSharp",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 2, Name = "Monitor", Code = "MONITOR" },
                    AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
                }
            };

            var mockset = assets.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockset.Object);
            _currentUserMock.Setup(m => m.Location).Returns("Office");

            var request = new GetAssetsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Name",
                SortColumnDirection = "asc",
                Category = "Laptop"
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items.First().Category, Is.EqualTo("Laptop"));
        }

        [Test]
        public async Task Handle_ShouldFilterByAssetStatusName_WhenAssetStatusNameIsProvided()
        {
            var assets = new List<Asset>
            {
                new Asset
                {
                    Id = 1,
                    Code = "ASSET-0001",
                    Name = "Laptop",
                    Location = "Office",
                    Specification = "HP EliteBook 840 G7",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 1, Name = "Laptop", Code = "LAPTOP" },
                    AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
                },
                new Asset
                {
                    Id = 2,
                    Code = "ASSET-0002",
                    Name = "Monitor",
                    Location = "Office",
                    Specification = "Dell UltraSharp",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 2, Name = "Monitor", Code = "MONITOR" },
                    AssetStatus = new AssetStatus { Id = 2, Name = "In Use" }
                }
            };

            var mockset = assets.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockset.Object);
            _currentUserMock.Setup(m => m.Location).Returns("Office");

            var request = new GetAssetsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Name",
                SortColumnDirection = "asc",
                AssetStatus = "Available"
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items.First().AssetStatus, Is.EqualTo("Available"));
        }

        [Test]
        public async Task Handle_ShouldFilterBySearchTerm_WhenSearchTermIsProvided()
        {
            var assets = new List<Asset>
            {
                new Asset
                {
                    Id = 1,
                    Code = "ASSET-0001",
                    Name = "Laptop",
                    Location = "Office",
                    Specification = "HP EliteBook 840 G7",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 1, Name = "Laptop", Code = "LAPTOP" },
                    AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
                },
                new Asset
                {
                    Id = 2,
                    Code = "ASSET-0002",
                    Name = "Monitor",
                    Location = "Office",
                    Specification = "Dell UltraSharp",
                    InstalledDate = DateTime.UtcNow,
                    Category = new Category { Id = 2, Name = "Monitor", Code = "MONITOR" },
                    AssetStatus = new AssetStatus { Id = 1, Name = "Available" }
                }
            };

            var mockset = assets.AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockset.Object);
            _currentUserMock.Setup(m => m.Location).Returns("Office");

            var request = new GetAssetsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Name",
                SortColumnDirection = "asc",
                SearchTerm = "Laptop"
            };

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(1));
            Assert.That(result.Items.First().Name, Is.EqualTo("Laptop"));
        }

        [Test]
        public void Handle_ShouldReturnEmptyPaginatedList_WhenNoAssetsExist()
        {
            var mockset = new List<Asset>().AsQueryable().BuildMockDbSet();

            _contextMock.Setup(m => m.Assets).Returns(mockset.Object);
            _currentUserMock.Setup(m => m.Location).Returns("Office");

            var request = new GetAssetsWithPaginationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumnName = "Name",
                SortColumnDirection = "asc"
            };

            var result = _handler.Handle(request, CancellationToken.None).Result;

            Assert.NotNull(result);
            Assert.That(result.Items.Count, Is.EqualTo(0));
            Assert.That(result.PageNumber, Is.EqualTo(1));
        }
    }
}