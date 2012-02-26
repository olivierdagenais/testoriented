namespace AtomicCms.Common.Abstract.DomainObjects
{
    public interface IUser : IDomainObject
    {
        string Login { get; set; }
        string Hash { get; set; }
        string DisplayName { get; set; }
        string Email { get; set; }
        int Status { get; set; }
        int Role { get; set; }
        bool IsValid { get; set; }
    }
}