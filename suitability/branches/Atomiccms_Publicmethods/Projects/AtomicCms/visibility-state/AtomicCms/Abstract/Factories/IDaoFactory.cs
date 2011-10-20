namespace AtomicCms.Common.Abstract.Factories
{
    using Dao;
    using DomainObjects;

    public interface IDaoFactory
    {
        IEntryDao EntryDao { get; }
        ISiteDao SiteDao { get; }
        IMenuDao MenuDao { get; }
        IUserDao UserDao { get; }
    }
}