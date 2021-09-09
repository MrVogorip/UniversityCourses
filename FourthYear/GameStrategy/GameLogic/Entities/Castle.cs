using System.Collections.Generic;
using Strategy.GameLogic.Entities.Buildings;
using Strategy.GameLogic.Entities.Resources;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic.Entities
{
    public class Castle
    {
        public Castle(Position position)
        {
            Position = position;

            CastleDetachment = new Detachment(
                Position,
                Position,
                Walls.DefenseForArmy,
                Temple.AttackForArmyOnAttack,
                Temple.AttackForArmyOnDefensive,
                true);
        }

        public Resource Resource { get; } = new Resource();

        public TownHall TownHall { get; } = new TownHall();

        public ResidentialBuildings ResidentialBuildings { get; } = new ResidentialBuildings();

        public Walls Walls { get; } = new Walls();

        public Temple Temple { get; } = new Temple();

        public Barracks Barracks { get; } = new Barracks();

        public Detachment CastleDetachment { get; }

        public QueueDetachment QueueDetachment { get; } = new QueueDetachment();

        public List<Detachment> Detachments { get; } = new List<Detachment>();

        public Position Position { get; private set; } = new Position();
    }
}
