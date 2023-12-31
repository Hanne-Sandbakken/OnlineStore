using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Configurations;
using OnlineStore.IRepository;
using OnlineStore.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//configurating Athentication with Azure Ad:
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "https://login.microsoftonline.com/a914a576-120d-477d-9304-9eace6cf5d4c";
            options.Audience = "https://azurehanneorg.onmicrosoft.com/7ab50482-19ec-46af-9170-7e8cec38a8e6";
            options.TokenValidationParameters.ValidateIssuer = false; 
        });

//Let the API know that it should use SQL Server:
var connectionString = builder.Configuration.GetConnectionString("OnlineStoreDbConnectionString");
builder.Services.AddDbContext<OnlineStoreDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Serilog:
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.WriteTo.Console().ReadFrom.Configuration(context.Configuration));

//let the application know that it must use AutoMapper. In this way we don't need to manually map from model class to dto-klass in each method in Controllers. 
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

//Lets us use Repositories, and tells the application how they work together. 
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter()); //Adds [Authorize] to all the metohods in every controller

}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
}).AddOData(options =>
{
    options.Select().Filter().OrderBy();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
