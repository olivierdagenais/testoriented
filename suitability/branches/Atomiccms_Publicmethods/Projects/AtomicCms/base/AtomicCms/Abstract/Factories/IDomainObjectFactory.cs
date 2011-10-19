namespace AtomicCms.Common.Abstract.Factories
{
    using DomainObjects;

    public interface IDomainObjectFactory
    {
        IEntry CreateEntry();
        ISitemap CreateSitemap();
        ISitemapUrl CreateSitemapUrl();
        IMenu CreateMenu();
    }
}