namespace AtomicCms.Web.Core.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Common.Attributes;

    public class SkinSupportViewEngine : WebFormViewEngine
    {
        public SkinSupportViewEngine()
        {
            string[] mastersLocation = new[]
                                           {
                                               string.Format("~/skins/{0}/{0}.master",
                                                             Utils.SkinName)
                                           };
            MasterLocationFormats = this.AddNewLocationFormats(new List<string>(MasterLocationFormats),
                                                               mastersLocation);

            string[] viewsLocation = new[]
                                         {
                                             string.Format("~/skins/{0}/Views/{{1}}/{{0}}.aspx",
                                                           Utils.SkinName),
                                             string.Format("~/skins/{0}/Views/{{1}}/{{0}}.ascx",
                                                           Utils.SkinName)
                                         };
            ViewLocationFormats =
                PartialViewLocationFormats = this.AddNewLocationFormats(new List<string>(ViewLocationFormats),
                                                                        viewsLocation);
        }

        internal string[] AddNewLocationFormats(IEnumerable<string> defaultLocationFormats,
                                               IEnumerable<string> newLocationFormats)
        {
            List<string> allItems = new List<string>(newLocationFormats);
            foreach (string s in defaultLocationFormats)
            {
                allItems.Add(s);
            }

            return allItems.ToArray();
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext,
                                                  string viewName,
                                                  string masterName,
                                                  bool useCache)
        {
            masterName = this.OverrideMasterPage(masterName,
                                                 controllerContext);
            return base.FindView(controllerContext,
                                 viewName,
                                 masterName,
                                 useCache);
        }

        internal string OverrideMasterPage(string masterName, ControllerContext controllerContext)
        {
            if (this.NeedChangeMasterPage(controllerContext))
            {
                masterName = Utils.SkinName;
            }

            return masterName;
        }

        internal bool NeedChangeMasterPage(ControllerContext context)
        {
            SupportSkinAttribute attr = Attribute.GetCustomAttribute(context.Controller.GetType(),
                                                                     typeof (SupportSkinAttribute)) as
                                        SupportSkinAttribute;
            return null != attr;
        }
    }
}