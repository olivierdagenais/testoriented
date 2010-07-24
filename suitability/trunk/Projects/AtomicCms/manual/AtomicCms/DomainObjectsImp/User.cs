namespace AtomicCms.Core.DomainObjectsImp
{
    using System;
    using Common.Abstract.DomainObjects;
    using Common.Utils;

    public class User : IUser
    {
        public User()
        {
            this.IsValid = true;
        }

        public static IUser NullObject
        {
            get
            {
                return new User { Id = 0, CreatedAt = DateTime.Now, DisplayName = "Unknow", Login = "Unknow", IsValid = false };
            }
        }

        #region IUser Members

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
        public bool IsValid { get; set; }

        public string Password
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Hash = SimpleHash.ComputeHash(value,
                                                  SimpleHash.Algorith.SHA512,
                                                  null);
                }
            }
        }

        #endregion

        public static IUser GetCurrentUser()
        {
            return new User
                       {
                           Id = 1,
                           CreatedAt = DateTime.Now,
                           DisplayName = "Alexander",
                           Email = "",
                           Hash = "",
                           Login = "Alexander",
                           Role = 1,
                           Status = 1
                       };
        }
    }
}