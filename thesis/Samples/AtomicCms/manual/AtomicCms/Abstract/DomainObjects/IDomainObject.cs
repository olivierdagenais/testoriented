namespace AtomicCms.Common.Abstract.DomainObjects
{
    using System;

    public interface IDomainObject
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}