namespace AtomicCms.Common.Utils
{
    using System;

    public class LazyLoader
    {
        public static T Load<T>(ref T objectToBeInitialized, Type objectType, params object[] constructorArguments)
            where T : class
        {
            if (null == objectToBeInitialized)
            {
                objectToBeInitialized = (T) Activator.CreateInstance(objectType, constructorArguments);
            }

            return objectToBeInitialized;
        }
    }
}