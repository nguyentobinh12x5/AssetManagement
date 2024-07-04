using AssetManagement.Application.Assets.Commands.Create;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Commands.Create
{
    [TestFixture]
    public class CreateNewAssetCommandValidatorTests
    {
        private CreateNewAssetCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateNewAssetCommandValidator();
        }

        [Test]
        public void Should_HaveError_WhenNameIsEmpty()
        {
            var command = new CreateNewAssetCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Test]
        public void Should_HaveError_WhenNameIsTooLong()
        {
            var command = new CreateNewAssetCommand { Name = new string('a', 257) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Test]
        public void Should_HaveError_WhenCategoryIsEmpty()
        {
            var command = new CreateNewAssetCommand { Category = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Category);
        }

        [Test]
        public void Should_HaveError_WhenSpecificationIsEmpty()
        {
            var command = new CreateNewAssetCommand { Specification = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Specification);
        }

        [Test]
        public void Should_HaveError_WhenSpecificationIsTooLong()
        {
            var command = new CreateNewAssetCommand { Specification = new string('a', 1201) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Specification);
        }

        [Test]
        public void Should_HaveError_WhenInstalledDateIsInTheFuture()
        {
            var command = new CreateNewAssetCommand { InstalledDate = DateTime.UtcNow.AddDays(1) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.InstalledDate);
        }

        [Test]
        public void Should_HaveError_WhenStateIsEmpty()
        {
            var command = new CreateNewAssetCommand { State = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.State);
        }

        [Test]
        public void Should_NotHaveError_WhenCommandIsValid()
        {
            var command = new CreateNewAssetCommand
            {
                Name = "Valid Name",
                Category = "Valid Category",
                Specification = "Valid Specification",
                InstalledDate = DateTime.UtcNow.AddDays(-10),
                State = "Valid State"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}