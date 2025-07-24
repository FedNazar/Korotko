/*
 * Korotko
 * API
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Application.Exceptions;
using Korotko.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Korotko.API.Middleware
{
    public class ExceptionHandlingMiddleware() : IMiddleware
    {
        private async Task ProblemDetailsResponse(HttpContext context,
            ProblemDetails problemDetails)
        {
            context.Response.StatusCode = problemDetails.Status!.Value;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (InvalidUrlException)
            {
                await ProblemDetailsResponse(context, new()
                {
                    Status = 400,
                    Title = "Invalid URL",
                    Detail = "Attempt to make a short link for the invalid URL."
                });
            }
            catch (LinkNotFoundException)
            {
                await ProblemDetailsResponse(context, new()
                {
                    Status = 404,
                    Title = "Link not found",
                    Detail = "Link with the specified ID was not found."
                });
            }
            catch (Exception)
            {
                await ProblemDetailsResponse(context, new()
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = "Internal server error has occurred."
                });
            }
        }
    }
}
