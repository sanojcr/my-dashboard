using Microsoft.AspNetCore.Diagnostics;
using MyDashboard.Model;
using MyDashboard.Service;
using MyDashboard.Service.Interface;
using MyDashboard.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<MyDashboardSettings>(
    builder.Configuration
    .GetSection(Constants.MYDASHBOARD_SETTINGS));
var myDashboardSettings = builder.Configuration
    .GetSection(Constants.MYDASHBOARD_SETTINGS)
    .Get<MyDashboardSettings>();

builder.Services
    .AddSingleton(myDashboardSettings!);
builder.Services
    .AddControllers();
builder.Services
    .AddEndpointsApiExplorer();
builder.Services
    .AddSwaggerGen();
builder.Services
    .AddOutputCache();

// custom
builder.Services
    .AddDbContextServices(myDashboardSettings!);
builder.Services
    .AddAutoMapper(typeof(MappingProfile));
builder.Services
    .AddAppServices();
builder.Services
    .RegisterAppLogger();
builder.Services
    .AddJwtAuthentication(myDashboardSettings!);
builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(Constants.POLICY, policy =>
        {
            policy.WithOrigins(myDashboardSettings!.AllowedHosts)
             .AllowAnyHeader()
              .AllowAnyMethod();
        });
    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(Constants.POLICY);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();