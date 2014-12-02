namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class ControllerExtensions
    {
        public static RedirectToRouteResult RedirectToAction<TController>(this TController controller, Expression<Action<TController>> action, object routeValues = null)
            where TController : Controller
        {
            var routeValuesDict = new RouteValueDictionary(routeValues);
            routeValuesDict.AddRoutesFromExpression(action);

            return new RedirectToRouteResult(routeValuesDict);
        }
    }
}
