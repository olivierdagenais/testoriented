namespace AtomicCms.Web.Core.Extensions
{
    using System.Globalization;
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Mvc;
    using System.Web.UI;

    public static class LocalizationExtensions
    {
        public static string Resource(this HtmlHelper htmlhelper,
                                      string expression,
                                      params object[] args)

        {
            string virtualPath = GetVirtualPath(htmlhelper);


            return GetResourceString(htmlhelper.ViewContext.HttpContext, expression, virtualPath, args);
        }


        public static string Resource(this Controller controller,
                                      string expression,
                                      params object[] args)

        {
            return GetResourceString(controller.HttpContext, expression, "~/", args);
        }


        private static string GetResourceString(HttpContextBase httpContext,
                                                string expression,
                                                string virtualPath,
                                                object[] args)

        {
            ExpressionBuilderContext context = new ExpressionBuilderContext(virtualPath);

            ResourceExpressionBuilder builder = new ResourceExpressionBuilder();

            ResourceExpressionFields fields = (ResourceExpressionFields) builder
                                                                             .ParseExpression(expression,
                                                                                              typeof (string), context);


            if (!string.IsNullOrEmpty(fields.ClassKey))

                return string.Format((string) httpContext.GetGlobalResourceObject(
                                                  fields.ClassKey,
                                                  fields.ResourceKey,
                                                  CultureInfo.CurrentUICulture),
                                     args);


            return string.Format((string) httpContext.GetLocalResourceObject(
                                              virtualPath,
                                              fields.ResourceKey,
                                              CultureInfo.CurrentUICulture),
                                 args);
        }

        private static string GetVirtualPath(HtmlHelper htmlhelper)
        {
            string virtualPath = null;
            TemplateControl tc = htmlhelper.ViewDataContainer as TemplateControl;

            if (tc != null)
            {
                virtualPath = tc.AppRelativeVirtualPath;
            }

            return virtualPath;
        }
    }
}