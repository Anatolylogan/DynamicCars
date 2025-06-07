using Domain.UseCase;
using Domain.Ñontracts;
using DynamicCarsNew.Infrastructure;
using Infrastructure.Configuration;
using Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RepositorySettings>(
    builder.Configuration.GetSection("RepositorySettings")
);
var ordersFilePath = builder.Configuration["DataFiles:Orders"];
var clientsFilePath = builder.Configuration["DataFiles:Clients"];
var managersFilePath = builder.Configuration["DataFiles:Managers"];

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IMakerRepository, MakerRepository>();


builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<CancelOrderUseCase>();
builder.Services.AddScoped<RegisterClientUseCase>();
builder.Services.AddScoped<LoginClientUseCase>();
builder.Services.AddScoped<RegisterManagerUseCase>();
builder.Services.AddScoped<LoginManagerUseCase>();
builder.Services.AddScoped<FilterOrdersByStatusUseCase>();
builder.Services.AddScoped<AssignMakerToOrderUseCase>();


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

