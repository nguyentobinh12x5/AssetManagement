using AssetManagement.Application.Users.Commands.Create;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Commands.Create
{
    [TestFixture]
    public class CreateUserCommandValidatorTests
    {
        private CreateUserCommandValidator _commandValidator;

        [SetUp]
        public void Setup()
        {
            _commandValidator = new CreateUserCommandValidator();
        }

        [Test]
        public void FirstName_Valid()
        {
            var command = new CreateUserCommand { FirstName = "John", DateOfBirth = DateTime.UtcNow.AddYears(-25), JoinDate = DateTime.UtcNow };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.IsValid);
        }

        [Test]
        public void FirstName_Invalid()
        {
            var command = new CreateUserCommand { FirstName = "John123" };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.Errors.Any(e => e.ErrorMessage == "The First Name field allows only alphabetical characters. Please remove any numbers, special characters, or spaces"));
        }

        [Test]
        public void LastName_Valid()
        {
            var command = new CreateUserCommand { LastName = "Doe", DateOfBirth = DateTime.UtcNow.AddYears(-25), JoinDate = DateTime.UtcNow };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.IsValid);
        }

        [Test]
        public void LastName_Invalid()
        {
            var command = new CreateUserCommand { LastName = "Doe123", DateOfBirth = DateTime.UtcNow.AddYears(-25), JoinDate = DateTime.UtcNow };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.Errors.Any(e => e.ErrorMessage == "The Last Name field allows only alphabetical characters and spaces. Please remove any numbers or special characters."));
        }

        [Test]
        public void DateOfBirth_Valid()
        {
            var command = new CreateUserCommand { DateOfBirth = DateTime.UtcNow.AddYears(-25), JoinDate = DateTime.UtcNow };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.IsValid);
        }

        [Test]
        public void DateOfBirth_Empty()
        {
            var command = new CreateUserCommand
            {
                DateOfBirth = DateTime.MinValue
            };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.Errors.Any(e => e.ErrorMessage == "Please Select Date of Birth"));
        }

        [Test]
        public void DateOfBirth_Under18()
        {
            var command = new CreateUserCommand { DateOfBirth = DateTime.UtcNow.AddYears(-17) };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.Errors.Any(e => e.ErrorMessage == "User is under 18. Please select a different date"));
        }

        [Test]
        public void JoinDate_Valid()
        {
            var command = new CreateUserCommand { DateOfBirth = DateTime.UtcNow.AddYears(-25), JoinDate = new DateTime(2024, 6, 27) };
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.IsValid);
        }

        [Test]
        public void JoinDate_Under18()
        {
            var command = new CreateUserCommand { DateOfBirth = DateTime.UtcNow.AddYears(-17), JoinDate = DateTime.UtcNow.AddYears(-1) }; // JoinDate should be at least 18 years after DateOfBirth
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.Errors.Any(e => e.ErrorMessage == "User under the age of 18 may not join the company. Please select a different date"));
        }

        [Test]
        public void JoinDate_Weekend()
        {
            var command = new CreateUserCommand { DateOfBirth = DateTime.UtcNow.AddYears(-25), JoinDate = new DateTime(2024, 6, 22) }; // A Saturday
            var result = _commandValidator.TestValidate(command);
            Assert.That(result.Errors.Any(e => e.ErrorMessage == "Joined date is Saturday or Sunday. Please select a different date"));
        }
    }
}