using Server.Services;
using Server.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("portfolio", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPortfolioData, PortfolioData>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Only attempt HTTPS redirection if an HTTPS port is configured to avoid log warning
var hasHttps = builder.Configuration["ASPNETCORE_URLS"]?.Contains("https://", StringComparison.OrdinalIgnoreCase) == true
               || Environment.GetEnvironmentVariable("ASPNETCORE_URLS")?.Contains("https://", StringComparison.OrdinalIgnoreCase) == true;
if (hasHttps)
{
    app.UseHttpsRedirection();
}
app.UseCors("portfolio");

// Root
app.MapGet("/", () => Results.Ok(new { message = "Portfolio API running" }));

// Portfolio endpoints
app.MapGet("/api/profile", (IPortfolioData data) => data.GetProfile())
    .WithName("GetProfile")
    .WithDescription("Returns profile metadata");

app.MapGet("/api/projects", (IPortfolioData data) => data.GetProjects())
    .WithName("GetProjects")
    .WithDescription("Returns list of projects");

app.MapGet("/api/experience", (IPortfolioData data) => data.GetExperience())
    .WithName("GetExperience")
    .WithDescription("Returns work experience timeline");

app.MapGet("/api/skills", (IPortfolioData data) => data.GetSkills())
    .WithName("GetSkills")
    .WithDescription("Returns list of skills with proficiency");

app.Run();
