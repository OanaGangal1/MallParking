using DataLayer;
using DataLayer.Repos;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

static void AddRepos(IServiceCollection services)
{
    services.AddScoped<ITicketRepo, TicketRepo>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
}

static void AddServices(IServiceCollection services)
{
    services.AddScoped<ITicketService, TicketService>();
    services.AddScoped<IBillingService, BillingService>();
    services.AddScoped<IATMService, ATMService>();
    services.AddScoped<IBarrierService, BarrierService>();
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.Run();

