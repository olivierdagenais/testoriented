namespace AtomicCms.Core.Models
{
    using System;
    using System.Text;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Abstract.Models;
    using Common.Extensions;

    public class SiteService : ISiteService
    {
        internal readonly IDaoFactory daoFactory;

        public SiteService(IDaoFactory daoFactory)
        {
            this.daoFactory = daoFactory;
        }

        #region ISiteService Members

        public ISiteAttributes LoadSiteAttributes()
        {
            return this.daoFactory.SiteDao.LoadAttributes();
        }

        public ISiteAttribute LoadSiteAttribute(int id)
        {
            return daoFactory.SiteDao.LoadAttribute(id);
        }

        public void Update(ISiteAttribute attribute)
        {
            daoFactory.SiteDao.Update(attribute);
        }

        #endregion
    }
}