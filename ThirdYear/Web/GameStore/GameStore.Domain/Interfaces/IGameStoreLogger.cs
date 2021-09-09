namespace GameStore.Domain.Interfaces
{
    public interface IGameStoreLogger
    {
        void Info(string message);

        void Warn(string message);

        void Debug(string message);

        void Error(string message);
    }
}
