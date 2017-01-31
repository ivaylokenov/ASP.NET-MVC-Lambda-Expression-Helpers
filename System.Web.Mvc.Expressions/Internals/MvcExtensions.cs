namespace System.Web.Mvc.Expressions.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Collections.Concurrent;

    public static class MvcExtensions
    {
        private const string ControllerSuffix = "Controller";

        private static ConcurrentDictionary<MethodInfo, string> ActionNameInfo = new ConcurrentDictionary<MethodInfo, string>();
        
        public static string GetActionName(this LambdaExpression actionExpression)
        {
            var methodCallExpression = actionExpression.Body as MethodCallExpression;
            if (methodCallExpression?.Object == null)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(actionExpression),
                    "Expected instance method call expression but received other type of expression instead.");
            }

            var actionMethod = methodCallExpression.Method;
            string result;
            if (ActionNameInfo.TryGetValue(actionMethod, out result))
            {
                return result;
            }
            
            var actionNameAttribute = actionMethod.GetCustomAttribute<ActionNameAttribute>();
            result = actionNameAttribute?.Name ?? actionMethod.Name;
            ActionNameInfo.TryAdd(actionMethod, result);
            return result;
        }

        public static string GetControllerName(this Type controllerType)
        {
            string typeName = controllerType.Name;
            return typeName.Substring(0, typeName.Length - ControllerSuffix.Length);
        }

        private static ConcurrentDictionary<Type, string> RouteAreaInfo = new ConcurrentDictionary<Type, string>();
        
        public static string GetAreaName(this Type type)
        {
            string result;
            if (RouteAreaInfo.TryGetValue(type, out result))
            {
                return result;
            }

            var routeAreaAttribute = type.GetCustomAttribute<RouteAreaAttribute>();
            if (routeAreaAttribute != null)
            {
                RouteAreaInfo.TryAdd(type, routeAreaAttribute.AreaName);
                return routeAreaAttribute.AreaName;
            }

            string[] namespaceParts = (type.Namespace ?? string.Empty).ToLowerInvariant().Split('.');
            int areaIndex = GetAreaIndex(namespaceParts);
            result = areaIndex < 0 ? string.Empty : namespaceParts[areaIndex + 1];
            RouteAreaInfo.TryAdd(type, result);
            return result;
        }

        private static int GetAreaIndex(IReadOnlyList<string> namespaceParts)
        {
            for (int i = 0; i < namespaceParts.Count; i++)
            {
                if (namespaceParts[i] == "areas")
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
