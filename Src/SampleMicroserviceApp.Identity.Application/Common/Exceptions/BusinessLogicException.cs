namespace SampleMicroserviceApp.Identity.Application.Common.Exceptions;

public class BusinessLogicException : Exception
{
    public BusinessLogicException(string msg) : base(msg)
    {
    }
}