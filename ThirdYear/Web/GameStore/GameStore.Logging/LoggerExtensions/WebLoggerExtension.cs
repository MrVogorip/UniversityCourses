using GameStore.Domain.Interfaces;

namespace GameStore.Logging.LoggerExtensions
{
    public static class WebLoggerExtension
    {
        public static void LogFilterExecuted(
            this IGameStoreLogger logger, string controller, string action)
        {
            string message = $"Executed Controller: {controller}; " +
                        $"Action: {action}; ";
            logger.Debug(message);
        }

        public static void LogFilterExecutedError(
            this IGameStoreLogger logger, string controller, string action)
        {
            string message = $"Executed Controller: {controller}; " +
                        $"Action: {action}; ";
            logger.Warn(message);
        }

        public static void LogFilterExecuting(
            this IGameStoreLogger logger, string query, string controller, string action)
        {
            string message = $"Executing: {query}; " +
                        $"Controller: {controller}; " +
                        $"Action: {action}; ";
            logger.Debug(message);
        }

        public static void LogFilterExecutingError(
            this IGameStoreLogger logger, string query, string controller, string action)
        {
            string message = $"Executing: {query}; " +
                        $"Controller: {controller}; " +
                        $"Action: {action}; ";
            logger.Warn(message);
        }
    }
}
