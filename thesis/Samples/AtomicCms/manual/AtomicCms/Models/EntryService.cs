namespace AtomicCms.Core.Models
{
    using System;
    using System.Collections.Generic;
    using Common.Abstract.DomainObjects;
    using Common.Abstract.Factories;
    using Common.Abstract.Models;
    using Common.CodeContract;
    using Common.Constants;
    using Common.Exceptions;
    using Common.Extensions;
    using DomainObjectsImp;
    using System.Linq;

    public class EntryService : IEntryService
    {
        private readonly IDaoFactory daoFactory;

        public EntryService(IDaoFactory factory)
        {
            Check.Argument.IsNotNull(factory,
                                     "factory");
            this.daoFactory = factory;
        }

        #region IEntryService Members

        public IEntry Load(int id)
        {
            try
            {
                return this.daoFactory.EntryDao.Load(id);
            }
            catch(EntryNotFoundException ex)
            {
                // todo: log this exception
                throw;
            }
        }

        public IEntry LoadDefault()
        {
            int parsedId = this.GetParsedDefaultPageId();

            return this.daoFactory.EntryDao.Load(parsedId);
        }

        private int GetParsedDefaultPageId()
        {
            ISiteAttributes attributes = this.daoFactory.SiteDao.LoadAttributes();
            string defaultPageIdFromDB = attributes.GetValue(Constant.Settings.DefaultPageId);
            int parsedId = ParseId(defaultPageIdFromDB);
            return parsedId;
        }

        internal static int ParseId(string defaultPageIdFromDB)
        {
            int parsedId;
            if (string.IsNullOrEmpty(defaultPageIdFromDB) || !Int32.TryParse(defaultPageIdFromDB,
                                                                             out parsedId))
            {
                throw new Exception("DefaultPageId attribute is not defined in settings table.");
            }
            return parsedId;
        }

        public void Save(IEntry entry)
        {
            Check.Argument.IsNotNull(entry,
                                     "entry");
            IEntry savedEntry = this.Load(entry.Id);
            entry.ModifiedAt = DateTime.Now;
            entry.Author = savedEntry.Author;

            this.daoFactory.EntryDao.Save(entry);
        }

        public void CreateEntry(IEntry entry)
        {
            Check.Argument.IsNotNull(entry,
                                     "entry");
            entry.ModifiedAt = DateTime.Now;
            entry.CreatedAt = DateTime.Now;
            entry.Author = User.GetCurrentUser();
            this.daoFactory.EntryDao.Save(entry);
        }

        public void Delete(int id)
        {
            Contract.Requires(id >= 0,
                              "Id must be greater or equal to zero.");
            this.daoFactory.EntryDao.Delete(id);
        }

        public ICollection<IEntry> LoadAll()
        {
            return this.daoFactory.EntryDao.LoadAll();
        }

        public ICollection<IEntry> LoadLast(int numberOfLastPages)
        {
            IEnumerable<IEntry> ret = this.LoadAll().OrderByDescending(x=>x.CreatedAt).Take(numberOfLastPages);
            int parsedId = this.GetParsedDefaultPageId();
            return TruncateEntryTitle(ret.OrderByDescending(x=>x.CreatedAt).Where(x => x.Id != parsedId)).ToList();
        }

        internal static IEnumerable<IEntry> TruncateEntryTitle(IEnumerable<IEntry> enumerable)
        {
            IEnumerable<IEntry> ret =  enumerable.Select((x) => Trunc(x));
            return ret;
        }

        internal static IEntry Trunc(IEntry entry)
        {
            entry.EntryTitle = entry.EntryTitle.Ellipsis(23);
            return entry;
        }

        #endregion
    }
}