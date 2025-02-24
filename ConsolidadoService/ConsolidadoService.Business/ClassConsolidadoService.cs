using ConsolidadoService.Data.Context;
using ConsolidadoService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace ConsolidadoService.Business
{
    public class ClassConsolidadoService
    {
        private readonly IConnection _rabbitMqConnection;
        private readonly IDatabase _redis;
        //private readonly AppDbContext _dbContext;
        private decimal _saldoConsolidado;
        private List<Lancamento> _lancamentosAcumulados = new List<Lancamento>();

        public ClassConsolidadoService(IConnection rabbitMqConnection, IDatabase redis /*AppDbContext dbContext*/)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _redis = redis;
            //_dbContext = dbContext;
            _saldoConsolidado = 0;
        }

        public void IniciarConsumo()
        {
            var channel = _rabbitMqConnection.CreateModel();
            channel.QueueDeclare(queue: "lancamentos", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var lancamento = JsonSerializer.Deserialize<Lancamento>(json);

                _lancamentosAcumulados.Add(lancamento);
                if(lancamento.Tipo == "Crédito")
                {
                    _saldoConsolidado += lancamento.Valor;
                }
                else if(lancamento.Tipo == "Débito")
                {
                    _saldoConsolidado -= lancamento.Valor;
                }
                
                _redis.StringSet("saldo_consolidado", _saldoConsolidado.ToString());

                if (_lancamentosAcumulados.Count >= 100)
                {
                    // await PersistirConsolidadoEmLote();
                    _lancamentosAcumulados.Clear();
                }
            };

            channel.BasicConsume(queue: "lancamentos", autoAck: true, consumer: consumer);
        }

        //private async Task PersistirConsolidadoEmLote()
        //{
        //    var saldoLote = _lancamentosAcumulados.Sum(l => l.Valor);

        //    var dataAtual = DateTime.Today;

        //    //var consolidado = await _dbContext.Consolidados
        //    //    .FirstOrDefaultAsync(c => c.Data == dataAtual);

        //    //if (consolidado == null)
        //    //{
        //    //    consolidado = new Consolidado
        //    //    {
        //    //        Data = dataAtual,
        //    //        Saldo = saldoLote
        //    //    };
        //    //    _dbContext.Consolidados.Add(consolidado);
        //    //}
        //    //else
        //    //{
        //    //    consolidado.Saldo += saldoLote;
        //    //}

        //    //await _dbContext.SaveChangesAsync();            //var consolidado = await _dbContext.Consolidados
        //    //    .FirstOrDefaultAsync(c => c.Data == dataAtual);

        //    //if (consolidado == null)
        //    //{
        //    //    consolidado = new Consolidado
        //    //    {
        //    //        Data = dataAtual,
        //    //        Saldo = saldoLote
        //    //    };
        //    //    _dbContext.Consolidados.Add(consolidado);
        //    //}
        //    //else
        //    //{
        //    //    consolidado.Saldo += saldoLote;
        //    //}

        //    //await _dbContext.SaveChangesAsync();
        //}

        public decimal ObterSaldoConsolidado()
        {
            var saldoString = _redis.StringGet("saldo_consolidado");

            if (saldoString.HasValue && decimal.TryParse(saldoString, out var saldo))
            {
                return saldo;
            }

            return 0;
        }
    }
}
