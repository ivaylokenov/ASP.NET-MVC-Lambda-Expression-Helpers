namespace System.Web.Mvc.Expressions.Internals
{
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteInformation
    {
        public RouteInformation(string actionName, string controllerName, RouteValueDictionary routeValueDictionary)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.RouteValueDictionary = routeValueDictionary;
        }

        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteValueDictionary RouteValueDictionary { get; set; }

        public static RouteInformation FromExpression<TController>(LambdaExpression action, object routeValues = null)
            where TController : Controller
        {
            string actionName = action.GetActionName();

            var controllerType = typeof(TController);
            string controllerName = controllerType.GetControllerName();

            var routeValueDictionary = new RouteValueDictionary(routeValues);
            routeValueDictionary.ProcessParameters(action);
            routeValueDictionary.ProcessArea(controllerType);

            var routeInformation = new RouteInformation(actionName, controllerName, routeValueDictionary);
            return routeInformation;
        }
    }
}
