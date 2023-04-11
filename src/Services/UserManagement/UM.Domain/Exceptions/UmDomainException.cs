using Common.Domain.Exceptions;

namespace UM.Domain.Exceptions;

public class UmDomainException : DomainException
{
    public UmDomainException()
    {
    }

    public UmDomainException(string message)
        : base(message)
    {
    }

    public UmDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}