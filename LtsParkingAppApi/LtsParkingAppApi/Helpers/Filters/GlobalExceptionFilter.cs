using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LtsParkingAppApi.Helpers.Filters
{
    /// <summary>
    /// Exception filter is user to log all the exception occuring at the application
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        Serilog.ILogger _logger;
        public GlobalExceptionFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.Error(context.Exception, "Team2-LtsParkingApi");
        }
    }
}
