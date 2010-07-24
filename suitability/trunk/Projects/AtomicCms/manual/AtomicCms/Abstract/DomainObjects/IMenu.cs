namespace AtomicCms.Common.Abstract.DomainObjects
{
    using System.Collections.Generic;

    public interface IMenu
    {
        ICollection<IMenuItem> MenuItems { get; set; }
        int Id { get; set; }
        string Title { get; set; }
        string Type { get; set; }
        string Description { get; set; }
    }
}