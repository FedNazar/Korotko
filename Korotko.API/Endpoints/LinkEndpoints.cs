/*
 * Korotko
 * API
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Korotko.API.Endpoints
{
    public static class LinkEndpoints
    {
        public static void MapKorotkoEndpoints(this WebApplication app)
        {
            app.MapGet("/api/links/{id}", Get)
               .RequireCors("AllowWebsite")
               .Produces<string>(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound);

            app.MapPost("/api/links", Post)
               .RequireCors("AllowWebsite")
               .Produces<string>(StatusCodes.Status201Created)
               .Produces(StatusCodes.Status400BadRequest);
        }

        /// <summary>
        /// Get URL with the specified short link ID.
        /// </summary>
        /// <param name="id">Short link ID.</param>
        /// <returns>Original long URL.</returns>
        /// <response code="200">Success.</response>
        /// <response code="404">ID not found.</response>
        private static async Task<IResult> Get(string id,
            [FromServices] IShortLinkService shortLinkService)
        {
            string url = await shortLinkService.GetAsync(id);
            return Results.Text(url, "text/plain", statusCode: 200);
        }

        /// <summary>
        /// Create a short link.
        /// </summary>
        /// <param name="url">URL to be shortened.</param>
        /// <returns>Short link ID.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Invalid URL.</response>
        private static async Task<IResult> Post([FromBody] string url,
            [FromServices] IShortLinkService shortLinkService)
        {
            string newId = await shortLinkService.CreateAsync(url);
            return Results.Text(newId, "text/plain", statusCode: 201);
        }
    }
}
