namespace AtomicCms.Core.DomainObjectsImp
{
    using System;
    using Common.Abstract.DomainObjects;

    public class SiteAttribute : ISiteAttribute
    {
        internal static readonly ISiteAttribute nullObject = new SiteAttribute {Key = string.Empty, Value = string.Empty};

        public static ISiteAttribute NullObject
        {
            get { return nullObject; }
        }

        #region ISiteAttribute Members

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        #endregion
    }
}