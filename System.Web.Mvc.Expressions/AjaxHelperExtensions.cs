namespace System.Web.Mvc.Expressions
{
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Expressions.Internals;
    using System.Web.Mvc.Html;

    public static class AjaxHelperExtensions
    {
        public static MvcForm BeginForm<TController>(
                this AjaxHelper helper,
                Expression<Action<TController>> action,
                object routeValues = null,
                AjaxOptions ajaxOptions = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.BeginForm(
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                ajaxOptions,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm BeginForm<TController>(
                this AjaxHelper helper,
                Expression<Func<TController, Task>> action,
                object routeValues = null,
                AjaxOptions ajaxOptions = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.BeginForm(
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                ajaxOptions,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLink<TController>(
                this AjaxHelper helper,
                string linkText,
                Expression<Action<TController>> action,
                object routeValues = null,
                AjaxOptions ajaxOptions = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.ActionLink(
                linkText,
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                ajaxOptions,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLink<TController>(
                this AjaxHelper helper,
                string linkText,
                Expression<Func<TController, Task>> action,
                object routeValues = null,
                AjaxOptions ajaxOptions = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.ActionLink(
                linkText,
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                ajaxOptions,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
    }
}
