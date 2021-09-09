using System.Collections.Generic;

namespace Strategy.GameLogic.Entities.Resources
{
    public class QueueDetachment
    {
        public Queue<(int, int)> QueueRecruits { get; set; } = new Queue<(int, int)>();

        public Queue<(int, int)> QueueShooters { get; set; } = new Queue<(int, int)>();

        public Queue<(int, int)> QueueInfantrymans { get; set; } = new Queue<(int, int)>();

        public Queue<(int, int)> QueueCavalries { get; set; } = new Queue<(int, int)>();
    }
}
