namespace AtomicCms.Common.CodeContract
{
    using System;
    using System.Diagnostics;
    using Exceptions;

    public static class Contract
    {

        [DebuggerStepThrough]
        public static void Requires(bool condition)
        {
            if (!condition)
            {
                throw new PreconditionException("Precondition failed.");
            }
        }

        [DebuggerStepThrough]
        public static void Requires(bool condition, string userMessage)
        {
            if (!condition)
            {
                throw new PreconditionException(userMessage);
            }
        }

        [DebuggerStepThrough]
        public static void Requires<TException>(bool condition) where TException : Exception, new()
        {
            if (!condition)
            {
                TException ex = new TException();
                throw ex;

            }
        }

        [DebuggerStepThrough]
        public static void Requires<TException>(bool condition, string userMessage) where TException : Exception, new()
        {
            if (!condition)
            {
                TException ex = (TException)typeof(TException)
                                                 .GetConstructor(new[] { typeof(string) })
                                                 .Invoke(new object[] { userMessage });

                throw ex;
            }
        }
    }
}