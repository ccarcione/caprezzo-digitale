using CaprezzoDigitale.WebApi.ApiKeyAuthorization;
using CaprezzoDigitale.WebApi.ExtensionMethods;
using CaprezzoDigitale.WebApi.Helpers;
using CaprezzoDigitale.WebApi.Models;
using EmailTools;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
// **************************************************************
// Add services to the container.
// **************************************************************

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
}); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Caprezzo Digitale WebApi",
        Version = "v2"
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<ETools>(x => new ETools(builder.Configuration.GetSection("EmailTools").Get<Configuration>()));
builder.Services.AddSingleton(x => new CaprezzoDigitale.WebApi.Models.Options()
{
    WebApiOptions = builder.Configuration.GetSection("CaprezzoDigitale.WebApi.Models.Options").Get<Dictionary<string, string>>(),
    EmailDestinatariFeedback = builder.Configuration.GetSection("CaprezzoDigitale.WebApi.Models.Options.EmailDestinatariFeedback").Get<IEnumerable<string>>(),
    EmailDestinatariLog = builder.Configuration.GetSection("CaprezzoDigitale.WebApi.Models.Options.EmailDestinatariLog").Get<IEnumerable<string>>(),
    ApiKeyAuthOptions = builder.Configuration.GetSection("CaprezzoDigitale.WebApi.Models.Options.ApiKeyAuthOptions").Get<IEnumerable<ApiKeyAuthOptions>>()
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});


// **************************************************************
// .NET CORE PIPELINE
// **************************************************************
var app = builder.Build();

// Configure the HTTP request pipeline.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = $"Swagger UI - Caprezzo Digitale - {builder.Environment.EnvironmentName}";
        c.SwaggerEndpoint("/swagger/v2/swagger.json", $"caprezzo_digitale {builder.Environment.EnvironmentName} API V2");
    });
    app.UseDeveloperExceptionPage();
}

app.UseStaticFilesCaprezzoDigitale(app.Services, builder.Environment);

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

DatabaseHelper.UpdateDatabaseMigrate(app, app.Services.GetRequiredService<ILogger<Program>>());
DatabaseHelper.SeedAllData(app, app.Services.GetRequiredService<ILogger<Program>>());

app.Run();
