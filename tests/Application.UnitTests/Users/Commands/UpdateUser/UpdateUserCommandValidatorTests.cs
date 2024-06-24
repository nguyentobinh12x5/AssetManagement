using AssetManagement.Application.Users.Commands.UpdateUser;
using AssetManagement.Domain.Enums;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Users.Commands.UpdateUser;

public class UpdateUserCommandValidatorTests
{
    private UpdateUserCommandValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new UpdateUserCommandValidator();
    }

    [Test]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var command = new UpdateUserCommand { Id = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Test]
    public void Should_Have_Error_When_Id_Exceeds_MaxLength()
    {
        var command = new UpdateUserCommand { Id = new string('a', 257) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Test]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        var command = new UpdateUserCommand { Id = "valid-id" };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    [Test]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var command = new UpdateUserCommand { FirstName = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.FirstName);
    }

    [Test]
    public void Should_Have_Error_When_FirstName_Exceeds_MaxLength()
    {
        var command = new UpdateUserCommand { FirstName = new string('a', 257) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.FirstName);
    }

    [Test]
    public void Should_Not_Have_Error_When_FirstName_Is_Valid()
    {
        var command = new UpdateUserCommand { FirstName = "valid-first-name" };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.FirstName);
    }

    [Test]
    public void Should_Have_Error_When_LastName_Is_Empty()
    {
        var command = new UpdateUserCommand { LastName = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.LastName);
    }

    [Test]
    public void Should_Have_Error_When_LastName_Exceeds_MaxLength()
    {
        var command = new UpdateUserCommand { LastName = new string('a', 257) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.LastName);
    }

    [Test]
    public void Should_Not_Have_Error_When_LastName_Is_Valid()
    {
        var command = new UpdateUserCommand { LastName = "valid-last-name" };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.LastName);
    }

    [Test]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new UpdateUserCommand { DateOfBirth = DateTime.Now.AddDays(1) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.DateOfBirth);
    }

    [Test]
    public void Should_Not_Have_Error_When_DateOfBirth_Is_Valid()
    {
        var command = new UpdateUserCommand { DateOfBirth = DateTime.Now.AddDays(-1) };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.DateOfBirth);
    }

    [Test]
    public void Should_Have_Error_When_JoinDate_Is_In_The_Future()
    {
        var command = new UpdateUserCommand { JoinDate = DateTime.Now.AddDays(1) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.JoinDate);
    }

    [Test]
    public void Should_Not_Have_Error_When_JoinDate_Is_Valid()
    {
        var command = new UpdateUserCommand { JoinDate = DateTime.Now.AddDays(-1) };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.JoinDate);
    }

    [Test]
    public void Should_Have_Error_When_Gender_Is_Invalid()
    {
        var command = new UpdateUserCommand { Gender = (Gender)99 };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Gender);
    }

    [Test]
    public void Should_Not_Have_Error_When_Gender_Is_Valid()
    {
        var command = new UpdateUserCommand { Gender = Gender.Male };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.Gender);
    }

    [Test]
    public void Should_Have_Error_When_Type_Is_Empty()
    {
        var command = new UpdateUserCommand { Type = string.Empty };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Type);
    }

    [Test]
    public void Should_Have_Error_When_Type_Exceeds_MaxLength()
    {
        var command = new UpdateUserCommand { Type = new string('a', 257) };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Type);
    }

    [Test]
    public void Should_Not_Have_Error_When_Type_Is_Valid()
    {
        var command = new UpdateUserCommand { Type = "valid-type" };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(c => c.Type);
    }
}