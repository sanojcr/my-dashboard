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

app.UseCors(Constants.POLICY);
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
   app.UseExceptionHandler("/error");
}

app.Map("/error", (HttpContext context) =>
{
    var exceptions = context
    .Features.Get<IExceptionHandlerFeature>()?
    .Error;

    return Results
    .Problem(
        detail: Constants.GENERAL_ERROR,
        title: Constants.SERVER_ERROR,
        statusCode: 500
        );
});

app.UseOutputCache();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();