using FluentValidation;
using FluentValidation.AspNetCore;
using FlyTonight.API.Options;
using FlyTonight.API.Services;
using FlyTonight.Application.Interfaces;
using FlyTonight.Application.Pipeline;
using FlyTonight.Application.Services;
using FlyTonight.DAL.Data;
using FlyTonight.DAL.Exceptions;
using FlyTonight.DAL.Repositories;
using FlyTonight.Domain.Interfaces;
using FlyTonight.Domain.Models;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Database
builder.Services.AddDbContext<FlyTonightDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Identity
builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<FlyTonightDbContext>()
.AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

//Problem Details
builder.Services
    .AddProblemDetails(options =>
    {
        options.MapToStatusCode<EntityNotFoundException>(StatusCodes.Status404NotFound);
        options.MapToStatusCode<ValidationException>(StatusCodes.Status400BadRequest);
        options.MapToStatusCode<InvalidOperationException>(StatusCodes.Status400BadRequest);
        options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
        options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
        options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
    })
    .AddControllers()
    .AddProblemDetailsConventions()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers().AddFluentValidation(s => 
{
    s.RegisterValidatorsFromAssembly(Assembly.Load("FlyTonight.Application"));
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

// Swagger
builder.Services.AddSwaggerDocument(document =>
{
    document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });

    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

// Cors
var FrontendOrigins = "_frontendOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: FrontendOrigins,
        policy =>
        {
            policy
                .WithOrigins(builder.Configuration["Frontend:BaseUrl"])
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader();
        });
});

// MediatR
builder.Services.AddMediatR(Assembly.Load("FlyTonight.Application"));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SaveBehavior<,>));

//Services
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IStorage, BlobStorage>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEventReportGeneratorService, EventReportGeneratorService>();

// Repositories
builder.Services.AddScoped<IUnitWork, UnitWork>();
builder.Services.AddScoped<ITaxRepository, TaxRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<IPlaneRepository, PlaneRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Options
builder.Services.Configure<BlobOptions>(builder.Configuration.GetSection(BlobOptions.Blob));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Email));

// Random event generator service
builder.Services.AddHostedService<EnvEventGeneratorService>();

// Event spreadsheet generator service
builder.Services.AddHostedService<EnvEventSheetGeneratorService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FlyTonightDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseCors(FrontendOrigins);

app.UseProblemDetails();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
