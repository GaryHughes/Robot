using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Robot.Backend
{
    // This is a simple filter to collect usage stats for the ApiController class.
    // It could be generalised to include all Controllers and/or to provide more
    // data such as success/failure counts.
    
    public class ApiUsage
    {
        public long Count { get; private set; }
        public void IncrementCount() {
            Count += 1;
        }
    }

    public class ApiUsageFilter : IActionFilter
    {
        readonly Dictionary<string, ApiUsage> _actionCounts = new Dictionary<string, ApiUsage>();

        public IDictionary<string, ApiUsage> ActionCounts => _actionCounts;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controllers.ApiController _) {    
                if (context.ActionDescriptor is ControllerActionDescriptor descriptor) {
                    if (!_actionCounts.TryGetValue(descriptor.ActionName, out var usage)) {
                        usage = new ApiUsage();
                        _actionCounts[descriptor.ActionName] = usage;
                    }
                    usage.IncrementCount();
                }
            }
        }
    }
}