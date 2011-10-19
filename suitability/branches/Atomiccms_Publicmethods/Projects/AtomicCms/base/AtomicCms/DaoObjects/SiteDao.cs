namespace AtomicCms.Data.NHibernate.DaoObjects
{
    using System;
    using System.Collections.Generic;
    using Base;
    using Common.Abstract.Dao;
    using Common.Abstract.DomainObjects;
    using Common.Utils;
    using Core.DomainObjectsImp;
    using global::NHibernate;

    public class SiteDao : GenericDao<SiteAttribute>, ISiteDao
    {
        public SiteDao(ISessionFactory factory) : base(factory)
        {
        }

        #region ISiteDao Members

        public ISiteAttributes LoadAttributes()
        {
            ICollection<ISiteAttribute> ats = base.LoadAll().Convert<ISiteAttribute, SiteAttribute>();
            if (ats != null)
            {
                return new SiteAttributes(ats);
            }

            return new SiteAttributes(new List<ISiteAttribute>());
        }

        public ISiteAttribute LoadAttribute(int id)
        {
            return base.Load(id);
        }

        public void Update(ISiteAttribute attribute)
        {
            base.Save((SiteAttribute) attribute);
        }

        #endregion
    }
}