using AssetManagement.Application.Common.Extensions;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Common.Extensions
{
    [TestFixture]
    public class StaffCodeGeneratorTests
    {
        [Test]
        public void GenerateNewStaffCode_ShouldReturnFirstCode_WhenNoExistingCodes()
        {
            // Arrange
            var existingCodes = Enumerable.Empty<string>();

            // Act
            var result = existingCodes.GenerateNewStaffCode();

            // Assert
            Assert.That(result, Is.EqualTo("SD0001"));
        }

        [Test]
        public void GenerateNewStaffCode_ShouldReturnNextCode_WhenExistingCodesPresent()
        {
            // Arrange
            var existingCodes = new List<string> { "SD0001", "SD0002", "SD0003" };

            // Act
            var result = existingCodes.GenerateNewStaffCode();

            // Assert
            Assert.That(result, Is.EqualTo("SD0004"));
        }

        [Test]
        public void GenerateNewStaffCode_ShouldIgnoreInvalidCodes()
        {
            // Arrange
            var existingCodes = new List<string> { "SD0001", "INVALID", "SD0002" };

            // Act
            var result = existingCodes.GenerateNewStaffCode();

            // Assert
            Assert.That(result, Is.EqualTo("SD0003"));
        }

        [Test]
        public void GenerateNewStaffCode_ShouldHandleLargeNumbers()
        {
            // Arrange
            var existingCodes = new List<string> { "SD9999" };

            // Act
            var result = existingCodes.GenerateNewStaffCode();

            // Assert
            Assert.That(result, Is.EqualTo("SD10000"));
        }

        [Test]
        public void GenerateNewStaffCode_ShouldIgnoreNonMatchingPrefixes()
        {
            // Arrange
            var existingCodes = new List<string> { "SD0001", "XY0002" };

            // Act
            var result = existingCodes.GenerateNewStaffCode();

            // Assert
            Assert.That(result, Is.EqualTo("SD0002"));
        }

        [Test]
        public void GenerateNewStaffCode_ShouldHandleEmptyAndInvalidCodes()
        {
            // Arrange
            var existingCodes = new List<string> { "", " ", "SD0001", "" };

            // Act
            var result = existingCodes.GenerateNewStaffCode();

            // Assert
            Assert.That(result, Is.EqualTo("SD0002"));
        }
    }
}