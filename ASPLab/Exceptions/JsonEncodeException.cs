using System;
using System.Runtime.Serialization;

namespace ASPLab.Exceptions
{
    [Serializable]
    public class JsonEncodeException : Exception
    {
        public JsonEncodeException():base("Ваш Json файл имеет неправильную кодировку")
        {
        }

        public JsonEncodeException(string message) : base(message)
        {
        }

        public JsonEncodeException(string message, Exception inner) : base(message, inner)
        {
        }

        protected JsonEncodeException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
