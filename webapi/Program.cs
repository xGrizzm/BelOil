using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using webapi.core.Constants;
using webapi.MappingProfiles;
using webapi.repositories;
using webapi.repositories.Contexts;
using webapi.repositories.contracts;
using webapi.services;
using webapi.services.contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// CORS policy
builder.Services.AddCors(c => c.AddPolicy("AllowAnyOrigins", builder =>
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Please enter a valid token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme,
                }
            },
            new List<string>()
        }
    });
});

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthorizationConstants.Issuer,
        ValidateAudience = true,
        ValidAudience = AuthorizationConstants.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = AuthorizationConstants.GetSymmetricSecurityKey(),
        ValidateLifetime = true
    });
builder.Services.AddAuthorization();

// Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Context
builder.Services.AddDbContext<PostgreDbContext>((optionsBuilder) =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

// Services
builder.Services.AddTransient<IAuthorizationService, AuthorizationService>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<ILeaderboardService, LeaderboardService>();

// Repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IFieldRepository, FieldRepository>();
builder.Services.AddTransient<IOilPumpRepository, OilPumpRepository>();

var app = builder.Build();

app.MapControllers();
app.UseRouting();

app.UseCors("AllowAnyOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
