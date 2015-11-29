namespace System.Web.Mvc.Expressions
{
    using System.Linq.Expressions;
    using System.Web.Mvc.Expressions.Internals;

    public static class ModelStateDictionaryExtensions
    {
        public static void AddModelError<TModel>(
            this ModelStateDictionary modelStateDictionary,
            Expression<Func<TModel, object>> keyExpression,
            string errorMessage)
        {
            ExpressionHelpers.ValidatePropertyOrIndexerExpression(keyExpression);

            var key = ExpressionHelpers.GetExpressionText(keyExpression);
            var modelState = GetModelStateForKey(modelStateDictionary, key);
            modelState.Errors.Add(errorMessage);
        }

        public static void AddModelError<TModel>(
            this ModelStateDictionary modelStateDictionary,
            Expression<Func<TModel, object>> keyExpression,
            Exception exception)
        {
            ExpressionHelpers.ValidatePropertyOrIndexerExpression(keyExpression);

            var key = ExpressionHelpers.GetExpressionText(keyExpression);
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
    }
}
