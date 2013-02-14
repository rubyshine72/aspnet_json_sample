﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace BranchingPipelines
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using Owin.Types;

    public class AddBreadCrumbMiddleware
    {
        private AppFunc _next;
        private string _breadcrumb;

        public AddBreadCrumbMiddleware(AppFunc next, string breadcrumb)
        {
            _next = next;
            _breadcrumb = breadcrumb;
        }

        public Task Invoke(IDictionary<string, object> environment)
        {
            OwinRequest request = new OwinRequest(environment);
            request.AddHeader("breadcrumbs", _breadcrumb);
            return _next(environment);
        }
    }
}