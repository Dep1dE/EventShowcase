using EventShowcase.API.Contracts.Image.Responses;
//using EventShowcase.API.Endpoints;
using EventShowcase.API.Extensions;
using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Interfaces.Services;
using EventShowcase.Application.Services;
using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Create;
using EventShowcase.DataAccess.Postgres;
using EventShowcase.DataAccess.Postgres.Repositories;
using EventShowcase.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddApiAuthentication(configuration);
builder.Services.AddControllers();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(ImageResponse).Assembly);
});
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventCreateValidator>();
builder.Services.AddValidatorsFromAssembly(typeof(UserCreateValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(EventCreateValidator).Assembly);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EventShowcaseDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("Local"));
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IJwtOptions, JwtOptions>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<EventShowcase.DataAccess.Postgres.AuthorizationOptions>();
builder.Services.AddScoped<Microsoft.AspNetCore.Authorization.AuthorizationOptions>();
builder.Services.AddScoped<IValidator<User>, UserCreateValidator>();
builder.Services.AddScoped<IValidator<Event>, EventCreateValidator>();
builder.Services.AddScoped<IUsersService, UsersService>();
//builder.Services.AddScoped<EventsService>();
builder.Services.AddTransient<IUsersService, UsersService>(); 
builder.Services.AddTransient<UsersService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ReadPolicy", policy =>
        policy.RequirePermissions(UserPermissions.Read));
    options.AddPolicy("CreatePolicy", policy =>
        policy.RequirePermissions(UserPermissions.Create));
    options.AddPolicy("UpdatePolicy", policy =>
        policy.RequirePermissions(UserPermissions.Update));
    options.AddPolicy("DeletePolicy", policy =>
        policy.RequirePermissions(UserPermissions.Delete));
});


builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<EventShowcase.DataAccess.Postgres.AuthorizationOptions>(configuration.GetSection(nameof(EventShowcase.DataAccess.Postgres.AuthorizationOptions)));


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");
//app.MapUsersEndpoints();
//app.MapEventsEndpoints();
//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<EventShowcaseDbContext>();
db.Database.Migrate();


app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
