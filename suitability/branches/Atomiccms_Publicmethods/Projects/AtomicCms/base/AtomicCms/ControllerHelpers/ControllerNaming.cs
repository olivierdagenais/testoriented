namespace AtomicCms.Core.ControllerHelpers
{
    using System;
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    public static class ControllerNaming
    {
        public static string Action<T>(Expression<Action<T>> action)
        {
            MethodCallExpression methodCall = action.Body as MethodCallExpression;
            if (null != methodCall)
            {
                return methodCall.Method.Name;
            }

            return string.Empty;
        }

        public static string Name<T>()
        {
            Type t = typeof (T);
            Regex regex = new Regex("controller",
                                    RegexOptions.IgnoreCase);
            return regex.Replace(t.Name,
                                 "");

        }
    }
}