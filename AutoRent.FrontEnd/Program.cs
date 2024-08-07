using AutomobileRent.Core.Contracts;
using AutomobileRent.Core.Repositories;
using AutomobileRent.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<IWorkerDBRepository, WorkerDBRepository>(_ => new WorkerDBRepository("Server=localhost;Database=autonuoma;Trusted_Connection=True;"));
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
