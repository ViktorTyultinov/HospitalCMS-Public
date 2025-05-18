using System.Threading.RateLimiting;
using Hospital.API.Extensions;
using Hospital.Composition.ServiceCollection;
using Hospital.Infrastructure.Persistance;
using Hospital.Infrastructure.Persistance.BackgroundServices;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOpenApi()
    .AddCorsPolicy()
    .AddHospitalServices()
    .AddJwtAuthentication(builder.Configuration)
    .AddDbContextPool<HospitalDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddRateLimiter(options =>
    {
        options.AddSlidingWindowLimiter("sliding", opt =>
        {
            opt.PermitLimit = 10;
            opt.Window = TimeSpan.FromSeconds(10);
            opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            opt.QueueLimit = 3;
        });
    })
    .AddOpenApiDocument(config =>
    {
        config.Title = "Medix API";
        config.Version = "v1";
        config.PostProcess = document =>
        {
            document.Info.Description = "Minimal OpenAPI spec";
        };
    });

builder.Services.AddControllers();
builder.ConfigureLogging();

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5001); // Set the port to 5001
});

builder.Services.AddHostedService<ExpiredTokenCleanupService>();
builder.Services.AddHostedService<DatabaseBackupService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
    await db.Hospitals.AsNoTracking().FirstOrDefaultAsync();
}

app.UseOpenApi(settings =>
{
    settings.Path = "/openapi/v1/openapi.json"; // Customize path
});

app.UseRouting();
app.MapControllers();
app.UseCorsPolicy();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
