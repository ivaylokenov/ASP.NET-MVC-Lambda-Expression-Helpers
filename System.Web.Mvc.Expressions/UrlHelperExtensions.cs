namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Internals;

    public static class UrlHelperExtensions
    {
        public static string Action<TController>(this UrlHelper url, Expression<Action<TController>> action, object routeValues = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression(action, routeValues);
            return url.Action(routeInfo.ActionName, routeInfo.ControllerName, routeInfo.RouteValueDictionary);
        }
    }
}
