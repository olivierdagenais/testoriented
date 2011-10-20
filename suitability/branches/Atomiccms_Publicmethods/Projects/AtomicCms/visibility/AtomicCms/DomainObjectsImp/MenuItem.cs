namespace AtomicCms.Core.DomainObjectsImp
{
    using System;
    using Common.Abstract.DomainObjects;

    public class MenuItem : IMenuItem
    {
        private static readonly IMenuItem nullObject = new MenuItem
                                                           {
                                                               Entry = DomainObjectsImp.Entry.NullObject,
                                                               Id = 0,
                                                               MenuId = 0,
                                                               NavigateUrl = string.Empty,
                                                               Ordering = 0,
                                                               Title = string.Empty,
                                                               Visible = false
                                                           };

        public static IMenuItem NullObject
        {
            get { return nullObject; }
        }

        public int Ordering { get; set; }

        #region IMenuItem Members

        public int Id { get; set; }

        public string Title { get; set; }

        public string NavigateUrl { get; set; }

        public int MenuId { get; set; }

        public bool IsExternalUrl
        {
            get { return this.Entry == null; }
        }

        public bool Visible { get; set; }
        public IEntry Entry { get; set; }

        public string GetUrl() 
        { 
                if (IsExternalUrl)
                {
                    
                }

            return this.NavigateUrl;
        }

        #endregion
    }
}