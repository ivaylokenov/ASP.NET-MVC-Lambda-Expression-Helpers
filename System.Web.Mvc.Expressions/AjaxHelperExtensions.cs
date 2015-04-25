namespace System.Web.Mvc.Expressions
{
    using System.Linq.Expressions;
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
            object htmlAttributes = null) where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression(action, routeValues);
            return helper.BeginForm(routeInfo.ActionName, routeInfo.ControllerName, routeValues, ajaxOptions, htmlAttributes);
        }
    }
}
