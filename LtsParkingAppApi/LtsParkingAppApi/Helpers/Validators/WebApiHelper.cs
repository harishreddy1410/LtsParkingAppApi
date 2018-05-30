using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.Helpers.Validators
{
    public class WebApiHelper
    {
        public static bool ValidateInternalToken(HttpRequest request, IConfiguration config)
        {
            try
            {
                var requestToken = GetApiAuthTokenFromRequestHeader(request);

                //Get token from config to compare to requestToken
                Guid configToken = new Guid();
                Guid.TryParse(config.GetSection("PublicAPI")["InternalAccessToken"], out configToken);

                if (configToken == requestToken) return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }
        public static Guid GetApiAuthTokenFromRequestHeader(HttpRequest request)
        {
            try
            {
                //get auth token from header and parse to Guid
                Microsoft.Extensions.Primitives.StringValues headerValue;
                request.Headers.TryGetValue("ApiAuthToken", out headerValue);
                Guid authToken = new Guid();
                Guid.TryParse(headerValue, out authToken);

                return authToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
