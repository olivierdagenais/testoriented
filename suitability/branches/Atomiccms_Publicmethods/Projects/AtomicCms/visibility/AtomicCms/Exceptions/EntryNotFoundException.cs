namespace AtomicCms.Common.Exceptions
{
    using System;

    public class EntryNotFoundException : AtomicCmsException
    {
        public EntryNotFoundException()
        {
        }

        public EntryNotFoundException(string message, Exception innerException) : base(message, 
                                                                                       innerException)
        {
        }

        public EntryNotFoundException(string message) : base(message)
        {
        }
    }
}