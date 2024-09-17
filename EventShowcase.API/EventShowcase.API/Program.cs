using EventShowcase.API.Endpoints;
using EventShowcase.API.Extensions;
using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Services;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators;
using EventShowcase.DataAccess.Postgres;
using EventShowcase.DataAccess.Postgres.Repositories;
using EventShowcase.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddApiAuthentication(configuration);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventValidator>();
builder.Services.AddValidatorsFromAssembly(typeof(UserValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(EventValidator).Assembly);



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
builder.Services.AddScoped<IValidator<User>, UserValidator>();
builder.Services.AddScoped<IValidator<Event>, EventValidator>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<EventsService>();


builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<EventShowcase.DataAccess.Postgres.AuthorizationOptions>(configuration.GetSection(nameof(EventShowcase.DataAccess.Postgres.AuthorizationOptions)));


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");
app.MapUsersEndpoints();
app.MapEventsEndpoints();
// Configure the HTTP request pipeline.
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
