

using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Users.Queries.GetUsers;

using FluentAssertions;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Queries.GetUserTypes;

[TestFixture]
public class GetUserTypesQueryHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private GetUserTypesHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _handler = new GetUserTypesHandler(_identityServiceMock.Object);
    }

    [Test]
    public async Task Handle_UserTypesExist_ReturnsUserTypes()
    {
        // Arrange
        var query = new Application.Users.Queries.GetUsers.GetUserTypes();

        var expectedTypes = new List<string?> { "Staff", "Admin" };

        _identityServiceMock
            .Setup(s => s.GetUserTypes())
            .ReturnsAsync(expectedTypes);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedTypes);
        _identityServiceMock.Verify(s => s.GetUserTypes(), Times.Once);
    }

}