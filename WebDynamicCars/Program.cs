using Domain.UseCase;
using Domain.Ñontracts;
using DynamicCarsNew.Infrastructure;
using Infrastructure.Repository;
using WebDynamicCars.Session;

var builder = WebApplication.CreateBuilder(args);

var ordersFilePath = builder.Configuration["DataFiles:Orders"];
var clientsFilePath = builder.Configuration["DataFiles:Clients"];

builder.Services.AddScoped<IOrderRepository>(provider =>
    new OrderRepository(ordersFilePath));

builder.Services.AddScoped<IClientRepository>(provider =>
    new ClientRepository(clientsFilePath));

builder.Services.AddSingleton<IdGenerator>();
builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddTransient<IDeliveryOption, PickupOption>();
builder.Services.AddScoped<RegisterClientUseCase>();
builder.Services.AddScoped<LoginClientUseCase>();
builder.Services.AddSingleton<ClientSessionService>();
builder.Services.AddScoped<CancelOrderUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
