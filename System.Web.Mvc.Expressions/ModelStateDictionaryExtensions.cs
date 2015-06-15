namespace System.Web.Mvc.Expressions
{
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Mvc.Expressions.Internals;

    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelError<TModel>(
            this ModelStateDictionary modelStateDictionary,
            Expression<Func<TModel, object>> keyExpression,
            string errorMessage)
        {
            ValidateKeyExpression(keyExpression);

            var key = ExpressionHelpers.GetExpressionText(keyExpression);
            var modelState = GetModelStateForKey(modelStateDictionary, key);
            modelState.Errors.Add(errorMessage);
        }

        public static void AddModelError<TModel>(
            this ModelStateDictionary modelStateDictionary,
            Expression<Func<TModel, object>> keyExpression,
            Exception exception)
        {
            ValidateKeyExpression(keyExpression);

            var key = ExpressionHelpers.GetExpressionText(keyExpression);
            var modelState = GetModelStateForKey(modelStateDictionary, key);
            modelState.Errors.Add(exception);
        }

        private static void ValidateKeyExpression(LambdaExpression keyExpression)
        {
            if (keyExpression == null)
            {
                throw new ArgumentNullException("keyExpression");
            }

            var isValid = false;

            if (keyExpression.Body.NodeType == ExpressionType.Convert)
            {
                var unaryExpression = keyExpression.Body as UnaryExpression;

                isValid = unaryExpression != null &&
                    (unaryExpression.Operand is MemberExpression ||
                        (unaryExpression.Operand is MethodCallExpression &&
                        ExpressionHelpers.IsSingleArgumentIndexer((MethodCallExpression)unaryExpression.Operand)));
            }
            else if (keyExpression.Body.NodeType == ExpressionType.Call)
            {
                var methodExpression = keyExpression.Body as MethodCallExpression;

                isValid = ExpressionHelpers.IsSingleArgumentIndexer(methodExpression);
            }
            else if (keyExpression.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = keyExpression.Body as MemberExpression;

                isValid = memberExpression != null && memberExpression.Member is PropertyInfo;
            }

            if (!isValid)
            {
                throw new ArgumentOutOfRangeException("keyExpression", "keyExpression should refer to a member property or indexer.");
            }
        }

        private static ModelState GetModelStateForKey(ModelStateDictionary modelStateDictionary, string key)
        {
            ModelState modelState;
            if (!modelStateDictionary.TryGetValue(key, out modelState))
            {
                modelState = new ModelState();
                modelStateDictionary[key] = modelState;
            }

            return modelState;
        }
    }
}
