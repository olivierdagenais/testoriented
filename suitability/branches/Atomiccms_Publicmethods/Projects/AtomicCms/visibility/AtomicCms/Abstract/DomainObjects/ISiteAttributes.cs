namespace AtomicCms.Common.Abstract.DomainObjects
{
    using System.Collections.Generic;

    public interface ISiteAttributes
    {
        string GetValue(string key);
        IEnumerable<ISiteAttribute> Attributes { get; }
    }
}