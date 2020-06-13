using System.Collections.Generic;

namespace NetCoreRabbitMQ.Infraestrutura.Fila.Configuracoes
{
    /// <summary>
    /// classe de configuração da Fila
    /// </summary>
    public static class FilaConfiguracao
    {
        public static bool Duravel { get; private set; } = false;
        public static bool Exclusivo { get; private set; } = false;
        public static bool AutoDeletavel { get; private set; } = false;
        public static IDictionary<string, object> Argumentos { get; private set; } = null;

        public static void AlterarDurabilidade(bool isDurable) => Duravel = isDurable;
        public static void AlterarExclusividade(bool isExclusive) => Exclusivo = isExclusive;
        public static void AlterarAutoDelecao(bool isAutoDelete) => AutoDeletavel = isAutoDelete;
        public static void AlterarArgumentos(IDictionary<string, object> arguments) => Argumentos = arguments;
    }
}
