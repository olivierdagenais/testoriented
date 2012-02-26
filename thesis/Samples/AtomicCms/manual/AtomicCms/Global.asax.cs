namespace AtomicCms.Web
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Common.Constants;
    using Common.Extensions;
    using Core.DI;
    using Core.Extensions;
    using Core.Mvc;
    using Core.Routes;

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            AdminRoutes(routes);
            EntryRoutes(routes);
            AccountRoutes(routes);
            InfrastructureRoutes(routes);

/*
            routes.MapRoute("catch-all",
                            "{*url}",
                            new {controller = "Home", action = "PageNotFound"});
*/
        }

        private static void InfrastructureRoutes(RouteCollection routes)
        {
            // remove this route after april 2009 (it's in use with google and yandex now)
            routes.MapRoute("infrastructure-sitemap-without-extension-remove-it",
                            "sitemap/",
                            new {controller = "Infrastructure", action = "Sitemap"});
            routes.MapRoute("infrastructure-sitemap",
                            "sitemap.xml",
                            new {controller = "Infrastructure", action = "Sitemap"});

            // rss routes
            routes.MapRoute("rss-route", "rss/", new {controller = "Infrastructure", action = "Rss"});
            routes.MapRoute("atom-route", "atom/", new {controller = "Infrastructure", action = "Atom"});
        }

        private static void AccountRoutes(RouteCollection routes)
        {
            routes.MapRoute("account",
                            "account/{action}",
                            new {controller = "Account"});
        }

        private static void EntryRoutes(RouteCollection routes)
        {
            EntryRoute entryRoute = new EntryRoute("{name}-{id}/",
                                                   new RouteValueDictionary(
                                                       new
                                                           {
                                                               controller = "Home",
                                                               action = "Content",
                                                               id = 0,
                                                               name = string.Empty
                                                           }),
                                                   new RouteValueDictionary(new {id = @"\d+"}),
                                                   new MvcRouteHandler());

            routes.Add("display-entry",
                       entryRoute);

            routes.MapRoute("default-entry",
                            string.Empty,
                            new {controller = "Home", action = "Default"});

            routes.MapRoute("menu-route",
                            "menu/{action}",
                            new {controller = "Menu"});

            routes.MapRoute("home-route",
                            "Home/{action}",
                            new {controller = "Home"});
        }

        private static void AdminRoutes(RouteCollection routes)
        {
            routes.MapRoute("edit-entry",
                            "Admin/EditEntry/{id}",
                            new {controller = "AdminEntry", action = "EditEntry", id = string.Empty});

            routes.MapRoute("delete-entry",
                            "Admin/DeleteEntry/{id}",
                            new {controller = "AdminEntry", action = "Delete", id = string.Empty});

            routes.MapRoute("edit-menu",
                            "admin/editmenu/{id}",
                            new {controller = "AdminMenu", action = "EditMenu", id = string.Empty});

            routes.MapRoute("edit-menu-item",
                            "admin/editmenuitem/{id}",
                            new { controller = "AdminMenuItem", action = "EditMenuItem", id = UrlParameter.Optional });

            routes.MapRoute("menu-list",
                            "admin/MenuList/",
                            new {controller = "AdminMenu", action = "MenuList"});

            routes.MapRoute("list-entry",
                            "admin/EntryList/",
                            new {controller = "AdminEntry", action = "EntryList"});

            routes.MapRoute("dashboard",
                            "admin/",
                            new {controller = "Admin", action = "Dashboard"});

            routes.MapRoute("admin-user-controller",
                            "adminuser/{action}/{id}",
                            new {controller = "AdminUser", action = "Index", id = string.Empty});

            routes.MapRoute("admin-settings-controller",
                            "AdminSettings/{action}/{id}",
                            new {controller = "AdminSettings", action = "Index", id = string.Empty});

            routes.MapRoute("file-upload",
                            "admin/FileUpload/{action}/{id}",
                            new {controller = "FileUpload", action = "Index", id = string.Empty});
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new SkinSupportViewEngine());
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            this.SeoRedirects();
        }

        private void SeoRedirects()
        {
            if (Request.HttpMethod.Equals("POST",
                                          StringComparison.InvariantCultureIgnoreCase))
            {
                // seo rules can be applyed only to GET requests
                return;
            }

            string authority = HttpContext.Current.Request.Url.Authority;
            string scheme = Request.Url.Scheme;
            string absolutePath = HttpContext.Current.Request.Url.AbsolutePath;
            string query = HttpContext.Current.Request.Url.Query;

            string url = (scheme + "://" + authority +
                          absolutePath);
/*
            string newUrl;

            if (this.RemoveWWWPrefix(scheme,
                                     authority,
                                     absolutePath,
                                     query,
                                     out newUrl))
            {
                this.PermanentRedirect(newUrl + query);
            }
*/

            if (this.RemoveDoubleSlashes(absolutePath))
            {
                this.PermanentRedirect(url.Replace("//",
                                                   "/") + query);
            }

            if (this.AddTrailingSlash(absolutePath))
            {
                this.PermanentRedirect(url + "/" + query);
            }
        }

        public bool RemoveDoubleSlashes(string absolutePath)
        {
            return InnerRemoveDoubleSlashes(absolutePath);
        }

        internal static bool InnerRemoveDoubleSlashes(string absolutePath)
        {
            // If we have double-slashes, strip them out
            if (absolutePath.Contains("//"))
            {
                return true;
            }

            return false;
        }

        public bool RemoveWWWPrefix(string scheme,
                                    string authority,
                                    string absolutePath,
                                    string query,
                                    out string newUrl)
        {
            newUrl = RemoveWWWPrefix(scheme, authority, absolutePath, query);
            return newUrl != null;
        }

        internal static string RemoveWWWPrefix(string scheme,
                                    string authority,
                                    string absolutePath,
                                    string query)
        {
            string newUrl = null;
            if (authority.StartsWith("www.",
                                     StringComparison.InvariantCultureIgnoreCase))
            {
                newUrl = (scheme + "://" + authority.Remove(0,
                                                            4) +
                          absolutePath);
            }

            return newUrl;
        }

        public void PermanentRedirect(string url)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.PermanentRedirect(url);
        }

        public bool AddTrailingSlash(string absolutePath)
        {
            return InnerAddTrailingSlash(absolutePath);
        }

        internal static bool InnerAddTrailingSlash(string absolutePath)
        {
            absolutePath = absolutePath.NullSafe();
            return !absolutePath.Contains(".") && !absolutePath.EndsWith("/");
        }
    }
}