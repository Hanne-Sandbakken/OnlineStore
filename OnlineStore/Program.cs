using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Configurations;
using OnlineStore.IRepository;
using OnlineStore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Let the API know that it should use SQL Server:
var connectionString = builder.Configuration.GetConnectionString("OnlineStoreDbConnectionString");
builder.Services.AddDbContext<OnlineStoreDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//let the application know that it must use AutoMapper. In this way we don't need to manually map from model class to dto-klass in each method in Controllers. 
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//Lets us use Repositories, and tells the application how they work together. 
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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
