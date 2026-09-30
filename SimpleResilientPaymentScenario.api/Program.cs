using Microsoft.EntityFrameworkCore;
using SimpleResilientPaymentScenario.api.Domain.Contracts.Interfaces;
using SimpleResilientPaymentScenario.api.Infrastructure.Banking;
using SimpleResilientPaymentScenario.api.Infrastructure.Data;
using SimpleResilientPaymentScenario.api.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IBankClient, FakeBankClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();