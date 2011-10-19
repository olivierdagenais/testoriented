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
    using global::NHibernate.Criterion;

    public class UserDao : GenericDao<User>, IUserDao
    {
        public UserDao(ISessionFactory factory) : base(factory)
        {
        }

        #region IUserDao Members

        public IUser Login(string userName, string password)
        {
            using (ISession s = base.OpenSession())
            {
                return s.CreateCriteria(typeof (User))
                    .Add(Restrictions.Eq("Login",
                                         userName)).UniqueResult<User>();
            }
        }

        public new ICollection<IUser> LoadAll()
        {
            return base.LoadAll().Convert<IUser, User>();
        }

        public new IUser Load(int id)
        {
            return base.Load(id);
        }

        public void Save(IUser user)
        {
            base.Save((User) user);
        }

        #endregion
    }
}