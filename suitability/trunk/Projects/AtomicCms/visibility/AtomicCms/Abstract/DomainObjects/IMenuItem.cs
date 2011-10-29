namespace AtomicCms.Common.Abstract.DomainObjects
{
    public interface IMenuItem
    {
        int Id { get; set; }
        string Title { get; set; }
        string NavigateUrl { get; set; }
        int MenuId { get; set; }
        bool IsExternalUrl { get; }
        bool Visible { get; set; }
        IEntry Entry { get; set; }
    }
}