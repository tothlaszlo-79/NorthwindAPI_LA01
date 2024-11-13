using Microsoft.EntityFrameworkCore;
using NorthwindAPI_LA01.Data;
using Microsoft.OpenApi.Models;
using NorthwindAPI.Auth;
using NorthwindAPI_LA01.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NorthwindAPIéalgkjdkfgjldfk", Version = "v1" });

    c.AddSecurityDefinition("APIKey", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Name = "X-Api-Key",
        Type = SecuritySchemeType.ApiKey

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "APIKey"
                        }
                    },
                    new string[] { }
                }});


});

builder.Services.AddAuthentication(options =>
{
    // Set the default authentication scheme to the custom API key authentication scheme
    options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
    // Set the default challenge scheme to the custom API key authentication scheme
    options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;

    // Add support for API key authentication
}).AddApiKeySupport(options => { options.ToString(); });



ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<NothwindContext>(options => options.UseNpgsql(
    configuration["ConnectionStrings:NorthwindConnection"]));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
