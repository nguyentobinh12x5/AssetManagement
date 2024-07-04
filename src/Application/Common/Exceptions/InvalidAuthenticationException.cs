namespace AssetManagement.Application.Common.Exceptions;

public class InvalidAuthenticationException : Exception
{
    public InvalidAuthenticationException(string message)
        : base(message)
    {
    }

}