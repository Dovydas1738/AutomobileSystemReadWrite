using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;
using MongoDB.Driver;
using System.Runtime.CompilerServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMongoClient, MongoClient>(_ =>new MongoClient("mongodb+srv://dovism:SLAPTAZODIS@cluster0.dh7gm.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
builder.Services.AddTransient<IMongoDbCacheRepository, MongoDbCacheRepository>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddTransient<IWorkerDBRepository, WorkerDBRepository>(_=> new WorkerDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;"));
builder.Services.AddTransient<IWorkerService, WorkerService>();
builder.Services.AddTransient<IRentOrderRepository, RentOrderDBRepository>(_ => new RentOrderDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;"));
builder.Services.AddTransient<IRentService, RentService>();
builder.Services.AddTransient<ICarsRepository, CarsDBRepository>(_ => new CarsDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;"));
builder.Services.AddTransient<ICarsService, CarsService>();
builder.Services.AddTransient<ICustomersRepository, CustomersDBRepository>(_ => new CustomersDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;"));
builder.Services.AddTransient<ICustomersService, CustomersService>();
builder.Services.AddTransient<AutoRentService>();

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

var cacheService = app.Services.GetRequiredService<ICacheService>();
cacheService.DeleteCaches();

app.Run();




