using MediatR;
using NetCoreRabbitMQ.Domain.Core.Bus;
using NetCoreRabbitMQ.Domain.Core.Comands;
using NetCoreRabbitMQ.Domain.Core.Events;
using NetCoreRabbitMQ.Infrastructure.Bus.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Infrastructure.Bus.Bus
{
    /// <summary>
    /// Implementação do Bus do RabbitMQ
    /// </summary>
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;//mediator para efetuar as ações
        private readonly Dictionary<string, List<Type>> _handlers; //grava todos handlers de eventos
        private readonly List<Type> _eventTypes;//lista de tipos de eventos

        public RabbitMQBus(IMediator mediator)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<T>(T comand) where T : Comand
            => _mediator.Send(comand);


        /// <summary>
        /// A publicação recebe sempre um evento genérico
        /// Por generic pega-se o nome do evento
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        public void Publish<T>(T @event) where T : Event
        {
            //criando a fabrica de conexao do rabbitMq
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            //criando uma conexao,abrindo o canal ,recuperando o evento
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                //declarando a fila ,o nome da fila sera o nome do evento
                channel.QueueDeclare(eventName, QueueConfiguration.IsDurable,
                    QueueConfiguration.IsExclusive, QueueConfiguration.IsAutoDelete, QueueConfiguration.Arguments);

                var message = JsonSerializer.Serialize(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var exchange = string.Empty;

                channel.BasicPublish(exchange, eventName, null, body);
            }
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {

            //recuperando o nome do event e o  handler
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            //verificando se o evento esta na lista de eventos
            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            //se  handler nao contiver a chave para o nome do evento, insere o nome do evento na lista de handlers
            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            //se o handler de evento ja estiver registrado para o evento
            if (_handlers[eventName].Any(x => x.GetType() == handlerType))
            {
                //alterar para domain notification
                throw new ArgumentException($"this handler Type {handlerType.Name} already is registered for {eventName}", nameof(handlerType));
            }

            //add o tipo de handler para o evento pelo nome
            _handlers[eventName].Add(handlerType);

            StartBasciConsume<T>();
        }

        private void StartBasciConsume<T>() where T : Event
        {
            //criar a fabrica e setando o consumidor async
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;
           
            //
            var isAutoAck = true;

            channel.QueueDeclare(eventName, QueueConfiguration.IsDurable,
                    QueueConfiguration.IsExclusive, QueueConfiguration.IsAutoDelete, QueueConfiguration.Arguments);

            //criando o consumidor passando o channel
            var consumer = new AsyncEventingBasicConsumer(channel);

            //evento de consumo
            consumer.Received += Consumer_Received;
            
            channel.BasicConsume(eventName, isAutoAck, consumer);
        }

        /// <summary>
        /// Evento de recebimento de consumo. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
               await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //:TODO
                //tratar exception
                //LOG
            }
        }

        private async Task ProcessEvent(string eventName,string message)
        {
            //se o handler contiver o evento...
            if(_handlers.ContainsKey(eventName))
            {
                var subscriptions = _handlers[eventName];

                foreach (var subscription in subscriptions)
                {
                    var handler = Activator.CreateInstance(subscription);

                    if (handler == null) continue;

                    var eventType = _eventTypes.SingleOrDefault(t => t.Name.Equals(eventName));

                    var @event = JsonSerializer.Deserialize(message, eventType);

                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
