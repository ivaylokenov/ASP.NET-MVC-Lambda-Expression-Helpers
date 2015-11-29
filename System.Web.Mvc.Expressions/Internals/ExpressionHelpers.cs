namespace System.Web.Mvc.Expressions.Internals
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExpressionHelpers
    {
        public static object GetArgumentValue(Expression argumentExpression)
        {
            object argumentValue;
            if (argumentExpression.NodeType == ExpressionType.Constant)
            {
                argumentValue = ((ConstantExpression)argumentExpression).Value;
            }
            else
            {
                var typeConversionExpression = Expression.Convert(argumentExpression, typeof(object));
                argumentValue = Expression.Lambda<Func<object>>(typeConversionExpression, null).Compile().Invoke();
            }

            return argumentValue;
        }

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

        public static void ValidatePropertyOrIndexerExpression(LambdaExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var isValid = false;

            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var unaryExpression = expression.Body as UnaryExpression;

                isValid = unaryExpression != null &&
                    (unaryExpression.Operand is MemberExpression ||
                        (unaryExpression.Operand is MethodCallExpression &&
                        IsSingleArgumentIndexer((MethodCallExpression)unaryExpression.Operand)));
            }
            else if (expression.Body.NodeType == ExpressionType.Call)
            {
                var methodExpression = expression.Body as MethodCallExpression;

                isValid = IsSingleArgumentIndexer(methodExpression);
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = expression.Body as MemberExpression;

                isValid = memberExpression?.Member is PropertyInfo;
            }

            if (!isValid)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(expression),
                    $"{nameof(expression)} should refer to a property or indexer.");
            }
        }

        private static bool IsSingleArgumentIndexer(MethodCallExpression methodExpression)
        {
            if (methodExpression != null && methodExpression.Arguments.Count == 1)
            {
                var methodDeclaringTypeDefaultMembers = methodExpression.Method.DeclaringType.GetDefaultMembers();
                for (int i = 0; i < methodDeclaringTypeDefaultMembers.Length; i++)
                {
                    var property = methodDeclaringTypeDefaultMembers[i] as PropertyInfo;
                    if (property?.GetGetMethod() == methodExpression.Method)
                    {
                        return true;
                    }
                }
            }

            return false;
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
                        var index = GetArgumentValue(methodExpression.Arguments[0]);

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
