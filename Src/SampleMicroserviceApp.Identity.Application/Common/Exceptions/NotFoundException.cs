namespace SampleMicroserviceApp.Identity.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string msg) : base(msg)
    { }
}