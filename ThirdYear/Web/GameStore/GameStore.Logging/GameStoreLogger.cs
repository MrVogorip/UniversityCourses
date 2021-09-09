using GameStore.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace GameStore.Logging
{
    public class GameStoreLogger : IGameStoreLogger
    {
        private readonly ILogger _logger;

        public GameStoreLogger(ILogger<GameStoreLogger> logger)
        {
            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Warn(string message)
        {
            _logger.LogWarning(message);
        }

        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }

        public void Error(string message)
        {
            _logger.LogError(message);
        }
    }
}
