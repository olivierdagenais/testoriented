using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AtomicCms.Common.Abstract.DomainObjects;
using AtomicCms.Common.Abstract.Factories;
using AtomicCms.Common.Abstract.Models;
using AtomicCms.Common.Abstract.Services;
using AtomicCms.Core.DomainObjectsImp;
using NUnit.Framework;
using AtomicCms.Web.Controllers;

namespace AtomicCms.ManualTests.Web.Controllers
{
    /// <summary>
    /// A class to test <see cref="AdminMenuItemController"/>.
    /// </summary>
    [TestFixture]
    public class AdminMenuItemControllerTest
    {
        internal class TestServiceFactory : IServiceFactory
        {
            public IMenuService MenuService { get; set; }

            public IEntryService EntryService { get; set; }

            public ISiteService SiteService { get; set; }

            public IInfrastructureService InfrastructureService { get; set; }

            public IUserService UserService { get; set; }

            public IFileUploadService FileUploadService { get; set; }

            public ISeoService SeoService { get; set; }
        }

        internal class MenuServiceMole : IMenuService
        {
            #region IMenuService Moles

            public Func<int, IMenu> LoadMenuInt { get; set; }

            public Action<IMenu> SaveMenu { get; set; }

            public Func<string, IMenu> LoadMenuString { get; set; }

            public Func<ICollection<IMenu>> LoadMenues { get; set; }

            public Func<int, IMenuItem> LoadMenuItem { get; set; }

            public Action<IMenuItem> SaveMenuItem { get; set; }

            #endregion

            #region Explicit IMenuService Members

            IMenu IMenuService.LoadMenu(int id)
            {
                if (LoadMenuInt == null)
                {
                    throw new NotImplementedException();
                }
                return LoadMenuInt(id);
            }

            void IMenuService.SaveMenu(IMenu menu)
            {
                if (SaveMenu == null)
                {
                    throw new NotImplementedException();
                }
                SaveMenu(menu);
            }

            IMenu IMenuService.LoadMenu(string menuName)
            {
                if (LoadMenuString == null)
                {
                    throw new NotImplementedException();
                }
                return LoadMenuString(menuName);
            }

            ICollection<IMenu> IMenuService.LoadMenues()
            {
                if (LoadMenues == null)
                {
                    throw new NotImplementedException();
                }
                return LoadMenues();
            }

            IMenuItem IMenuService.LoadMenuItem(int id)
            {
                if (LoadMenuItem == null)
                {
                    throw new NotImplementedException();
                }
                return LoadMenuItem(id);
            }

            void IMenuService.SaveMenuItem(IMenuItem item)
            {
                if (SaveMenuItem == null)
                {
                    throw new NotImplementedException();
                }
                SaveMenuItem(item);
            }

            #endregion
        }

[Test]
public void FormatResultMessage_Created()
{
    // arrange
    var serviceFactory = new TestServiceFactory
    {
        MenuService = new MenuServiceMole
        {
            SaveMenuItem = i => { },
        },
    };
    var iut = new AdminMenuItemController(serviceFactory);
    var item = new MenuItem
    {
        Id = 42,
    };
    var formCollection = new FormCollection();

    // act - EditMenuItem eventually calls FormatResultMessage
    iut.EditMenuItem(null, item, formCollection);

    // assert
    var actual = iut.TempData["SaveResult"];
    Assert.AreEqual("Items was successfully created with Id = 42", actual);
}

        [Test]
        public void FormatResultMessage_Updated()
        {
            // arrange
            var serviceFactory = new TestServiceFactory
            {
                MenuService = new MenuServiceMole
                {
                    SaveMenuItem = i => { },
                },
            };
            var iut = new AdminMenuItemController(serviceFactory);
            var item = new MenuItem
            {
                Id = 42,
            };
            var formCollection = new FormCollection();

            // act - EditMenuItem eventually calls FormatResultMessage
            iut.EditMenuItem(42, item, formCollection);

            // assert
            var actual = iut.TempData["SaveResult"];
            Assert.AreEqual("Items was successfully updated", actual);
        }
    }
}
