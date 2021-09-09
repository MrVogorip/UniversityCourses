namespace Strategy.GameLogic.Entities.Army.Base
{
    public class BaseArmyUnit
    {
        public int Protection { get; protected set; }
        
        public int Attack { get; protected set; }

        public int SpeedAndInitiative { get; protected set; }

        public int PreparationTime { get; set; }

        public int HiringCost { get; protected set; }
    }
}
