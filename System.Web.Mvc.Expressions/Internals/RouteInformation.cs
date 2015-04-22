namespace System.Web.Mvc.Expressions.Internals
{
    using System;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    internal class RouteInformation
    {
        public static RouteInformation FromExpression<TController>(Expression<Action<TController>> action, object routeValues = null)
            where TController : Controller
        {
            return GetFromCache(action, routeValues);
        }

        private static RouteInformation GetFromCache<TController>(Expression<Action<TController>> action, object routeValues = null)
            where TController : Controller
        {
            var routeValueDict = routeValues == null ? new RouteValueDictionary() : new RouteValueDictionary(routeValues);
            routeValueDict.ProcessParameters(action);
            var controllerType = typeof(TController);

            var expressionAsString = string.Format("{0}{1}{2}", controllerType.FullName, action, routeValueDict.ValuesToString());
            if (HttpRuntime.Cache[expressionAsString] != null)
            {
                return HttpRuntime.Cache[expressionAsString] as RouteInformation;
            }

            string controllerName = controllerType.GetControllerName();
            string actionName = action.GetActionName();
            routeValueDict.ProcessArea(controllerType);

            var routeInformation = new RouteInformation
            {
                ActionName = actionName,
                ControllerName = controllerName,
                RouteValueDictionary = routeValueDict
            };

            HttpRuntime.Cache.Insert(expressionAsString, routeInformation);
            return routeInformation;
        }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteValueDictionary RouteValueDictionary { get; set; }
    }
}
