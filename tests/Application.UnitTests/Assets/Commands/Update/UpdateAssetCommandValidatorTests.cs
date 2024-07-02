using System;

using AssetManagement.Application.Assets.Commands.Update;

using FluentValidation.TestHelper;

using NUnit.Framework;

namespace AssetManagement.Application.UnitTests.Assets.Commands.Update
{
    [TestFixture]
    public class UpdateAssetCommandValidatorTests
    {
        private UpdateAssetCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateAssetCommandValidator();
        }

        [Test]
        public void Should_HaveError_WhenNameIsEmpty()
        {
            var command = new UpdateAssetCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Test]
        public void Should_HaveError_WhenNameIsTooLong()
        {
            var command = new UpdateAssetCommand { Name = new string('a', 257) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Test]
        public void Should_HaveError_WhenSpecificationIsEmpty()
        {
            var command = new UpdateAssetCommand { Specification = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Specification);
        }

        [Test]
        public void Should_HaveError_WhenSpecificationIsTooLong()
        {
            var command = new UpdateAssetCommand { Specification = new string('a', 1201) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Specification);
        }

        [Test]
        public void Should_HaveError_WhenInstalledDateIsInTheFuture()
        {
            var command = new UpdateAssetCommand { InstalledDate = DateTime.UtcNow.AddDays(1) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.InstalledDate);
        }

        [Test]
        public void Should_HaveError_WhenStateIsEmpty()
        {
            var command = new UpdateAssetCommand { State = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.State);
        }

        [Test]
        public void Should_NotHaveError_WhenCommandIsValid()
        {
            var command = new UpdateAssetCommand
            {
                Name = "Valid Name",
                Specification = "Valid Specification",
                InstalledDate = DateTime.UtcNow.AddDays(-10),
                State = "Valid State"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}