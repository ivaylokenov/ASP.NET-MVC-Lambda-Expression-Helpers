namespace System.Web.Mvc.Expressions
{
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelError<TModel, TProperty>(
            this ModelStateDictionary modelStateDictionary,
            Expression<Func<TModel, TProperty>> keyExpression,
            string errorMessage)
        {
            ValidateKeyExpression(keyExpression);

            var key = ExpressionHelper.GetExpressionText(keyExpression);
            var modelState = GetModelStateForKey(modelStateDictionary, key);
            modelState.Errors.Add(errorMessage);
        }

        public static void AddModelError<TModel, TProperty>(
            this ModelStateDictionary modelStateDictionary,
            Expression<Func<TModel, TProperty>> keyExpression,
            Exception exception)
        {
            ValidateKeyExpression(keyExpression);

            var key = ExpressionHelper.GetExpressionText(keyExpression);
            var modelState = GetModelStateForKey(modelStateDictionary, key);
            modelState.Errors.Add(exception);
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

        private static void ValidateKeyExpression<TModel, TProperty>(Expression<Func<TModel, TProperty>> keyExpression)
        {
            if (keyExpression == null)
            {
                throw new ArgumentNullException("keyExpression");
            }

            var methodExpression = keyExpression.Body as MethodCallExpression;
            if (methodExpression == null)
            {
                var memberExpression = keyExpression.Body as MemberExpression;
                if (memberExpression == null)
                {
                    throw new ArgumentOutOfRangeException("keyExpression", "keyExpression should refer to a model member.");
                }

                if (!(memberExpression.Member is PropertyInfo))
                {
                    throw new ArgumentOutOfRangeException("keyExpression", "keyExpression should refer to a model property.");
                }
            }
            else if (!IsSingleArgumentIndexer(methodExpression))
            {
                throw new ArgumentOutOfRangeException("keyExpression", "keyExpression should refer to a model member or indexer.");
            }
        }

        private static bool IsSingleArgumentIndexer(MethodCallExpression methodExpression)
        {
            var result = methodExpression.Arguments.Count == 1 &&
                methodExpression.Method.DeclaringType
                    .GetDefaultMembers()
                    .OfType<PropertyInfo>()
                    .Any(p => p.GetGetMethod() == methodExpression.Method);

            return result;
        }
    }
}
