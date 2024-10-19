using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Serilog;
using UploadService.Data;
using UploadService.Services;


var builder = WebApplication.CreateBuilder(args);

var nodeId = Environment.GetEnvironmentVariable("NODE_ID");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithProperty("nodeId", nodeId)
    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {nodeId} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}") // Customize the output format
    .WriteTo.Http("http://localhost:3100/loki/api/v1/push",
        batchSizeLimitBytes: 1048576,
        queueLimitBytes: 1048576,
        period: TimeSpan.FromSeconds(2),
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information) // Minimum log level to send to Loki
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddDbContext<UploadServiceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<RabbitMQService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();