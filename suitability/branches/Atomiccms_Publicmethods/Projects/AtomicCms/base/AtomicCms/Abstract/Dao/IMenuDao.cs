namespace AtomicCms.Common.Abstract.Dao
{
    using System.Collections.Generic;
    using DomainObjects;

    public interface IMenuDao
    {
        IMenu LoadMenu(string menuName);
        ICollection<IMenu> LoadAll();
        IMenu Load(int id);
        IMenuItem LoadMenuItem(int id);
        void Save(IMenu menu);
        void SaveMenuItem(IMenuItem item);
    }
}