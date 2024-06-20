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
    public class UsernameGeneratorTests
    {
        [Test]
        public void GenerateUsername_ShouldReturnCorrectUsername_ForValidInput()
        {
            // Arrange
            var existingUsernames = new List<string> { "johnd", "asmith", "johnd1" };
            var firstName = "John";
            var lastName = "Doe";

            // Act
            var result = existingUsernames.GenerateUsername(firstName, lastName);

            // Assert
            Assert.That(result, Is.EqualTo("johnd2"));
        }

        [Test]
        public void GenerateUsername_ShouldHandleExistingUsernames()
        {
            // Arrange
            var existingUsernames = new List<string> { "johnd", "asmith", "johnd1", "johnd2" };
            var firstName = "John";
            var lastName = "Doe";

            // Act
            var result = existingUsernames.GenerateUsername(firstName, lastName);

            // Assert
            Assert.That(result, Is.EqualTo("johnd3"));
        }

        [Test]
        public void GenerateUsername_ShouldHandleSingleName()
        {
            // Arrange
            var existingUsernames = new List<string> { "jdoe", "asmith" };
            var firstName = "Alice";
            var lastName = " ";

            // Act
            var result = existingUsernames.GenerateUsername(firstName, lastName);

            // Assert
            Assert.That(result, Is.EqualTo("alice"));
        }

        [Test]
        public void GenerateUsername_ShouldHandleEmptyLastName()
        {
            // Arrange
            var existingUsernames = new List<string> { "jdoe", "asmith" };
            var firstName = "Bob";
            var lastName = "";

            // Act
            var result = existingUsernames.GenerateUsername(firstName, lastName);

            // Assert
            Assert.That(result, Is.EqualTo("bob"));
        }

        [Test]
        public void GenerateUsername_ShouldHandleEmptyExistingUsernames()
        {
            // Arrange
            var existingUsernames = new List<string>();
            var firstName = "Jane";
            var lastName = "Smith";

            // Act
            var result = existingUsernames.GenerateUsername(firstName, lastName);

            // Assert
            Assert.That(result, Is.EqualTo("janes"));
        }
    }
}