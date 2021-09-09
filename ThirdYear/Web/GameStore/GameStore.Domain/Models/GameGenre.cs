namespace GameStore.Domain.Models
{
    public class GameGenre
    {
        public string GameId { get; set; }

        public Game Game { get; set; }

        public string GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
