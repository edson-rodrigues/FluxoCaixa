using ConsolidadoService.Business;
using ConsolidadoService.Data.Context;
using ConsolidadoService.Domain.Util;
using ConsolidadoService.Worker;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(redisConnectionString));

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

builder.Services.AddHostedService<ConsolidadoBackgroundService>();

var host = builder.Build();

host.Run();
