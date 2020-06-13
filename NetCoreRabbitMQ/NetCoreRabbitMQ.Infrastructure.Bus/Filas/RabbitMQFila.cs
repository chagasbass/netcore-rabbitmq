using MediatR;
using NetCoreRabbitMQ.Domain.Core.Bus;
using NetCoreRabbitMQ.Dominio.Core.Comandos;
using NetCoreRabbitMQ.Dominio.Core.Eventos;
using NetCoreRabbitMQ.Dominio.Core.Filas;
using NetCoreRabbitMQ.Infraestrutura.Fila.Configuracoes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetCoreRabbitMQ.Infraestrutura.Fila.Filas
{
    /// <summary>
    /// Implementação do Bus do RabbitMQ
    /// </summary>
    public sealed class RabbitMQFila : IFilaDeEventos
    {
        private readonly IMediator _mediator;//mediator para efetuar as ações
        private readonly Dictionary<string, List<Type>> _fluxos; //grava todos handlers de eventos
        private readonly List<Type> _tiposDeEventos;//lista de tipos de eventos

        public RabbitMQFila(IMediator mediator)
        {
            _mediator = mediator;
            _fluxos = new Dictionary<string, List<Type>>();
            _tiposDeEventos = new List<Type>();
        }

        public Task EnviarComando<T>(T comando) where T : Comando
            => _mediator.Send(comando);


        /// <summary>
        /// A publicação recebe sempre um evento genérico
        /// Por generic pega-se o nome do evento
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        public void PublicarEvento<T>(T evento) where T : Evento
        {
            //criando a fabrica de conexao do rabbitMq
            var fabricaDeConexao = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            //criando uma conexao,abrindo o canal ,recuperando o evento
            using (var conexao = fabricaDeConexao.CreateConnection())
            using (var canal = conexao.CreateModel())
            {
                var nomeDoEvento = evento.GetType().Name;

                //declarando a fila ,o nome da fila sera o nome do evento
                canal.QueueDeclare(nomeDoEvento, FilaConfiguracao.Duravel,
                    FilaConfiguracao.Exclusivo, FilaConfiguracao.AutoDeletavel, FilaConfiguracao.Argumentos);

                var mensagem = JsonSerializer.Serialize(evento);
                var corpoDaMensagem = Encoding.UTF8.GetBytes(mensagem);

                var troca = string.Empty;

                canal.BasicPublish(troca, nomeDoEvento, null, corpoDaMensagem);
            }
        }

        public void EfetuarInscricao<T, TH>()
            where T : Evento
            where TH : IEventoFluxo<T>
        {

            //recuperando o nome do event e o  handler
            var nomeDoEvento = typeof(T).Name;
            var tipoDoFluxo = typeof(TH);

            //verificando se o evento esta na lista de eventos
            if (!_tiposDeEventos.Contains(typeof(T)))
                _tiposDeEventos.Add(typeof(T));

            //se  handler nao contiver a chave para o nome do evento, insere o nome do evento na lista de handlers
            if (!_fluxos.ContainsKey(nomeDoEvento))
                _fluxos.Add(nomeDoEvento, new List<Type>());

            //se o handler de evento ja estiver registrado para o evento
            if (_fluxos[nomeDoEvento].Any(x => x.GetType() == tipoDoFluxo))
            {
                //alterar para domain notification
                throw new ArgumentException($"this handler Type {tipoDoFluxo.Name} already is registered for {nomeDoEvento}", nameof(tipoDoFluxo));
            }

            //add o tipo de handler para o evento pelo nome
            _fluxos[nomeDoEvento].Add(tipoDoFluxo);

            IniciarConsumidorBasico<T>();
        }

        private void IniciarConsumidorBasico<T>() where T : Evento
        {
            //criar a fabrica e setando o consumidor async
            var fabricaDeConexao = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            var conexao = fabricaDeConexao.CreateConnection();
            var canal = conexao.CreateModel();

            var nomeDoEvento = typeof(T).Name;
            
            var reconhecidoAutomaticamente = true;

            canal.QueueDeclare(nomeDoEvento, FilaConfiguracao.Duravel,
                    FilaConfiguracao.Exclusivo, FilaConfiguracao.AutoDeletavel, FilaConfiguracao.Argumentos);

            //criando o consumidor passando o channel
            var consumidor = new AsyncEventingBasicConsumer(canal);

            //evento de consumo
            consumidor.Received += Consumo_Recebido;
            
            canal.BasicConsume(nomeDoEvento, reconhecidoAutomaticamente, consumidor);
        }

        /// <summary>
        /// Evento de recebimento de consumo. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task Consumo_Recebido(object sender, BasicDeliverEventArgs e)
        {
            var nomeDoEvento = e.RoutingKey;
            var mensagem = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
               await ProcessarEvento(nomeDoEvento, mensagem).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //:TODO
                //tratar exception
                //LOG
            }
        }

        private async Task ProcessarEvento(string nomeDoEvento,string mensagem)
        {
            //se o handler contiver o evento...
            if(_fluxos.ContainsKey(nomeDoEvento))
            {
                var inscricoes = _fluxos[nomeDoEvento];

                foreach (var inscricao in inscricoes)
                {
                    var handler = Activator.CreateInstance(inscricao);

                    if (handler == null) continue;

                    var eventType = _tiposDeEventos.SingleOrDefault(t => t.Name.Equals(nomeDoEvento));

                    var evento = JsonSerializer.Deserialize(mensagem, eventType);

                    var tipoConcreto = typeof(IEventoFluxo<>).MakeGenericType(eventType);

                    await (Task)tipoConcreto.GetMethod("Handle").Invoke(handler, new object[] { evento });
                }
            }
        }
    }
}
