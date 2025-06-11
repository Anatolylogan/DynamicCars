using Application.UseCase;
using Application.Ñontracts;
using DynamicCarsNew.Infrastructure;
using Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RepositorySettings>(
    builder.Configuration.GetSection("RepositorySettings")
);

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IMakerRepository, MakerRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();

builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<CancelOrderUseCase>();
builder.Services.AddScoped<RegisterClientUseCase>();
builder.Services.AddScoped<LoginClientUseCase>();
builder.Services.AddScoped<RegisterManagerUseCase>();
builder.Services.AddScoped<LoginManagerUseCase>();
builder.Services.AddScoped<FilterOrdersByStatusUseCase>();
builder.Services.AddScoped<AssignMakerToOrderUseCase>();
builder.Services.AddScoped<CompleteMakingUseCase>();
builder.Services.AddScoped<SendToStoreUseCase>();
builder.Services.AddScoped<SendToDeliveryUseCase>();


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

