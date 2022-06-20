using DataLayer;
using DataLayer.Repos;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using ServiceLayer.Utilities;

static void AddRepos(IServiceCollection services)
{
    services.AddScoped<ITicketRepo, TicketRepo>();
    services.AddScoped<IBillRepo, BillRepo>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
}

static void AddServices(IServiceCollection services)
{
    services.AddScoped<ITicketService, TicketService>();
    services.AddScoped<IBillingService, BillingService>();
    services.AddScoped<IATMService, ATMService>();
    services.AddScoped<IBarrierService, BarrierService>();
    services.AddScoped<IBillService, BillService>();
}

static void AddAppUtility(IServiceCollection services)
{
    services.AddSingleton<IAppUtility, TestAppUtility>();
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddAppUtility(builder.Services);

AddRepos(builder.Services);

AddServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            context.Items["Exception"] = contextFeature.Error.Message;
            context.Items["StackTrace"] = contextFeature.Error.StackTrace;

            context.Response.StatusCode = contextFeature.Error switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                AppException => StatusCodes.Status200OK,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new List<string> { contextFeature.Error.Message }));
        }
    });
});

app.Run();

