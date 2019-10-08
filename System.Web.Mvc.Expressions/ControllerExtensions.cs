namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
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
            return GetRedirectFromExpression<TController>(action, routeValues);
        }

        public static RedirectToRouteResult RedirectToAction<TController>(
                this TController controller,
                Expression<Func<TController, Task>> action,
                object routeValues = null)
            where TController : Controller
        {
            return GetRedirectFromExpression<TController>(action, routeValues);
        }

        public static RedirectToRouteResult RedirectToAction<TRedirectController>(
                this Controller controller,
                Expression<Action<TRedirectController>> action,
                object routeValues = null)
            where TRedirectController : Controller
        {
            return GetRedirectFromExpression<TRedirectController>(action, routeValues);
        }

        public static RedirectToRouteResult RedirectToAction<TRedirectController>(
                this Controller controller,
                Expression<Func<TRedirectController, Task>> action,
                object routeValues = null)
            where TRedirectController : Controller
        {
            return GetRedirectFromExpression<TRedirectController>(action, routeValues);
        }

        public static RedirectToRouteResult RedirectToActionPermanent<TController>(
                this TController controller,
                Expression<Action<TController>> action,
                object routeValues = null)
            where TController : Controller
        {
            return GetRedirectFromExpression<TController>(action, routeValues, true);
        }

        public static RedirectToRouteResult RedirectToActionPermanent<TController>(
                this TController controller,
                Expression<Func<TController, Task>> action,
                object routeValues = null)
            where TController : Controller
        {
            return GetRedirectFromExpression<TController>(action, routeValues, true);
        }

        public static RedirectToRouteResult RedirectToActionPermanent<TRedirectController>(
                this Controller controller,
                Expression<Action<TRedirectController>> action,
                object routeValues = null)
            where TRedirectController : Controller
        {
            return GetRedirectFromExpression<TRedirectController>(action, routeValues, true);
        }

        public static RedirectToRouteResult RedirectToActionPermanent<TRedirectController>(
                this Controller controller,
                Expression<Func<TRedirectController, Task>> action,
                object routeValues = null)
            where TRedirectController : Controller
        {
            return GetRedirectFromExpression<TRedirectController>(action, routeValues, true);
        }

        private static RedirectToRouteResult GetRedirectFromExpression<TRedirectController>(
                LambdaExpression action,
                object routeValues = null,
                bool permanent = false)
            where TRedirectController : Controller
        {
            var routeValuesDictionary = new RouteValueDictionary(routeValues);
            routeValuesDictionary.AddRouteValuesFromExpression<TRedirectController>(action);

            return new RedirectToRouteResult(null, routeValuesDictionary, permanent);
        }
    }
}
