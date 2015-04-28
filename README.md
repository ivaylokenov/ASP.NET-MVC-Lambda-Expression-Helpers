ASP.NET-MVC-Lambda-Expression-Helpers
=====================================
Extension methods allowing using Lambda Expressions instead of magic strings in your ASP.NET MVC 5 application. It resolves all route values, including areas and parameters in the method expression.

Currently supported in Controller (add "using System.Web.Mvc.Expressions;"):

- RedirectToAction<HomeController>(c => c.Index())

Currently supported in Views (add namespace "System.Web.Mvc.Expressions" to the web.config file in the Views folder):

- Html.ActionLink<HomeController>(c => c.Index(5))

- Html.BeginForm<HomeController>(c => c.Index(5))

- Html.RenderAction<HomeController>(c => c.Index(5))

- Html.Action<HomeController>(c => c.Index(5))

- Url.Action<HomeController>(c => c.Index(5))

- Ajax.ActionLine<HomeController>(c => c.Index(5))

- Ajax.BeginForm<HomeController>(c => c.Index(5))

Contributors:

- Ivaylo Kenov
- Vladislav Karamfilov