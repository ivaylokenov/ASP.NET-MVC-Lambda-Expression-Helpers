namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions.Internals;

    public static class UrlHelperExtensions
    {
        public static string Action<TController>(
                this UrlHelper url,
                Expression<Action<TController>> action,
                object routeValues = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return url.Action(routeInfo.ActionName, routeInfo.ControllerName, routeInfo.RouteValueDictionary);
        }

        public static string Action<TController>(
                this UrlHelper url,
                Expression<Func<TController, Task>> action,
                object routeValues = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return url.Action(routeInfo.ActionName, routeInfo.ControllerName, routeInfo.RouteValueDictionary);
        }
    }
}
