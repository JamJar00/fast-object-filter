using System;
using System.Runtime.Serialization;

namespace FastObjectFilter
{
    [Serializable]
    public class FilterStringSyntaxException : Exception
    {
        public FilterStringSyntaxException()
        {
        }

        public FilterStringSyntaxException(string message) : base(message)
        {
        }

        public FilterStringSyntaxException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FilterStringSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
