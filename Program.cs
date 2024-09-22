using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using UploadService.Data;
using UploadService.Services;

var builder = WebApplication.CreateBuilder(args);

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