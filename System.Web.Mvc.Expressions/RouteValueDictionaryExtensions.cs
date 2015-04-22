namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteValueDictionaryExtensions
    {
        public static void AddRoutesFromExpression<TController>(
            this RouteValueDictionary routeValueDictionary,
            Expression<Action<TController>> action) where TController : Controller
        {
            if (routeValueDictionary == null)
            {
                return;
            }

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

        public static void ProcessAction<TController>(this RouteValueDictionary routeValues, Expression<Action<TController>> action)
            where TController : Controller
        {
            string actionName = action.GetActionName();
            routeValues.AddOrUpdateRouteValue("Action", actionName);
        }

        public static void ProcessParameters<TController>(this RouteValueDictionary routeValues, Expression<Action<TController>> action)
            where TController : Controller
        {
            var method = action.Body as MethodCallExpression;
            var argsNames = method.Method
                .GetParameters()
                .Select(p => p.Name)
                .ToList();

            var args = method.Arguments
                .Select(arg => Expression.Convert(arg, typeof(object)))
                .Select(exp => Expression.Lambda<Func<object>>(exp, null).Compile()())
                .ToList();

            for (int i = 0; i < argsNames.Count; i++)
            {
                routeValues.AddOrUpdateRouteValue(argsNames[i], args[i]);
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

        public static string ValuesToString(this RouteValueDictionary routeValues)
        {
            var result = new StringBuilder();
            foreach (var key in routeValues.Keys)
            {
                result.Append(string.Format("{0}:{1}", key, routeValues[key]));
            }

            return result.ToString();
        }
    }
}
