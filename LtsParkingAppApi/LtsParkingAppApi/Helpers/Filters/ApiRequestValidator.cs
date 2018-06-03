using LtsParkingAppApi.Helpers.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.Helpers.Filters
{
    public class ApiRequestValidator : IAuthorizationFilter
    {
        private IConfiguration _config;
        private IHttpContextAccessor _httpContextAccessor;

        public ApiRequestValidator(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!WebApiHelper.ValidateInternalToken(context.HttpContext.Request, _config))
                throw new UnauthorizedAccessException();
        }
    }
}
