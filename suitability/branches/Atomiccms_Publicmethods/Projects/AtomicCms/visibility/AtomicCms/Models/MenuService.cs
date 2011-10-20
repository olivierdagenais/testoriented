namespace AtomicCms.Core.Models
{
    using System.Collections.Generic;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Abstract.Models;
    using DomainObjectsImp;

    public class MenuService : IMenuService
    {
        private readonly IDaoFactory daoFactory;

        public MenuService(IDaoFactory daoFactory)
        {
            this.daoFactory = daoFactory;
        }

        #region IMenuService Members

        public void SaveMenuItem(IMenuItem item)
        {
            this.daoFactory.MenuDao.SaveMenuItem(item);
        }

        public ICollection<IMenu> LoadMenues()
        {
            return this.daoFactory.MenuDao.LoadAll();
        }

        public IMenuItem LoadMenuItem(int id)
        {
            return this.daoFactory.MenuDao.LoadMenuItem(id);
        }

        public IMenu LoadMenu(string menuName)
        {
            IMenu menu = this.daoFactory.MenuDao.LoadMenu(menuName);
            if(null != menu)
            {
                this.RemoveNotVisibleMenuItems(menu);
            }
            else
            {
                menu = Menu.NullObject;
            }

            return menu;
        }

        public IMenu LoadMenu(int id)
        {
            return this.daoFactory.MenuDao.Load(id);
        }

        public void SaveMenu(IMenu menu)
        {
            this.daoFactory.MenuDao.Save(menu);
        }

        #endregion

        private void RemoveNotVisibleMenuItems(IMenu menu)
        {
            ICollection<IMenuItem> itemsToRemove = new List<IMenuItem>();

            foreach (IMenuItem item in menu.MenuItems)
            {
                if (item.Visible == false)
                {
                    itemsToRemove.Add(item);
                }
            }

            foreach (IMenuItem item in itemsToRemove)
            {
                menu.MenuItems.Remove(item);
            }
        }
    }
}