namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class RouteValueDictionaryExtensions
    {
        public static void AddRoutesFromExpression<TController>(this RouteValueDictionary routeValueDictionary,
            Expression<Action<TController>> action)
            where TController : Controller
        {
            if (routeValueDictionary == null)
            {
                return;
            }

            var type = typeof(TController);

            routeValueDictionary.ProcessArea(type);
            routeValueDictionary.ProcessController(type);
            routeValueDictionary.ProcessAction(action);
        }

        public static void ProcessArea(this RouteValueDictionary routeValues, Type targetControllerType)
        {
            var areaName = targetControllerType.GetAreaName() ?? string.Empty;
            if (!string.IsNullOrEmpty(areaName))
            {
                routeValues.AddOrUpdateRouteValue("area", areaName);
            }
        }

        public static void ProcessController(this RouteValueDictionary routeValues, Type targetControllerType)
        {
            string controllerName = targetControllerType.GetControllerName();
            routeValues.AddOrUpdateRouteValue("Controller", controllerName);
        }

        public static void ProcessAction<TController>(this RouteValueDictionary routeValues,
            Expression<Action<TController>> action)
            where TController : Controller
        {
            string actionName = action.GetActionName();
            routeValues.AddOrUpdateRouteValue("Action", actionName);
        }

        public static void AddOrUpdateRouteValue(this RouteValueDictionary routeValues, string key, string value)
        {
            if (routeValues.ContainsKey(key))
                routeValues[key] = value;
            else
                routeValues.Add(key, value);
        }
    }
}
