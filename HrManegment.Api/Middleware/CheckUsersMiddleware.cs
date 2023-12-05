using HR.LeaveManagement.Api.Middleware;
using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;
using HrManegment.Application.Model.Identity;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace HrManegment.Api.Middleware
{
    public class CheckUsersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckUsersMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            this._logger = logger;
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
            _httpContextAccessor= httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
         {
            var controllerName = httpContext.GetRouteData()?.Values["controller"]?.ToString();

            // Check if the controller is the one you want to exclude
            if (!IsExcludedController(controllerName))
            {

                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)
                {
                    var UserId = GetUserId(token);

                    var Blocked = BlockedUser(UserId);
                    if (!Blocked)
                    {

                        try
                        {
                            await _next(httpContext);
                        }
                        catch (Exception ex)
                        {
                            await HandleExceptionAsync(httpContext, ex);
                        }
                    }

                    else
                    {
                        httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        await httpContext.Response.WriteAsync("Unauthorized");
                    }


                }

            }

            else await _next(httpContext);
        }



        private bool IsExcludedController(string controllerName)
        {
            // Customize this logic based on your requirements
            return controllerName == "Auth";
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = new();

            switch (ex)
            {
                case UnauthorizedAccessException unauthorized:
                    statusCode = HttpStatusCode.Unauthorized;
                    problem = new CustomProblemDetails
                    {
                        Title = unauthorized.Message,
                        Status = (int)statusCode,
                        Type = nameof(UnauthorizedAccessException),
                        Detail = unauthorized.InnerException?.Message,
                    };
                    break;
               
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonConvert.SerializeObject(problem);
            _logger.LogError(logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem);

        }


        private bool BlockedUser (string userId)
        {

           if(_memoryCache.TryGetValue("LogOutUsers", out LogoutCachingData CurrentData))
            {

              var found=CurrentData.UsersId.Find(user=>user==userId);
                if(found==userId) 
                   return true;
            }

            return false;
        }

        private string GetUserId( string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                
                var uid = jsonToken.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

                if (!string.IsNullOrEmpty(uid))
                {
                    return uid;
                }
            }
            return "";
        }


    }
}
