namespace GameStore.Domain.Models
{
    public class GamePlatform
    {
        public string GameId { get; set; }

        public Game Game { get; set; }

        public string PlatformId { get; set; }

        public Platform Platform { get; set; }
    }
}
