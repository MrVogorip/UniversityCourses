using Strategy.GameLogic.Entities;
using Strategy.GameLogic.Services.GameStyle;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic
{
    public class Player
    {
        public Player(Position position)
        {
            Castle = new Castle(position);
        }

        public Castle Castle { get; set; }

        public IGameStyle GameStyle { get; set; }

        public bool IsLost { get; set; } = false;
    }
}
