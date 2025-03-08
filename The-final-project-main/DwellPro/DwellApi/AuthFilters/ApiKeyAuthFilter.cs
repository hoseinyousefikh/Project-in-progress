using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DwellApi.AuthFilters
{
    public class ApiKeyAuthFilter : IActionFilter
    {
        private const string API_KEY_HEADER_NAME = "X-API-KEY";
        private readonly IConfiguration _configuration;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var validApiKey = _configuration["ApiSettings:ApiKey"];
            var providedApiKey = context.HttpContext.Request.Headers[API_KEY_HEADER_NAME].FirstOrDefault();

            if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != validApiKey)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Invalid API Key" });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
