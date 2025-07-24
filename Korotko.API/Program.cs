/*
 * Korotko
 * API
 * (C) 2025 Nazar Fedorenko
 * Licensed under the MIT License
 */

using Korotko.API.Middleware;
using Korotko.Application;
using Korotko.API.Endpoints;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

Version? mariaDbVersion = null;
string? overrideMariaDbVersion = builder.Configuration.GetSection("MariaDB")["OverrideVersion"];
if (overrideMariaDbVersion != null && overrideMariaDbVersion.Equals("true"))
{
    mariaDbVersion = new Version(
        Convert.ToInt32(builder.Configuration.GetSection("MariaDB")["VersionMajor"]),
        Convert.ToInt32(builder.Configuration.GetSection("MariaDB")["VersionMinor"]),
        Convert.ToInt32(builder.Configuration.GetSection("MariaDB")["VersionPatch"])
    );
}

builder.Services.AddApplicationServices(
    builder.Configuration.GetConnectionString("MariaDB"),
    mariaDbVersion,
    builder.Configuration.GetConnectionString("Redis"),
    builder.Configuration.GetSection("IDs")["Alphabet"]!,
    Convert.ToInt32(builder.Configuration.GetSection("IDs")["MinLength"])
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebsite",
        policy => policy.WithOrigins(builder.Configuration
                            .GetSection("Website")["URL"]!)
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebsite");

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapKorotkoEndpoints();

app.Run();
