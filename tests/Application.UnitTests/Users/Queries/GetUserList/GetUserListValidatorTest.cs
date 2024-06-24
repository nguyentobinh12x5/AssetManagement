﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

using Application.Users.Queries.GetUsers;

using AssetManagement.Application.Common.Models;
using AssetManagement.Application.Users.Queries.GetUsers;

using FluentAssertions;

using FluentValidation.TestHelper;

using Moq;

using NUnit.Framework;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AssetManagement.Application.UnitTests.Users.Queries.GetUserList;

[TestFixture]
public class GetUserListValidatorTest
{
    private GetUsersQueryValidator validator;

    [SetUp]
    public void Setup()
    {
        validator = new GetUsersQueryValidator();
    }
    [Test]
    public void Handle_SortByUsername_ThrowsException()
    {
        // Arrange
        var query = new GetUsersQuery
        {
            PageNumber = 1,
            PageSize = 5,
            SortColumnName = "UserName",
            SortColumnDirection = "Ascending",
            Location = "HCM"
        };

        // Act
        var result = validator.TestValidate(query);

        // Assert
        result.Errors.Should().Contain(e => e.ErrorMessage == "Sorting by Username is not allowed.");
    }
}