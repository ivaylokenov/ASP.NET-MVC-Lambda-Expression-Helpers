namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class MvcExtensions
    {
        private const string ControllerSuffix = "Controller";

        public static string GetControllerName(this Type controllerType)
        {
            var typeName = controllerType.Name;
            return typeName.Substring(0, typeName.Length - ControllerSuffix.Length);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            var method = ((MethodCallExpression)actionExpression.Body).Method;

            var actionNameAttribute = method.GetCustomAttribute<ActionNameAttribute>();
            if (actionNameAttribute != null)
            {
                return actionNameAttribute.Name;
            }

            return method.Name;
        }

        public static MemberInfo GetMember(this LambdaExpression actionExpression)
        {
            var body = actionExpression.Body;
            if (body.NodeType == ExpressionType.Convert)
            {
                body = ((UnaryExpression)body).Operand;
            }

            var memberExpr = body as MemberExpression;

            if (memberExpr != null)
            {
                return memberExpr.Member;
            }

            var callExpr = body as MethodCallExpression;

            if (callExpr != null)
            {
                return callExpr.Method;
            }

            return null;
        }

        public static Type GetControllerType(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Object.Type;
        }

        public static string GetAreaName(this Type type)
        {
            string[] namespaces = (type.Namespace ?? string.Empty).ToLowerInvariant().Split('.');
            int areaIndex = GetAreaIndex(namespaces);
            if (areaIndex < 0)
            {
                return null;
            }

            return namespaces[areaIndex + 1];
        }

        private static int GetAreaIndex(IReadOnlyList<string> namespaces)
        {
            for (int i = 0; i < namespaces.Count; i++)
            {
                if (namespaces[i] == "areas")
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
