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
            var expressionAsString = string.Format("{0}{1}", action.ToString(), routeValues);
            if (HttpRuntime.Cache[expressionAsString] != null)
            {
                return HttpRuntime.Cache[expressionAsString] as RouteInformation;
            }

            var routeValueDict = routeValues == null ? null : new RouteValueDictionary(routeValues);

            var type = typeof(TController);
            string controllerName = type.GetControllerName();
            string actionName = action.GetActionName();
            routeValueDict.ProcessArea(type);

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
