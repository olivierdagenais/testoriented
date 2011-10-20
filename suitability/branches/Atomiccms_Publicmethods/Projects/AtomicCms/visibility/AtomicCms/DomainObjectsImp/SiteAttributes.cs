namespace AtomicCms.Core.DomainObjectsImp
{
    using System.Collections.Generic;
    using Common.Abstract.DomainObjects;
    using Common.CodeContract;

    public class SiteAttributes : ISiteAttributes
    {
        internal readonly List<ISiteAttribute> attributes;

        public SiteAttributes(IEnumerable<ISiteAttribute> attributes)
        {
            Check.Argument.IsNotNull(attributes,
                                     "attributes");
            this.attributes = new List<ISiteAttribute>(attributes);
        }

        public static ISiteAttributes NullObject
        {
            get { return new SiteAttributes(new List<ISiteAttribute>()); }
        }

        #region ISiteAttributes Members

        public string GetValue(string key)
        {
            ISiteAttribute attr = this.attributes.Find(x => x.Key == key);
            if (attr != null && !string.IsNullOrEmpty(attr.Value))
            {
                return attr.Value;
            }

            return string.Empty;
        }

        public IEnumerable<ISiteAttribute> Attributes
        {
            get { return this.attributes; }
        }

        #endregion
    }
}