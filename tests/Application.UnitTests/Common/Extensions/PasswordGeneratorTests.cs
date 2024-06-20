using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AssetManagement.Application.Common.Extensions;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Common.Extensions
{
    [TestFixture]
    public class PasswordGeneratorTests
    {
        [Test]
        public void GeneratePassword_ShouldReturnCorrectPassword_ForValidInput()
        {
            // Arrange
            var username = "johnDoe";
            var dateOfBirth = new DateTime(1990, 1, 1);

            // Act
            var result = username.GeneratePassword(dateOfBirth);

            // Assert
            Assert.That(result, Is.EqualTo("JohnDoe@01011990"));
        }

        [Test]
        public void GeneratePassword_ShouldHandleSingleCharacterUsername()
        {
            // Arrange
            var username = "j";
            var dateOfBirth = new DateTime(1990, 1, 1);

            // Act
            var result = username.GeneratePassword(dateOfBirth);

            // Assert
            Assert.That(result, Is.EqualTo("J@01011990"));
        }

        [Test]
        public void GeneratePassword_ShouldHandleUsernameWithLeadingWhitespace()
        {
            // Arrange
            var username = " johnDoe";
            var dateOfBirth = new DateTime(1990, 1, 1);

            // Act
            var result = username.GeneratePassword(dateOfBirth);

            // Assert
            Assert.That(result, Is.EqualTo("JohnDoe@01011990"));
        }

        [Test]
        public void GeneratePassword_ShouldHandleUsernameWithTrailingWhitespace()
        {
            // Arrange
            var username = "johnDoe ";
            var dateOfBirth = new DateTime(1990, 1, 1);

            // Act
            var result = username.GeneratePassword(dateOfBirth);

            // Assert
            Assert.That(result, Is.EqualTo("JohnDoe@01011990"));
        }
    }
}