using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.ServiceModel.Syndication;

namespace AtomicCms.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web.Mvc;

    public interface IDomainObject
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }

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

    public interface IEntry : IDomainObject
    {
        string SeoTitle { get; set; }
        string EntryTitle { get; set; }
        string Alias { get; set; }
        string EntryBody { get; set; }
        IUser Author { get; set; }
        DateTime ModifiedAt { get; set; }
        string MetaDescription { get; set; }
        string MetaKeywords { get; set; }
    }

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

    public interface IMenu
    {
        ICollection<IMenuItem> MenuItems { get; set; }
        int Id { get; set; }
        string Title { get; set; }
        string Type { get; set; }
        string Description { get; set; }
    }

    public interface IMenuService
    {
        IMenu LoadMenu(int id);
        void SaveMenu(IMenu menu);
        IMenu LoadMenu(string menuName);
        ICollection<IMenu> LoadMenues();
        IMenuItem LoadMenuItem(int id);
        void SaveMenuItem(IMenuItem item);
    }

    public interface IEntryService
    {
        IEntry Load(int id);
        IEntry LoadDefault();
        void Save(IEntry entry);
        void CreateEntry(IEntry entry);
        void Delete(int id);
        ICollection<IEntry> LoadAll();
        ICollection<IEntry> LoadLast(int numberOfLastPages);
    }

    public interface ISiteAttributes
    {
        string GetValue(string key);
        IEnumerable<ISiteAttribute> Attributes { get; }
    }

    public interface ISiteAttribute : IDomainObject
    {
        string Key { get; set; }
        string Value { get; set; }
    }

    public interface ISiteService
    {
        ISiteAttributes LoadSiteAttributes();
        ISiteAttribute LoadSiteAttribute(int id);
        void Update(ISiteAttribute attribute);
    }

    public interface ISitemap : IList
    {
        void Serialize(TextWriter writer);
    }

    public interface IInfrastructureService
    {
        ISitemap CreateSitemap(Func<IEntry, string> generateEntryUrl, string homePage);
        SyndicationFeed BuildFeed(string websiteUrl, Func<IEntry, string> generateEntryUrl);
    }

    public interface IUserService
    {
        IUser Login(string userName, string password);
        ICollection<IUser> LoadAll();
        IUser Load(int id);
        void Update(IUser user);
    }

    public interface IFileUploadService
    {
        ICollection<FileInfo> GetFiles(string uploadsFolder);
    }

    public interface ISeoService
    {
        string CreateAlias(string source);
    }

    public interface IServiceFactory
    {
        IMenuService MenuService { get; }
        IEntryService EntryService { get; }
        ISiteService SiteService { get; }
        IInfrastructureService InfrastructureService { get; }
        IUserService UserService { get; }
        IFileUploadService FileUploadService { get; }
        ISeoService SeoService { get; }
    }

    public class Entry : IEntry
    {
        internal static readonly IEntry nullObject = new Entry
        {
            Alias = string.Empty,
            SeoTitle = string.Empty,
            EntryBody = string.Empty,
            EntryTitle = string.Empty,
            CreatedAt = DateTime.Now,
            Id = 0,
            MetaDescription = string.Empty,
            MetaKeywords = string.Empty,
            ModifiedAt = DateTime.Now,
            Author = null
        };

        public static IEntry NullObject
        {
            get { return nullObject; }
        }

        #region IEntry Members

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string SeoTitle { get; set; }
        public string EntryTitle { get; set; }
        public string Alias { get; set; }
        public string EntryBody { get; set; }
        public IUser Author { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        #endregion
    }

    public class MenuItemViewModel
    {
        public IMenuItem MenuItem { get; set; }
        public IList<SelectListItem> MenuTypes { get; set; }

    }
    public class MenuItem : IMenuItem
    {
        internal static readonly IMenuItem nullObject = new MenuItem
        {
            Entry = Controllers.Entry.NullObject,
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

    public class Check
    {
        internal Check()
        {
        }

        #region Nested type: Argument

        public class Argument
        {
            internal Argument()
            {
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty(Guid argument, string argumentName)
            {
                if (argument == Guid.Empty)
                {
                    throw new ArgumentException("\"{0}\" cannot be empty guid.".FormatWith(argumentName), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty(string argument, string argumentName)
            {
                if (string.IsNullOrEmpty(( argument ?? string.Empty ).Trim()))
                {
                    throw new ArgumentException("\"{0}\" cannot be blank.".FormatWith(argumentName), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotOutOfLength(string argument, int length, string argumentName)
            {
                if (argument.Trim().Length > length)
                {
                    throw new ArgumentException(
                        "\"{0}\" cannot be more than {1} character.".FormatWith(argumentName, length), argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(int argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(int argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(long argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(long argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(float argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(float argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(decimal argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(decimal argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotInPast(DateTime argument, string argumentName)
            {
                if (argument < DateTime.UtcNow)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotInFuture(DateTime argument, string argumentName)
            {
                if (argument > DateTime.UtcNow)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(TimeSpan argument, string argumentName)
            {
                if (argument < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(TimeSpan argument, string argumentName)
            {
                if (argument <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotEmpty<T>(ICollection<T> argument, string argumentName)
            {
                IsNotNull(argument, argumentName);

                if (argument.Count == 0)
                {
                    throw new ArgumentException("Collection cannot be empty.", argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotOutOfRange(int argument, int min, int max, string argumentName)
            {
                if (( argument < min ) || ( argument > max ))
                {
                    throw new ArgumentOutOfRangeException(argumentName,
                                                          "{0} must be between \"{1}\"-\"{2}\".".FormatWith(
                                                              argumentName, min, max));
                }
            }
        }

        #endregion
    }

    public static class StringExtension
    {
        [DebuggerStepThrough]
        public static string NullSafe(this string target)
        {
            return ( target ?? string.Empty ).Trim();
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            Check.Argument.IsNotEmpty(target, "target");

            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static string Ellipsis(this string value, int maxLength)
        {
            const string suffix = "...";

            if (value == null)
                return null;

            string s = value;

            if (string.IsNullOrEmpty(s) || s.Length <= maxLength || s.Length < suffix.Length)
                return s;

            return s.Substring(0, maxLength - suffix.Length) + suffix;
        }
    }

    [Authorize]
    public class AdminMenuItemController : Controller
    {
        private readonly IServiceFactory serviceFactory;

        public AdminMenuItemController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        public ViewResult EditMenuItem(int? id)
        {
            List<SelectListItem> menuTypes = this.GetMenuTypes();

            if (null != id)
            {
                IMenuItem menuItem = this.serviceFactory.MenuService.LoadMenuItem(id.Value);
                if (menuItem.Entry == null)
                {
                    menuItem.Entry = Entry.NullObject;
                }

                return View(new MenuItemViewModel()
                                {
                                    MenuItem = menuItem,
                                    MenuTypes = menuTypes
                                });
            }

            // load empty screen
            return View(new MenuItemViewModel()
                            {
                                MenuItem = MenuItem.NullObject,
                                MenuTypes = menuTypes
                            });
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult EditMenuItem(int? id, MenuItem menuItem, FormCollection form)
        {
            this.LoadEntryToCurrentMenuItem(form,
                                            menuItem);

            menuItem.NavigateUrl = menuItem.NavigateUrl.NullSafe();
            this.serviceFactory.MenuService.SaveMenuItem(menuItem);
            this.FormatResultMessage(menuItem,
                                id);

            return RedirectToAction("EditMenuItem",
                                    "AdminMenuItem",
                                    new {id = menuItem.Id});
        }

        private void FormatResultMessage(IMenuItem menuItem, int? id)
        {
            TempData["SaveResult"] = InnerFormatResultMessage(id, menuItem.Id);
        }

        internal static string InnerFormatResultMessage(int? id, int menuItemId)
        {
            string result;
            if (null == id || 0 == id)
            {
                result = string.Format("Items was successfully created with Id = {0}",
                                       menuItemId);
            }
            else
            {
                result = "Items was successfully updated";
            }
            return result;
        }

        private void LoadEntryToCurrentMenuItem(NameValueCollection form, IMenuItem menuItem)
        {
            string entryId = form["MenuItem.EntryId"];
            if (!string.IsNullOrEmpty(entryId))
            {
                int parsedEntryId = Int32.Parse(entryId);
                menuItem.Entry = this.serviceFactory.EntryService.Load(parsedEntryId);
            }
        }


        private List<SelectListItem> GetMenuTypes()
        {
            ICollection<IMenu> menues = this.serviceFactory.MenuService.LoadMenues();
            List<SelectListItem> menuTypes = new List<SelectListItem>();
            foreach (IMenu menu in menues)
            {
                menuTypes.Add(new SelectListItem() {Text = menu.Type, Value = menu.Id.ToString()});
            }
            return menuTypes;
        }
    }
}