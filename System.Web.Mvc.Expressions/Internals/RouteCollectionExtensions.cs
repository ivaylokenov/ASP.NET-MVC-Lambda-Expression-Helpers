// Modelled after System.Web.Mvc.RouteCollectionExtensions.FilterRouteCollectionByArea
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace System.Web.Mvc.Expressions.Internals
{
    using System.Web.Routing;

    internal class RouteCollectionExtensions
    {
        internal static bool DetermineUsingAreas(RouteCollection routes)
        {
            using (routes.GetReadLock())
            {
                foreach (RouteBase route in routes)
                {
                    string a = AreaHelpers.GetAreaName(route) ?? string.Empty;
                    if (a.Length > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
