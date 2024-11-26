using System.Runtime.Serialization;

namespace CarAuctionApi.Service.Exceptions;

public class UserNotFoundException : Exception
{
    private const string DefaultMessage = "User is not found!";
    
    public UserNotFoundException() : base(DefaultMessage)
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }   
}