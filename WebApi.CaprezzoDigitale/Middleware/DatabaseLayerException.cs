using System;
using System.Runtime.Serialization;

namespace WebApi.CaprezzoDigitale.Middleware
{
    [Serializable]
    internal class DatabaseLayerException : Exception
    {
        public DatabaseLayerException()
        {
        }

        public DatabaseLayerException(string message) : base(message)
        {
        }

        public DatabaseLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}