using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;

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