using AssetManagement.Application.Assignments.Commands.Create;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assignments.Commands.Create
{
    [TestFixture]
    public class CreateNewAssignmentCommandValidatorTests
    {
        private CreateNewAssignmentCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateNewAssignmentCommandValidator();
        }

        [Test]
        public void Should_HaveError_WhenAssetIdIsEmpty()
        {
            var command = new CreateNewAssignmentCommand { AssetId = 0, UserId = "Admin", Note = "Note" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.AssetId);
        }

        [Test]
        public void Should_HaveError_WhenUserIdIsEmpty()
        {
            var command = new CreateNewAssignmentCommand { AssetId = 1, UserId = string.Empty, Note = "Note" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Test]
        public void Should_HaveError_WhenNoteIsEmpty()
        {
            var command = new CreateNewAssignmentCommand { AssetId = 1, UserId = "Admin", Note = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Note);
        }

        [Test]
        public void Should_HaveError_WhenNoteIsTooLong()
        {
            var command = new CreateNewAssignmentCommand { AssetId = 1, UserId = "Admin", Note = new string('a', 1201) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Note);
        }

        [Test]
        public void Should_HaveError_WhenAssignedDateIsInThePast()
        {
            var command = new CreateNewAssignmentCommand { AssetId = 1, UserId = "Admin", Note = "Note", AssignedDate = DateTime.UtcNow.AddDays(-1) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.AssignedDate);
        }

        [Test]
        public void Should_NotHaveError_WhenCommandIsValid()
        {
            var command = new CreateNewAssignmentCommand
            {
                AssetId = 1,
                UserId = "User1",
                Note = "Valid Note",
                AssignedDate = DateTime.UtcNow.Date
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}