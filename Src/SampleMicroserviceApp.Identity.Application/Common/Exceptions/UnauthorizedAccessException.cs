namespace SampleMicroserviceApp.Identity.Application.Common.Exceptions;

public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException(string msg) : base(msg)
    { }
}