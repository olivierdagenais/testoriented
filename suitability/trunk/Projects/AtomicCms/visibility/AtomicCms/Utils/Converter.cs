namespace AtomicCms.Common.Utils
{
    using System.Collections.Generic;

    public static class Converter
    {
        public static ICollection<TInterface> Convert<TInterface, TImplementation>(this IEnumerable<TImplementation> initialCollection) where TImplementation : TInterface
        {
            ICollection<TInterface> convertedCollection = new List<TInterface>();

            foreach (TImplementation entry in initialCollection)
            {
                convertedCollection.Add(entry);
            }

            return convertedCollection;
        }
    }
}