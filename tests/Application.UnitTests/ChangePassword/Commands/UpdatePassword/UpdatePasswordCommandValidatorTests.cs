using AssetManagement.Application.ChangePassword.Commands.UpdatePassword;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.ChangePassword.Commands.UpdatePassword
{
    public class UpdatePasswordCommandValidatorTests
    {
        private UpdatePasswordCommandValidator _validator = null!;

        [SetUp]
        public void SetUp()
        {
            _validator = new UpdatePasswordCommandValidator();
        }

        [Test]
        public void ShouldHaveError_WhenNewPasswordIsEmpty()
        {
            var model = new UpdatePasswordCommand("CurrentPassword123!", string.Empty);
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.NewPassword)
                  .WithErrorMessage("New password is required.");
        }

        [Test]
        public void ShouldHaveError_WhenNewPasswordIsLessThan6Characters()
        {
            var model = new UpdatePasswordCommand("CurrentPassword123!", "Ab1!");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.NewPassword)
                  .WithErrorMessage("New password must be at least 6 characters long.");
        }

        [Test]
        public void ShouldHaveError_WhenNewPasswordDoesNotContainUppercaseLetter()
        {
            var model = new UpdatePasswordCommand("CurrentPassword123!", "abc123!");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.NewPassword)
                  .WithErrorMessage("New password must contain at least one uppercase letter.");
        }

        [Test]
        public void ShouldHaveError_WhenNewPasswordDoesNotContainLowercaseLetter()
        {
            var model = new UpdatePasswordCommand("CurrentPassword123!", "ABC123!");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.NewPassword)
                  .WithErrorMessage("New password must contain at least one lowercase letter.");
        }

        [Test]
        public void ShouldHaveError_WhenNewPasswordDoesNotContainDigit()
        {
            var model = new UpdatePasswordCommand("CurrentPassword123!", "Abcdef!");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.NewPassword)
                  .WithErrorMessage("New password must contain at least one digit.");
        }

        [Test]
        public void ShouldHaveError_WhenNewPasswordDoesNotContainNonAlphanumericCharacter()
        {
            var model = new UpdatePasswordCommand("CurrentPassword123!", "Abcdef1");
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(command => command.NewPassword)
                  .WithErrorMessage("New password must contain at least one non-alphanumeric character.");
        }

        [Test]
        public void ShouldNotHaveError_WhenNewPasswordIsValid()
        {
            var model = new UpdatePasswordCommand("CurrentPassword123!", "Abc123!");
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(command => command.NewPassword);
        }
    }
}