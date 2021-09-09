using Strategy.GameLogic.Entities.Army.Base;

namespace Strategy.GameLogic.Entities.Army
{
    public class Recruit : BaseArmyUnit
    {
        public Recruit()
        {
            Protection = 2;
            Attack = 2;
            SpeedAndInitiative = 1;
            PreparationTime = 1;
            HiringCost = 2;
        }
    }
}
