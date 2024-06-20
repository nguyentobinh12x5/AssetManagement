using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace AssetManagement.Application.Common.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException()
            : base("Password is incorrect")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public IncorrectPasswordException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}