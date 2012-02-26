namespace AtomicCms.Common.Exceptions
{
    using System;

    public class ArgumentException :AtomicCmsException
    {
        public ArgumentException()
        {
        }

        public ArgumentException(string message, Exception innerException) : base(message,
                                                                  innerException)
        {
        }

        public ArgumentException(string message) : base(message)
        {
        }
    }
}