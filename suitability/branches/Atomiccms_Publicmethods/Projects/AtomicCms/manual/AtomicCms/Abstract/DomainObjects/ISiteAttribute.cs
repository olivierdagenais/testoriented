namespace AtomicCms.Common.Abstract.DomainObjects
{
    public interface ISiteAttribute : IDomainObject
    {
        string Key { get; set; }
        string Value { get; set; }
    }
}