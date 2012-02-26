namespace AtomicCms.Common.Abstract.Models
{
    using System.Collections.Generic;
    using DomainObjects;

    public interface IMenuService
    {
        IMenu LoadMenu(int id);
        void SaveMenu(IMenu menu);
        IMenu LoadMenu(string menuName);
        ICollection<IMenu> LoadMenues();
        IMenuItem LoadMenuItem(int id);
        void SaveMenuItem(IMenuItem item);
    }
}