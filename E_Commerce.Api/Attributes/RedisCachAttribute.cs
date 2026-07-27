using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Api.Attributes
{
    
    public class RedisCachAttribute : ActionFilterAttribute
    {
        private readonly int  _durationInsec;
        public RedisCachAttribute(int durationInsec = 90)
        {
             _durationInsec = durationInsec;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            var cachKey = CreateCacheKey(context.HttpContext.Request);
            var cached = await cacheService.GetAsync(cachKey);
            if (!string.IsNullOrEmpty(cached)) {
                context.Result = new ContentResult()
                {
                    Content = cached,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }
            var executed = await next.Invoke();
            if(executed.Result is OkObjectResult { Value : not null} ok)
            {
                //var jsonResponse = JsonSerializer.Serialize(ok.Value);
                await cacheService.SetAsync(cachKey, ok.Value, TimeSpan.FromSeconds(_durationInsec));
            }

        }
        private static string CreateCacheKey(HttpRequest request) {
            var key = new StringBuilder();
            key.Append(request.Path);
            key.Append('?');
            foreach (var (k, v) in request.Query.OrderBy(q => q.Key)) {
                key.Append(k);
                key.Append('=');
                key.Append(v);
                key.Append('&');
            }
            return key.ToString();
        }

    }
}
