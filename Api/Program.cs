
using System.Text.Json.Serialization;
using System.Text.Json;
using Api.Services;
using Api.Data;
using System.Threading.RateLimiting;
using Microsoft.OpenApi.Models;
using NLog.Web;
using NLog;
using Asp.Versioning;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    });
    builder.Services.AddRateLimiter(l => l
        .AddFixedWindowLimiter(policyName: "fixed", options =>
        {
            options.PermitLimit = 10;
            options.Window = TimeSpan.FromSeconds(10);
            options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            options.QueueLimit = 5;
        }));
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
    });
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<ICarsService, CarsService>();
    builder.Services.AddScoped<IClientsService, ClientsService>();
    builder.Services.AddScoped<IPartsService, PartsService>();
    builder.Services.AddScoped<IReportService, ReportService>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 1);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    });
    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseHttpsRedirection();
    app.UseRateLimiter();
    app.UseAuthentication();
    app.UseCors("AllowAll");
    app.Use(async (context, next) =>
    {
        Thread.CurrentPrincipal = context.User;
        await next(context);
    });
    app.UseAuthorization();
    app.MapControllers();

    app.Run("http://0.0.0.0:5000");
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}