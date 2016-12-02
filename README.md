<h1><img src="https://raw.githubusercontent.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers/master/logo.png" align="left" alt="MyTested.AspNetCore.Mvc" width="100">&nbsp; ASP.NET-MVC-Lambda-Expression-Helpers - Typed <br />&nbsp;  link generation for ASP.NET MVC</h1>
=====================================
Extension methods allowing using Lambda Expressions instead of magic strings in your ASP.NET MVC 5 application. It resolves all route values, including areas and parameters in the method expression.

[![Build status](https://ci.appveyor.com/api/projects/status/7afu9dfmj9y1k0bv?svg=true)](https://ci.appveyor.com/project/ivaylokenov/asp-net-mvc-lambda-expression-helpers) [![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://github.com/ivaylokenov/ASP.NET-MVC-Lambda-Expression-Helpers/blob/master/LICENSE) [![NuGet Badge](https://buildstats.info/nuget/System.Web.Mvc.Expressions)](https://www.nuget.org/packages/System.Web.Mvc.Expressions/)

To install from NuGet:

	Install-Package System.Web.Mvc.Expressions
	
For other interesting packages check out:

 - [MyTested.AspNetCore.Mvc](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc) - fluent testing framework for ASP.NET Core MVC
 - [MyTested.WebApi](https://github.com/ivaylokenov/MyTested.WebApi) - fluent testing framework for ASP.NET Web API 2
 - [MyTested.HttpServer](https://github.com/ivaylokenov/MyTested.HttpServer) - fluent testing framework for remote HTTP servers
 - [AspNet.Mvc.TypedRouting](https://github.com/ivaylokenov/AspNet.Mvc.TypedRouting) - typed routing and link generation for ASP.NET MVC 6

Currently supported in Controller (add "using System.Web.Mvc.Expressions;"):

```
- RedirectToAction<HomeController>(c => c.Index())

- RedirectToActionPermanent<HomeController>(c => c.Index())

- AddModelError<FooInputModel>(m => m.Bar, "Invalid value for Bar.")

- AddModelError<FooInputModel>(m => m.Baz, new ArgumentException("Invalid value for Baz.")
```

Currently supported in Views (add namespace "System.Web.Mvc.Expressions" to the web.config file in the Views folder):
```
- Html.ActionLink<HomeController>(c => c.Index(5))

- Html.BeginForm<HomeController>(c => c.Index(5))

- Html.RenderAction<HomeController>(c => c.Index(5))

- Html.Action<HomeController>(c => c.Index(5))

- Url.Action<HomeController>(c => c.Index(5))

- Ajax.ActionLink<HomeController>(c => c.Index(5))

- Ajax.BeginForm<HomeController>(c => c.Index(5))
```
More info:
- Support for areas out of the box.
- Support for attribute routing - `RouteAttribute`, `RoutePrefixAttribute` and `RouteAreaAttribute`.
- Support for `ActionNameAttribute` which value overrides the action name when generating URL.
- Support for URL generation to async controller actions.

Authors:

- [Ivaylo Kenov](https://github.com/ivaylokenov)
- [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)
