using System;
using Strategy.GameLogic.Entities.Army;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic.Entities.Resources
{
    public class Detachment
    {
        public Detachment(
            Position position,
            Position targetPosition,
            int protectorProtectionBonus, 
            int attackerAttackBonus, 
            int attackerProtectionBonus,
            bool v = false)
        {
            Position = position;
            TargetPosition = targetPosition;
            ProtectorProtectionBonus = protectorProtectionBonus;
            AttackerAttackBonus = attackerAttackBonus;
            AttackerProtectionBonus = attackerProtectionBonus;
            Name = Guid.NewGuid();
            if (v)
            {
                Recruits = (new Recruit(), 500);
                Shooters = (new Shooter(), 100);
                Infantrymans = (new Infantryman(), 50);
                Cavalries = (new Cavalry(), 20);
            }
        }

        public (Recruit, int) Recruits { get; set; } = (new Recruit(),0);

        public (Shooter,int) Shooters { get; set; } = (new Shooter(), 0);

        public (Infantryman,int) Infantrymans { get; set; } = (new Infantryman(), 0);

        public (Cavalry,int) Cavalries { get; set; } = (new Cavalry(), 0);

        public Position Position { get; set; } = new Position();

        public Position TargetPosition { get; set; } = new Position();

        public int ProtectorProtectionBonus { get; set; }

        public int AttackerAttackBonus { get; set; }

        public int AttackerProtectionBonus { get; set; }

        public int SpeedDetachment { get; set; }

        public bool IsDead { get; set; } = false;

        public bool IsWin { get; set; } = false;

        public int CountStay { get; set; }

        public int VictoryAwardCoins { get; set; }

        public bool IsPursues { get; set; } = false;

        public Guid Name { get; set; }

        public Guid TargetName { get; set; }
    }
}
