namespace AtomicCms.Common.Exceptions
{
    using System;

    public class PreconditionException : AtomicCmsException
    {
        public PreconditionException()
        {
        }

        public PreconditionException(string message, Exception innerException) : base(message,
                                                                                      innerException)
        {
        }

        public PreconditionException(string message) : base(message)
        {
        }
    }
}