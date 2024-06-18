using AssetManagement.Application.Common.Exceptions;

using FluentAssertions;

using FluentValidation.Results;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetManagement.Application.UnitTests.Common.Exceptions
{
    public class IncorrectPasswordExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            // Act
            var exception = new IncorrectPasswordException();

            // Assert
            exception.Message.Should().Be("Password is incorrect");
            exception.Errors.Keys.Should().BeEmpty();
        }

        [Test]
        public void ConstructorWithFailuresCreatesErrorDictionary()
        {
            // Arrange
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Password", "must contain lower case letter"),
                new ValidationFailure("Password", "must contain upper case letter"),
                new ValidationFailure("Password", "must contain at least 8 characters"),
                new ValidationFailure("Password", "must contain a digit"),
            };

            // Act
            var exception = new IncorrectPasswordException(failures);

            // Assert
            exception.Message.Should().Be("Password is incorrect");
            exception.Errors.Keys.Should().ContainSingle().And.Contain("Password");
            exception.Errors["Password"].Should().BeEquivalentTo(new[]
            {
                "must contain lower case letter",
                "must contain upper case letter",
                "must contain at least 8 characters",
                "must contain a digit"
            });
        }

        [Test]
        public void ConstructorWithEmptyFailuresCreatesEmptyErrorDictionary()
        {
            // Arrange
            var failures = Enumerable.Empty<ValidationFailure>();

            // Act
            var exception = new IncorrectPasswordException(failures);

            // Assert
            exception.Message.Should().Be("Password is incorrect");
            exception.Errors.Keys.Should().BeEmpty();
        }

        [Test]
        public void ConstructorWithNullFailuresCreatesEmptyErrorDictionary()
        {
            // Act
            var exception = new IncorrectPasswordException(Enumerable.Empty<ValidationFailure>());

            // Assert
            exception.Message.Should().Be("Password is incorrect");
            exception.Errors.Keys.Should().BeEmpty();
        }
    }
}
