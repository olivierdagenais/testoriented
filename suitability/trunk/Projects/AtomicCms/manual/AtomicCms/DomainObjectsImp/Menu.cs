namespace AtomicCms.Core.DomainObjectsImp
{
    using System.Collections.Generic;
    using Common.Abstract.DomainObjects;

    public class Menu : IMenu
    {
        private static readonly IMenu nullMenu = new Menu
                                                     {
                                                         Id = 0,
                                                         Description = string.Empty,
                                                         MenuItems = new List<IMenuItem>(),
                                                         Title = string.Empty,
                                                         Type = string.Empty
                                                     };

        public static IMenu NullObject
        {
            get { return nullMenu; }
        }

        #region IMenu Members

        public ICollection<IMenuItem> MenuItems { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        #endregion
    }
}