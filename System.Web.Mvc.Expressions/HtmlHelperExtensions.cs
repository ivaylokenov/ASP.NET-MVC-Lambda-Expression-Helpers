﻿namespace System.Web.Mvc.Expressions
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions.Internals;
    using System.Web.Mvc.Html;

    public static class HtmlHelperExtensions
    {
        /// <summary>Writes an opening &lt;form&gt; tag to the response and sets the action tag to the specified controller and action. The form uses the POST method.</summary>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginForm<TController>(
            this HtmlHelper helper,
            Expression<Action<TController>> action,
            object routeValues = null,
            object htmlAttributes = null)
            where TController : Controller
        {
            return helper.BeginForm(action, FormMethod.Post, routeValues, htmlAttributes);
        }

        /// <summary>Writes an opening &lt;form&gt; tag to the response and sets the action tag to the specified controller and action. The form uses the POST method.</summary>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginForm<TController>(
            this HtmlHelper helper,
            Expression<Func<TController, Task>> action,
            object routeValues = null,
            object htmlAttributes = null)
            where TController : Controller
        {
            return helper.BeginForm(action, FormMethod.Post, routeValues, htmlAttributes);
        }


        /// <summary>Writes an opening &lt;form&gt; tag to the response and sets the action tag to the specified controller and action. The form uses the specified HTTP method and includes the HTML attributes.</summary>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginForm<TController>(
                this HtmlHelper helper,
                Expression<Action<TController>> action,
                FormMethod method,
                object routeValues = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.BeginForm(
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                method,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>Writes an opening &lt;form&gt; tag to the response and sets the action tag to the specified controller and action. The form uses the specified HTTP method and includes the HTML attributes.</summary>
        /// <returns>An opening &lt;form&gt; tag.</returns>
        public static MvcForm BeginForm<TController>(
                this HtmlHelper helper,
                Expression<Func<TController, Task>> action,
                FormMethod method,
                object routeValues = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.BeginForm(
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                method,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLink<TController>(
                this HtmlHelper helper,
                string linkText,
                Expression<Action<TController>> action,
                object routeValues = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.ActionLink(
                linkText,
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString ActionLink<TController>(
                this HtmlHelper helper,
                string linkText,
                Expression<Func<TController, Task>> action,
                object routeValues = null,
                object htmlAttributes = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.ActionLink(
                linkText,
                routeInfo.ActionName,
                routeInfo.ControllerName,
                routeInfo.RouteValueDictionary,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static void RenderAction<TController>(
                this HtmlHelper helper,
                Expression<Action<TController>> action,
                object routeValues = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            helper.RenderAction(routeInfo.ActionName, routeInfo.ControllerName, routeInfo.RouteValueDictionary);
        }

        public static void RenderAction<TController>(
                this HtmlHelper helper,
                Expression<Func<TController, Task>> action,
                object routeValues = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            helper.RenderAction(routeInfo.ActionName, routeInfo.ControllerName, routeInfo.RouteValueDictionary);
        }

        public static MvcHtmlString Action<TController>(
                this HtmlHelper helper,
                Expression<Action<TController>> action,
                object routeValues = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.Action(routeInfo.ActionName, routeInfo.ControllerName, routeInfo.RouteValueDictionary);
        }

        public static MvcHtmlString Action<TController>(
                this HtmlHelper helper,
                Expression<Func<TController, Task>> action,
                object routeValues = null)
            where TController : Controller
        {
            var routeInfo = RouteInformation.FromExpression<TController>(action, routeValues);
            return helper.Action(routeInfo.ActionName, routeInfo.ControllerName, routeInfo.RouteValueDictionary);
        }
    }
}
