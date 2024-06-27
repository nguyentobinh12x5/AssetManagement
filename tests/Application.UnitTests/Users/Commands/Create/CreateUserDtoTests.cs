using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Domain.Enums;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Commands.Create
{
    [TestFixture]
    public class CreateUserDtoTests
    {
        [Test]
        public void UserDTOs_Initialization_Default_Values()
        {
            // Arrange
            var userDto = new CreateUserDto();

            // Assert
            Assert.IsNull(userDto.FirstName);
            Assert.IsNull(userDto.LastName);
            Assert.IsNull(userDto.Location);
            Assert.That(userDto.DateOfBirth.Date, Is.EqualTo(DateTime.UtcNow.Date));
            Assert.That(userDto.Gender, Is.EqualTo(Gender.Male));
            Assert.That(userDto.JoinDate, Is.EqualTo(default(DateTime)));
            Assert.IsNull(userDto.Role);
        }

        [Test]
        public void UserDTOs_Set_Values()
        {
            // Arrange
            var joinDate = DateTime.Now.AddDays(-30);
            var userDto = new CreateUserDto
            {
                FirstName = "John",
                LastName = "Doe",
                Location = "New York",
                DateOfBirth = new DateTime(1990, 5, 15),
                Gender = Gender.Female,
                JoinDate = joinDate,
                Role = "Administrator"
            };

            // Act (none needed for property checks)

            // Assert
            Assert.That(userDto.FirstName, Is.EqualTo("John"));
            Assert.That(userDto.LastName, Is.EqualTo("Doe"));
            Assert.That(userDto.Location, Is.EqualTo("New York"));
            Assert.That(userDto.DateOfBirth, Is.EqualTo(new DateTime(1990, 5, 15)));
            Assert.That(userDto.Gender, Is.EqualTo(Gender.Female));
            Assert.That(userDto.JoinDate, Is.EqualTo(joinDate));
            Assert.That(userDto.Role, Is.EqualTo("Administrator"));
        }
    }
}