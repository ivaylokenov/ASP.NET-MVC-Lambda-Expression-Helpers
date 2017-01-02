namespace System.Web.Mvc.Expressions.Internals
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class MvcExtensions
    {
        private const string ControllerSuffix = "Controller";

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

            var actionNameAttribute = actionMethod.GetCustomAttribute<ActionNameAttribute>();
            if (actionNameAttribute == null)
            {
                return actionMethod.Name;
            }

            return actionNameAttribute.Name;
        }

        public static string GetControllerName(this Type controllerType)
        {
            string typeName = controllerType.Name;
            return typeName.Substring(0, typeName.Length - ControllerSuffix.Length);
        }

        public static string GetAreaName(this Type type)
        {
            var routeAreaAttribute = type.GetCustomAttribute<RouteAreaAttribute>();
            if (routeAreaAttribute != null)
            {
                return routeAreaAttribute.AreaPrefix;
            }

            string[] namespaceParts = (type.Namespace ?? string.Empty).ToLowerInvariant().Split('.');
            int areaIndex = GetAreaIndex(namespaceParts);
            if (areaIndex < 0)
            {
                return string.Empty;
            }

            return namespaceParts[areaIndex + 1];
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
