using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SoftwareNinjas.Core;

namespace SoftwareNinjas.PublicInterfaceComparer
{
    /// <summary>
    /// Scans the public interface of an <see cref="Assembly"/> instance.
    /// </summary>
    public class PublicInterfaceScanner
    {
        /// <summary>
        /// The path to the baseline <see cref="Assembly"/>.
        /// </summary>
        public FileInfo BaselineFile
        {
            get;
            set;
        }

        internal void Execute()
        {
            var baselineFullPath = BaselineFile.FullName;
            var baseline = Assembly.LoadFile(baselineFullPath);
        }

        internal static IEnumerable<Type> GetVisibleTypes(Assembly assembly)
        {
            return assembly.GetTypes().Filter(t => t.IsVisible);
        }

        internal static IEnumerable<MemberInfo> GetVisibleMembers(Type type)
        {
            var unfiltered = type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic
                                             | BindingFlags.Instance | BindingFlags.Static);
            var result = unfiltered.Filter(IsVisible);
            return result;
        }

        internal static bool IsVisible(MemberInfo memberInfo)
        {
            if (memberInfo is MethodBase)
            {
                return IsVisible((MethodBase) memberInfo);
            }
            if (memberInfo is FieldInfo)
            {
                return IsVisible((FieldInfo) memberInfo);
            }
            if (memberInfo is EventInfo)
            {
                return true;
            }
            if (memberInfo is Type)
            {
                return IsVisible((Type) memberInfo);
            }
            if (memberInfo is PropertyInfo)
            {
                // we aren't processing them anyway; we instead focus on associated accessor methods
                return false;
            }
            throw new ArgumentException("Parameter 'memberInfo' is an unsupported type.", "memberInfo");
        }

        internal static bool IsVisible(Type type)
        {
            return type.IsNested && (type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamORAssem);
        }

        internal static bool IsVisible(MethodBase methodBase)
        {
            return IsProtected(methodBase) || IsPublic(methodBase);
        }

        internal static bool IsProtected(MethodBase methodBase)
        {
            return methodBase.IsFamily || methodBase.IsFamilyOrAssembly;
        }

        internal static bool IsPublic(MethodBase methodBase)
        {
            return methodBase.IsPublic;
        }

        internal static bool IsVisible(FieldInfo fieldInfo)
        {
            return fieldInfo.IsPublic || fieldInfo.IsFamily || fieldInfo.IsFamilyOrAssembly;
        }

        internal static string Describe(MethodBase methodBase)
        {
            return "{0} {1}".FormatInvariant(Describe(methodBase.ReflectedType), methodBase.ToString());
        }

        internal static string Describe(Type type)
        {
            return type.FullName;
        }

        internal static string Describe(EventInfo eventInfo)
        {
            return "{0} {1}".FormatInvariant(Describe(eventInfo.ReflectedType), eventInfo.ToString());
        }

        internal static string Describe(FieldInfo fieldInfo)
        {
            return "{0} {1}".FormatInvariant(Describe(fieldInfo.ReflectedType), fieldInfo.ToString());
        }

        internal static string Describe(MemberInfo memberInfo)
        {
            if (memberInfo is MethodBase)
            {
                return Describe((MethodBase) memberInfo);
            }
            if (memberInfo is EventInfo)
            {
                return Describe((EventInfo) memberInfo);
            }
            if (memberInfo is FieldInfo)
            {
                return Describe((FieldInfo) memberInfo);
            }
            if (memberInfo is Type)
            {
                return Describe((Type) memberInfo);
            }
            throw new ArgumentException("Unsupported MemberInfo instance", "memberInfo");
        }
    }
}
