namespace AtomicCms.Common.Abstract.Models
{
    using System.Collections.Generic;
    using DomainObjects;

    public interface IUserService
    {
        IUser Login(string userName, string password);
        ICollection<IUser> LoadAll();
        IUser Load(int id);
        void Update(IUser user);
    }
}