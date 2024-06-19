using AssetManagement.Application.Auth.Commands.ChangePasswordFirstTime;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Auth.Commands.ChangePasswordFirstTime;

public class ChangePasswordFirstTimeValidatorTest
{
    private ChangePasswordFirstTimeCommandValidator _validator = null!;

    [SetUp]
    public void SetUp()
    {
        _validator = new ChangePasswordFirstTimeCommandValidator();
    }


    [Test]
    public void ShouldHaveError_WhenNewPasswordIsEmpty()
    {
        var model = new ChangePasswordFirstTimeCommand(string.Empty);
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.NewPassword)
              .WithErrorMessage("New password is required.");
    }

    [Test]
    public void ShouldHaveError_WhenNewPasswordIsLessThan6Characters()
    {
        var model = new ChangePasswordFirstTimeCommand("Ab1!");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.NewPassword)
              .WithErrorMessage("New password must be at least 6 characters long.");
    }

    [Test]
    public void ShouldHaveError_WhenNewPasswordDoesNotContainUppercaseLetter()
    {
        var model = new ChangePasswordFirstTimeCommand("abc123!");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.NewPassword)
              .WithErrorMessage("New password must contain at least one uppercase letter.");
    }

    [Test]
    public void ShouldHaveError_WhenNewPasswordDoesNotContainLowercaseLetter()
    {
        var model = new ChangePasswordFirstTimeCommand("ABC123!");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.NewPassword)
              .WithErrorMessage("New password must contain at least one lowercase letter.");
    }

    [Test]
    public void ShouldHaveError_WhenNewPasswordDoesNotContainDigit()
    {
        var model = new ChangePasswordFirstTimeCommand("Abcdef!");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.NewPassword)
              .WithErrorMessage("New password must contain at least one digit.");
    }

    [Test]
    public void ShouldHaveError_WhenNewPasswordDoesNotContainNonAlphanumericCharacter()
    {
        var model = new ChangePasswordFirstTimeCommand("Abcdef1");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(command => command.NewPassword)
              .WithErrorMessage("New password must contain at least one non-alphanumeric character.");
    }

    [Test]
    public void ShouldNotHaveError_WhenNewPasswordIsValid()
    {
        var model = new ChangePasswordFirstTimeCommand("Abc123!");
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(command => command.NewPassword);
    }
}