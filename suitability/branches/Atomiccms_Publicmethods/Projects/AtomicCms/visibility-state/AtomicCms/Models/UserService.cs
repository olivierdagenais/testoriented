namespace AtomicCms.Core.Models
{
    using System;
    using System.Collections.Generic;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Abstract.Models;
    using Common.CodeContract;
    using Common.Utils;
    using DomainObjectsImp;

    public class UserService : IUserService
    {
        internal readonly IDaoFactory daoFactory;

        public UserService(IDaoFactory daoFactory)
        {
            this.daoFactory = daoFactory;
        }

        #region IUserService Members

        public IUser Login(string userName, string password)
        {
            IUser user = this.daoFactory.UserDao.Login(userName, password);
            if (null != user && this.IsPasswordValid(user, password))
            {
                return user;
            }

            return User.NullObject;
        }

        public ICollection<IUser> LoadAll()
        {
            return daoFactory.UserDao.LoadAll();
        }

        public IUser Load(int id)
        {
            return daoFactory.UserDao.Load(id);
        }

        public void Update(IUser user)
        {
            daoFactory.UserDao.Save(user);
        }

        #endregion

        private bool IsPasswordValid(IUser user, string password)
        {
            Contract.Requires<NullReferenceException>( user != null);

            return SimpleHash.VerifyHash(password,
                                         SimpleHash.Algorith.SHA512,
                                         user.Hash
                );
        }
    }
}