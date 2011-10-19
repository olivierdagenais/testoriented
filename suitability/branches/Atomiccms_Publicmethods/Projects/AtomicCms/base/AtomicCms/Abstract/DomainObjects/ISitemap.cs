namespace AtomicCms.Common.Abstract.DomainObjects
{
    using System.Collections;
    using System.IO;

    public interface ISitemap : IList
    {
        void Serialize(TextWriter writer);
    }
}