using System.Runtime.Serialization;

namespace CarAuctionApi.Service.Exceptions;

public class TokenExpiredException : Exception
{
    private const string DefaultMessage = "Token is expired";
    public TokenExpiredException() : base(DefaultMessage)
    {
    }

    protected TokenExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TokenExpiredException(string? message) : base(message)
    {
    }

    public TokenExpiredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}