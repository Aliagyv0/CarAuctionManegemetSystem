using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionApi.Service.Exceptions
{
    public class DeleteMainImageException : Exception
    {
        public DeleteMainImageException()
        {
        }

        public DeleteMainImageException(string? message) : base(message)
        {
        }

        
        public DeleteMainImageException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeleteMainImageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
