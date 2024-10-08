﻿using AssetManagement.Application.Common.Extensions;
using AssetManagement.Application.Common.Interfaces;
using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Commands.Create;
using AssetManagement.Domain.Enums;

using Moq;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Commands.Create
{
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        [Test]
        public async Task Handle_ValidCommand_GeneratesCorrectStaffCode()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockIdentityService = new Mock<IIdentityService>();

            var handler = new CreateUserCommandHandler(mockContext.Object, mockIdentityService.Object);

            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Location = "New York",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = Gender.Male,
                JoinDate = DateTime.UtcNow,
                Type = "Employee"
            };

            var existingCodes = new List<string> { "SD0001", "SD0002", "SD0003" }; // Mock existing codes

            var expectedStaffCode = "SD0004"; // Expected new staff code based on existingCodes

            mockIdentityService.Setup(x => x.CreateUserAsync(It.IsAny<CreateUserDto>()))
                               .ReturnsAsync((Result.Success(), expectedStaffCode));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedStaffCode));

            mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
            mockIdentityService.Verify(x => x.CreateUserAsync(It.IsAny<CreateUserDto>()), Times.Once);

            // Additional assertion to verify the generated staff code
            var generatedStaffCode = existingCodes.GenerateNewStaffCode();
            Assert.That(generatedStaffCode, Is.EqualTo(expectedStaffCode));
        }

        [Test]
        public async Task Handle_ValidCommand_SuccessfullyCreatesUser()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockIdentityService = new Mock<IIdentityService>();

            var handler = new CreateUserCommandHandler(mockContext.Object, mockIdentityService.Object);

            var command = new CreateUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                Location = "New York",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = Gender.Male,
                JoinDate = DateTime.UtcNow,
                Type = "Employee"
            };

            var expectedStaffCode = "SD0001"; // Mocked staff code

            mockIdentityService.Setup(x => x.CreateUserAsync(It.IsAny<CreateUserDto>()))
                               .ReturnsAsync((Result.Success(), expectedStaffCode));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(expectedStaffCode));
            mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
            mockIdentityService.Verify(x => x.CreateUserAsync(It.IsAny<CreateUserDto>()), Times.Once);
        }
    }
}