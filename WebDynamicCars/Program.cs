using Domain.UseCase;
using Domain.Ñontracts;
using DynamicCarsNew.Infrastructure;
using Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

var ordersFilePath = builder.Configuration["DataFiles:Orders"];
var clientsFilePath = builder.Configuration["DataFiles:Clients"];

builder.Services.AddScoped<IOrderRepository>(_ => new OrderRepository(ordersFilePath));
builder.Services.AddScoped<IClientRepository>(_ => new ClientRepository(clientsFilePath));

builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<CancelOrderUseCase>();
builder.Services.AddScoped<RegisterClientUseCase>();
builder.Services.AddScoped<LoginClientUseCase>();

builder.Services.AddSingleton<IdGenerator>();
builder.Services.AddTransient<IDeliveryOption, PickupOption>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();

