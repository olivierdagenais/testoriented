namespace AtomicCms.Common.Abstract.Dao
{
    using System.Collections.Generic;
    using DomainObjects;

    public interface IUserDao
    {
        IUser Login(string userName, string password);
        ICollection<IUser> LoadAll();
        IUser Load(int id);
        void Save(IUser user);
    }
}