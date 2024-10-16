using EventShowcase.API.Contracts.Image.Responses;
using EventShowcase.API.Extensions;
using EventShowcase.API.Middleware;
using EventShowcase.Application.Interfaces.Auth;
using EventShowcase.Application.Interfaces.Repositories;
using EventShowcase.Application.Interfaces.Services;
using EventShowcase.Application.Mappings;
using EventShowcase.Application.RequestsValidators.Create;
using EventShowcase.Application.RequestsValidators.Delete;
using EventShowcase.Application.RequestsValidators.Update;
using EventShowcase.Application.Services;
using EventShowcase.Core.Enums;
using EventShowcase.Core.Models;
using EventShowcase.Core.Validators.Create;
using EventShowcase.DataAccess.Postgres;
using EventShowcase.DataAccess.Postgres.Repositories;
using EventShowcase.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();




builder.Services.AddApiAuthentication(configuration);
builder.Services.AddControllers().AddFluentValidation(configuration =>
{
    configuration.RegisterValidatorsFromAssemblyContaining<UserCreateValidator>();
    configuration.RegisterValidatorsFromAssemblyContaining<EventCreateValidator>();
});
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(ImageResponse).Assembly);
});
builder.Services.AddAutoMapper(typeof(EventMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventCreateValidator>();
builder.Services.AddValidatorsFromAssembly(typeof(UserCreateValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(EventCreateValidator).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<AddNewEventRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddNewImageRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddNewUserRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DeleteEventRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateEventRequestValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));




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
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<EventShowcase.DataAccess.Postgres.AuthorizationOptions>();
builder.Services.AddScoped<Microsoft.AspNetCore.Authorization.AuthorizationOptions>();
builder.Services.AddScoped<IValidator<User>, UserCreateValidator>();
builder.Services.AddScoped<IValidator<Event>, EventCreateValidator>();
builder.Services.AddScoped<IUsersService, UsersService>();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EventShowcaseDbContext>();
    dbContext.Database.Migrate();
    var adminInitializer = new AdminInitializer(scope.ServiceProvider, configuration);
    await adminInitializer.EnsureAdminUserExistsAsync();
}

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
