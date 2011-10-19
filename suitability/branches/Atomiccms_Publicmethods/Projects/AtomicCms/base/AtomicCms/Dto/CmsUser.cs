namespace AtomicCms.Common.Dto
{
    using System;

    public class CmsUser
    {
        public int Id{ get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime Registered { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
    }
}