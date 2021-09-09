using Strategy.GameLogic.Entities.Army.Base;

namespace Strategy.GameLogic.Entities.Army
{
    public class Cavalry : BaseArmyUnit
    {
        public Cavalry()
        {
            Protection = 7;
            Attack = 20;
            SpeedAndInitiative = 3;
            PreparationTime = 4;
            HiringCost = 48;
        }
    }
}
