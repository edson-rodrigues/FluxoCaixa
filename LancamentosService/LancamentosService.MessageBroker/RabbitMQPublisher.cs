using LancamentosService.Domain.Util;
using LancamentosService.MessageBroker.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LancamentosService.MessageBroker
{
    public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
    {
        private readonly RabbitMQSetting _rabbitMqSetting;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

        public RabbitMQPublisher(IOptions<RabbitMQSetting> rabbitMqSetting)
        {
            _rabbitMqSetting = rabbitMqSetting.Value;

            _retryPolicy = Policy
                .Handle<BrokerUnreachableException>() 
                .Or<OperationInterruptedException>() 
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))); 

            
            _circuitBreakerPolicy = Policy
                .Handle<BrokerUnreachableException>()
                .Or<OperationInterruptedException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 3, 
                    durationOfBreak: TimeSpan.FromSeconds(30) 
                );
        }

        public async Task PublishMessageAsync(T message, string queueName)
        {
            
            var policy = Policy.WrapAsync(_retryPolicy, _circuitBreakerPolicy);

            await policy.ExecuteAsync(async () =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMqSetting.HostName,
                    UserName = _rabbitMqSetting.UserName,
                    Password = _rabbitMqSetting.Password
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var messageJson = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(messageJson);

                await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
            });
        }
    }
}
