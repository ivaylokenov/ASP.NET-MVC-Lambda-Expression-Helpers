namespace System.Web.Mvc.Expressions.Internals
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    internal static class RouteValueDictionaryExtensions
    {
        public static void AddRouteValuesFromExpression<TController>(
                this RouteValueDictionary routeValueDictionary,
                Expression<Action<TController>> action)
            where TController : Controller
        {
            var type = typeof(TController);

            routeValueDictionary.ProcessArea(type);
            routeValueDictionary.ProcessController(type);
            routeValueDictionary.ProcessAction(action);
            routeValueDictionary.ProcessParameters(action);
        }

        public static void ProcessArea(this RouteValueDictionary routeValues, Type targetControllerType)
        {
            string areaName = targetControllerType.GetAreaName() ?? string.Empty;
            routeValues.AddOrUpdateRouteValue("area", areaName);
        }

        public static void ProcessController(this RouteValueDictionary routeValues, Type targetControllerType)
        {
            string controllerName = targetControllerType.GetControllerName();
            routeValues.AddOrUpdateRouteValue("Controller", controllerName);
        }

        public static void ProcessAction<TController>(
                this RouteValueDictionary routeValues,
                Expression<Action<TController>> action)
            where TController : Controller
        {
            string actionName = action.GetActionName();
            routeValues.AddOrUpdateRouteValue("Action", actionName);
        }

        public static void ProcessParameters<TController>(
                this RouteValueDictionary routeValues,
                Expression<Action<TController>> action)
            where TController : Controller
        {
            var methodCallExpression = action.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(action),
                    "Expected method call expression but received other type of expression instead.");
            }

            var methodParameters = methodCallExpression.Method.GetParameters();
            for (int i = 0; i < methodParameters.Length; i++)
            {
                var argumentValue = ExpressionHelpers.GetArgumentValue(methodCallExpression.Arguments[i]);

                routeValues.AddOrUpdateRouteValue(methodParameters[i].Name, argumentValue);
            }
        }

        public static void AddOrUpdateRouteValue(this RouteValueDictionary routeValues, string key, object value)
        {
            if (routeValues.ContainsKey(key))
            {
                routeValues[key] = value;
            }
            else
            {
                routeValues.Add(key, value);
            }
        }
    }
}
