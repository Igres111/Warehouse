using DataAccess.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Service.AuthToken;
using Service.Implementations.ProductRepositorys;
using Service.Implementations.UserRepositories;
using Service.Interfaces.ProductInterfaces;
using Service.Interfaces.TokenInterfaces;
using Service.Interfaces.UserInterfaces;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Service Configuration
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

DotNetEnv.Env.Load();

var connection = Environment.GetEnvironmentVariable("connection");

var key = Environment.GetEnvironmentVariable("Key");

if (string.IsNullOrEmpty(key))
{
    throw new Exception("JWT secret key is not set in the environment variables.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateIssuer = true,
            ValidateAudience = true,
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IProduct,ProductRepo>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IToken, TokenLogic>();

#endregion

var app = builder.Build();

#region Middleware Configuration
app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Exception occurred.");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred. Application is still running.");
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();
#endregion

app.Run();
