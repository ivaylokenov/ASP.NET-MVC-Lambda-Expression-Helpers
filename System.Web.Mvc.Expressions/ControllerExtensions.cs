namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions.Internals;
    using System.Web.Routing;

    public static class ControllerExtensions
    {
        public static RedirectToRouteResult RedirectToAction<TController>(
                this TController controller,
                Expression<Action<TController>> action,
                object routeValues = null)
            where TController : Controller
        {
            return GetRedirectFromExpression(action, routeValues);
        }

        public static RedirectToRouteResult RedirectToAction<TRedirectController>(
                this Controller controller,
                Expression<Action<TRedirectController>> action,
                object routeValues = null)
            where TRedirectController : Controller
        {
            return GetRedirectFromExpression(action, routeValues);
        }

        public static RedirectToRouteResult RedirectToActionPermanent<TController>(
                this TController controller,
                Expression<Action<TController>> action,
                object routeValues = null)
            where TController : Controller
        {
            return GetRedirectFromExpression(action, routeValues, true);
        }

        public static RedirectToRouteResult RedirectToActionPermanent<TRedirectController>(
                this Controller controller,
                Expression<Action<TRedirectController>> action,
                object routeValues = null)
            where TRedirectController : Controller
        {
            return GetRedirectFromExpression(action, routeValues, true);
        }

        private static RedirectToRouteResult GetRedirectFromExpression<TRedirectController>(
                Expression<Action<TRedirectController>> action,
                object routeValues = null,
                bool permanent = false)
            where TRedirectController : Controller
        {
            var routeValuesDictionary = new RouteValueDictionary(routeValues);
            routeValuesDictionary.AddRouteValuesFromExpression(action);

            return new RedirectToRouteResult(null, routeValuesDictionary, permanent);
        }
    }
}
