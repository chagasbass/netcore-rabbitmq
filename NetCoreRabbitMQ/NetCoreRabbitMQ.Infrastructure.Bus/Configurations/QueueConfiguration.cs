using System.Collections.Generic;

namespace NetCoreRabbitMQ.Infrastructure.Bus.Configurations
{
    /// <summary>
    /// classe de configuração da Fila
    /// </summary>
    public static class QueueConfiguration
    {
        public static bool IsDurable { get; private set; } = false;
        public static bool IsExclusive { get; private set; } = false;
        public static bool IsAutoDelete { get; private set; } = false;
        public static IDictionary<string, object> Arguments { get; private set; } = null;

        public static void ChangeIsDurable(bool isDurable) => IsDurable = isDurable;
        public static void ChangeIsExclusive(bool isExclusive) => IsExclusive = isExclusive;
        public static void ChangeIsAutoDelete(bool isAutoDelete) => IsAutoDelete = isAutoDelete;
        public static void ChangeArguments(IDictionary<string, object> arguments) => Arguments = arguments;
    }
}
