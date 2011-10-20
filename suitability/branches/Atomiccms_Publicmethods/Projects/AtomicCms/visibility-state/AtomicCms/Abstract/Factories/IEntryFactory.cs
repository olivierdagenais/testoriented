namespace AtomicCms.Common.Abstract.Factories
{
    using DomainObjects;

    public interface IEntryFactory
    {
        IEntry CreateEntry();
    }
}