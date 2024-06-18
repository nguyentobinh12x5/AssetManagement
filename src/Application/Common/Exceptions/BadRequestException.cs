using FluentValidation.Results;

namespace AssetManagement.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message)
    {
    }

}