using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Data.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(opts => {
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// Add services to the container.
// Log.Logger = new LoggerConfiguration()
//     .MinimumLevel
//     .Debug()
//     .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();
// builder.Host.UseSerilog();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, Logging>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
