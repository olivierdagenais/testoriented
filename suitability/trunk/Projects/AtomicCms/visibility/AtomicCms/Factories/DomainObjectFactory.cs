namespace AtomicCms.Core.Factories
{
    using System;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using DomainObjectsImp;

    public class DomainObjectFactory : IDomainObjectFactory
    {
        #region IDomainObjectFactory Members

        public IEntry CreateEntry()
        {
            return new Entry();
        }

        public ISitemap CreateSitemap()
        {
            return new Sitemap();
        }

        public ISitemapUrl CreateSitemapUrl()
        {
            return new SitemapUrl();
        }

        public IMenu CreateMenu()
        {
            return new Menu();
        }

        #endregion
    }
}