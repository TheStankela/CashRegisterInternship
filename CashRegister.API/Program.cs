using Microsoft.OpenApi.Models;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using CashRegister.Infrastructure.Repositories;
using CashRegister.Domain.Interfaces;
using CashRegister.Application.Services;
using CashRegister.Application.Interfaces;
using FluentValidation;
using CashRegister.API.Validators;
using FluentValidation.AspNetCore;
using System.Reflection;
using CashRegister.API.Mediator.Handlers.BillHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
	.AddControllers()
	.AddFluentValidation(options =>
	{
		// Validate child properties and root collection elements
		options.ImplicitlyValidateChildProperties = true;
		options.ImplicitlyValidateRootCollectionElements = true;

		// Automatic registration of validators in assembly
		options.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>();
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Initalizing automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IProductBillRepository, ProductBillRepository>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductBillService, ProductBillService>();
builder.Services.AddScoped<IPriceCalculatorService,  PriceCalculatorService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateBillHandler)));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CashRegister.API", Version = "v1" });
});

builder.Services.AddDbContext<CashRegisterDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CashRegisterDBConnection")
    ));



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
