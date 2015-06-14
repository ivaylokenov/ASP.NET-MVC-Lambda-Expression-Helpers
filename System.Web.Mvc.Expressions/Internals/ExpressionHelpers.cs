namespace System.Web.Mvc.Expressions.Internals
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExpressionHelpers
    {
        public static string GetExpressionText(LambdaExpression expression)
        {
            var result = string.Empty;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    result = string.Join(".", GetProperties(unaryExpression.Operand));
                }
            }
            else
            {
                result = ExpressionHelper.GetExpressionText(expression);
            }

            return result;
        }

        public static bool IsSingleArgumentIndexer(MethodCallExpression methodExpression)
        {
            var result = methodExpression != null &&
                methodExpression.Arguments.Count == 1 &&
                methodExpression.Method.DeclaringType
                    .GetDefaultMembers()
                    .OfType<PropertyInfo>()
                    .Any(p => p.GetGetMethod() == methodExpression.Method);

            return result;
        }

        private static IEnumerable<string> GetProperties(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                var methodExpression = expression as MethodCallExpression;
                if (methodExpression != null && IsSingleArgumentIndexer(methodExpression))
                {
                    var indexerMemberExpression = methodExpression.Object as MemberExpression;
                    if (indexerMemberExpression != null)
                    {
                        var index = methodExpression.Arguments
                            .Select(arg => Expression.Convert(arg, typeof(object)))
                            .Select(exp => Expression.Lambda<Func<object>>(exp, null).Compile()())
                            .First();

                        var propertyName = string.Format(
                                "{0}.{1}[{2}]",
                                string.Join(".", GetProperties(indexerMemberExpression.Expression)),
                                indexerMemberExpression.Member.Name,
                                index);

                        yield return propertyName.TrimStart('.');
                    }
                }

                yield break;
            }

            foreach (var propertyInfo in GetProperties(memberExpression.Expression))
            {
                yield return propertyInfo;
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property != null)
            {
                yield return property.Name;
            }
        }
    }
}
