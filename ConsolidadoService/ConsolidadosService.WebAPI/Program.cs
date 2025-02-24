using ConsolidadoService.Business;
using ConsolidadoService.Data.Context;
using ConsolidadosService.WebAPI;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IDatabase>(sp =>
{
    // Exemplo de como obter a conexão com o Redis
    var redis = ConnectionMultiplexer.Connect(redisConnectionString); // Substitua pelo seu host
    return redis.GetDatabase();
});
builder.Services.AddSingleton<IConnection>(provider =>
{
    var factory = new ConnectionFactory
    {
        HostName = "host.docker.internal",
        UserName = "guest",
        Password = "guest"
    };
    return factory.CreateConnection();
});
builder.Services.AddSingleton<ClassConsolidadoService>();
builder.Services.AddHostedService<ConsolidadoBackgroundService>();
builder.Services.AddControllers();
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