using Strategy.GameLogic.Entities.Army.Base;

namespace Strategy.GameLogic.Entities.Army
{
    public class Shooter : BaseArmyUnit
    {
        public Shooter()
        {
            Protection = 3;
            Attack = 4;
            SpeedAndInitiative = 3;
            PreparationTime = 3;
            HiringCost = 8;
        }
    }
}
